using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace OccaSoftware.Buto
{
    public class ButoVolumetricLightingAndFog : ScriptableRendererFeature
    {
        class RenderFogPass : ScriptableRenderPass
        {
            private RenderTargetIdentifier source;
            private RenderTargetHandle fogRenderTarget;
            private RenderTargetHandle fogMergeTarget;
            private RenderTargetHandle depthDownscaleTarget;

            private ButoFogComponent butoFogComponent = null;
            private Material mergeMaterial = null;
            private Material depthDownscaleMaterial = null;
            private const string mergeShaderPath = "Shader Graphs/VolumetricFogMergeShader";
            private const string depthDownscaleShaderPath = "Shader Graphs/DepthDownscaleShader";
            private const string mergeInputTexId = "_FOG_MERGE_INPUT_TEX";
            private const string downscaleInputTexId = "_DOWNSCALED_DEPTH_TEX";

            Shader mergeShader;
            Shader depthShader; 

            void SetupMaterials()
            {
                mergeShader = Shader.Find(mergeShaderPath);
                if (mergeShader != null && mergeMaterial == null)
                {
                    mergeMaterial = CoreUtils.CreateEngineMaterial(mergeShaderPath);
                }

                depthShader = Shader.Find(depthDownscaleShaderPath);
                if (depthShader != null && depthDownscaleMaterial == null)
                {
                    depthDownscaleMaterial = CoreUtils.CreateEngineMaterial(depthShader);
                }
            }

            public RenderFogPass()
            {
                fogRenderTarget.Init("Fog Render Target");
                fogMergeTarget.Init("Fog Merge Target");
                depthDownscaleTarget.Init("Depth Downscale Target");

                SetupMaterials();
            }



            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                if(butoFogComponent == null)
                {
                    butoFogComponent = FindObjectOfType<ButoFogComponent>();
                }

                RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
                descriptor.colorFormat = RenderTextureFormat.DefaultHDR;
                RenderTextureDescriptor descriptorHalfRes = descriptor;
                descriptorHalfRes.width /= 2;
                descriptorHalfRes.height /= 2;
                cmd.GetTemporaryRT(fogRenderTarget.id, descriptorHalfRes);
                cmd.GetTemporaryRT(depthDownscaleTarget.id, descriptorHalfRes);

                cmd.GetTemporaryRT(fogMergeTarget.id, descriptor);
            }

            bool IsValidRenderPass()
            {
                if (butoFogComponent == null)
                    return false;

                if (butoFogComponent.GetFogMaterial() == null || depthDownscaleMaterial == null || mergeMaterial == null)
                    return false;

                return true;
            }


            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (!IsValidRenderPass())
                    return;

                source = renderingData.cameraData.renderer.cameraColorTarget;

                CommandBuffer cmd = CommandBufferPool.Get("FogRenderPass");

                Blit(cmd, source, fogRenderTarget.Identifier(), butoFogComponent.GetFogMaterial());
                cmd.SetGlobalTexture(mergeInputTexId, fogRenderTarget.Identifier());
                Blit(cmd, source, depthDownscaleTarget.Identifier(), depthDownscaleMaterial);
                cmd.SetGlobalTexture(downscaleInputTexId, depthDownscaleTarget.Identifier());

                Blit(cmd, source, fogMergeTarget.Identifier(), mergeMaterial);
                Blit(cmd, fogMergeTarget.Identifier(), source);
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                CommandBufferPool.Release(cmd);
            }


            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(fogRenderTarget.id);
                cmd.ReleaseTemporaryRT(fogMergeTarget.id);
            }
        }

        RenderFogPass renderFogPass;

        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += Recreate;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= Recreate;
        }

        private void Recreate(UnityEngine.SceneManagement.Scene current, UnityEngine.SceneManagement.Scene next)
        {
            Create();
        }

        public override void Create()
        {
            renderFogPass = new RenderFogPass();
            renderFogPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
        }


        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.camera.cameraType == CameraType.Reflection)
                return;

            renderer.EnqueuePass(renderFogPass);
        }
    }



}