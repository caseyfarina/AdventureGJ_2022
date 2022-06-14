using UnityEngine;

namespace OccaSoftware.Buto
{
    [ExecuteAlways]
    public class ButoFogComponent : MonoBehaviour
    {
        [SerializeField] private Material fogMaterial;
        internal static readonly int _MAXLIGHTS = 8;

        private void Awake()
        {
            ButoCommon.CheckLightCount(ButoLight.Lights.Count, _MAXLIGHTS, this);
        }

        public void SetFogMaterial(Material fogMaterial)
        {
            this.fogMaterial = fogMaterial;
        }

        public Material GetFogMaterial()
        {
            return fogMaterial;
        }

        private void Update()
        {
            if (fogMaterial == null)
                return;

            Vector4[] positions = new Vector4[_MAXLIGHTS];
            float[] intensities = new float[_MAXLIGHTS];
            Vector4[] colors = new Vector4[_MAXLIGHTS];

            int lightCount = Mathf.Min(ButoLight.Lights.Count, _MAXLIGHTS);
            for(int i = 0; i < lightCount; i++)
            {
                positions[i] = ButoLight.Lights[i].transform.position;
                intensities[i] = ButoLight.Lights[i].LightIntensity;
                colors[i] = ButoLight.Lights[i].LightColor;
            }

            fogMaterial.SetInt(ShaderParams._LightCountButo, lightCount);
            if(lightCount > 0)
            {
                fogMaterial.SetVectorArray(ShaderParams._LightPosButo, positions);
                fogMaterial.SetFloatArray(ShaderParams._LightIntensityButo, intensities);
                fogMaterial.SetVectorArray(ShaderParams._LightColorButo, colors);
            }
        }


        private static class ShaderParams
        {
            public static int _LightCountButo = Shader.PropertyToID("_LightCountButo");
            public static int _LightPosButo = Shader.PropertyToID("_LightPosButo");
            public static int _LightIntensityButo = Shader.PropertyToID("_LightIntensityButo");
            public static int _LightColorButo = Shader.PropertyToID("_LightColorButo");
        }
    }

}