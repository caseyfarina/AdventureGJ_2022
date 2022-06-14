using UnityEditor;
using UnityEngine;

namespace OccaSoftware.Buto.Editor
{
	public class ButoShaderGUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            materialEditor.serializedObject.Update();
            Material t = materialEditor.target as Material;
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField(new GUIContent("Quality", "Configure settings that affect the baseline quality of the Volumetric Fog."), EditorStyles.boldLabel);
            int _SampleCount = EditorGUILayout.IntSlider(Params.SampleCount.Content, t.GetInt(Params.SampleCount.Id), 16, 128);
            bool _AnimateSamplePosition = EditorGUILayout.Toggle(Params.AnimateSamplePosition.Content, IntToBool(t.GetInt(Params.AnimateSamplePosition.Id)));

            bool _SelfShadowingEnabled = KeywordToggle(t, Params.EnableSelfShadowing);
            bool _HorizonShadowingEnabled = KeywordToggle(t, Params.EnableHorizonShadowing);
            float _MaxDistanceVolumetric = EditorGUILayout.FloatField(Params.MaxDistanceVolumetric.Content, t.GetFloat(Params.MaxDistanceVolumetric.Id));
            bool _AnalyticFogEnabled = KeywordToggle(t, Params.AnalyticFogEnabled);
            float _MaxDistanceNonVolumetric = t.GetFloat(Params.MaxDistanceNonVolumetric.Id);

            if (_AnalyticFogEnabled)
			{
                _MaxDistanceNonVolumetric = EditorGUILayout.FloatField(Params.MaxDistanceNonVolumetric.Content, _MaxDistanceNonVolumetric);
            }
            

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Characteristics", EditorStyles.boldLabel);
            float _FogDensity = EditorGUILayout.FloatField(Params.FogDensity.Content, t.GetFloat(Params.FogDensity.Id));
            float _Anisotropy = EditorGUILayout.Slider(Params.Anisotropy.Content, t.GetFloat(Params.Anisotropy.Id), -1, 1);
            float _LightIntensity = EditorGUILayout.FloatField(Params.LightIntensity.Content, t.GetFloat(Params.LightIntensity.Id));
            float _ShadowIntensity = EditorGUILayout.FloatField(Params.ShadowIntensity.Content, t.GetFloat(Params.ShadowIntensity.Id));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Geometry", EditorStyles.boldLabel);
            float _BaseHeight = EditorGUILayout.FloatField(Params.BaseHeight.Content, t.GetFloat(Params.BaseHeight.Id));
            float _AttenuationBoundarySize = EditorGUILayout.FloatField(Params.AttenuationBoundarySize.Content, t.GetFloat(Params.AttenuationBoundarySize.Id));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color", EditorStyles.boldLabel);
            Texture2D _ColorRamp = (Texture2D)EditorGUILayout.ObjectField(Params.ColorRamp.Content, t.GetTexture(Params.ColorRamp.Id), typeof(Texture2D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            float _ColorRampInfluence = EditorGUILayout.Slider(Params.ColorRampInfluence.Content, t.GetFloat(Params.ColorRampInfluence.Id), 0, 1);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Volumetric Noise", EditorStyles.boldLabel);
            Texture3D _NoiseTexture = (Texture3D)EditorGUILayout.ObjectField(Params.NoiseTexture.Content,  t.GetTexture(Params.NoiseTexture.Id), typeof(Texture3D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            int _Octaves = EditorGUILayout.IntSlider(Params.Octaves.Content, t.GetInt(Params.Octaves.Id), 1, 3);
            float _Lacunarity = t.GetFloat(Params.Lacunarity.Id);
            float _Gain = t.GetFloat(Params.Gain.Id);
            if(_Octaves > 1)
			{
                EditorGUI.indentLevel++;
                _Lacunarity = EditorGUILayout.Slider(Params.Lacunarity.Content, _Lacunarity, 1.0f, 8.0f);
                _Gain = EditorGUILayout.Slider(Params.Gain.Content, _Gain, 0, 1);
                EditorGUI.indentLevel--;
			}
            float _NoiseTiling = EditorGUILayout.FloatField(Params.NoiseTiling.Content, t.GetFloat(Params.NoiseTiling.Id));
            Vector3 _NoiseWindSpeed = EditorGUILayout.Vector3Field(Params.NoiseWindSpeed.Content, t.GetVector(Params.NoiseWindSpeed.Id));
            float _NoiseIntensityMin = t.GetFloat(Params.NoiseIntensityMin.Id);
            float _NoiseIntensityMax = t.GetFloat(Params.NoiseIntensityMax.Id);
            GUIContent noiseIntensityContent = new GUIContent(
                "Noise Intensity Mapping",
                "Remaps the noise input texture from the original [0, 1] range to a new range defined by [NoiseIntensityMin, NoiseIntensityMax]. For example, an mapping of [0.2, 0.8] will remap the noise by clipping any values below 0.2 and any values above 0.8, then remapping the remaining 0.2 to 0.8 range back to 0.0 to 1.0 to retain detail."
                );
            EditorGUILayout.MinMaxSlider(noiseIntensityContent, ref _NoiseIntensityMin, ref _NoiseIntensityMax, 0f, 1f);
            

            

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(t, "ButoShaderUndo");

                _NoiseTiling = Mathf.Max(0, _NoiseTiling);
                _FogDensity = Mathf.Max(0, _FogDensity);
                _AttenuationBoundarySize = Mathf.Max(1, _AttenuationBoundarySize);
                _LightIntensity = Mathf.Max(0, _LightIntensity);
                _ShadowIntensity = Mathf.Max(0, _ShadowIntensity);
                _MaxDistanceVolumetric = Mathf.Max(10, _MaxDistanceVolumetric);
                _MaxDistanceNonVolumetric = Mathf.Max(_MaxDistanceVolumetric + 1, _MaxDistanceNonVolumetric);


                t.SetInt(Params.SampleCount.Id, _SampleCount);
                t.SetInt(Params.AnimateSamplePosition.Id, BoolToInt(_AnimateSamplePosition));
                SetKeyword(t, Params.EnableSelfShadowing.Property, _SelfShadowingEnabled);
                SetKeyword(t, Params.EnableHorizonShadowing.Property, _HorizonShadowingEnabled);
                SetKeyword(t, Params.AnalyticFogEnabled.Property, _AnalyticFogEnabled);

                t.SetFloat(Params.MaxDistanceVolumetric.Id, _MaxDistanceVolumetric);
                t.SetFloat(Params.MaxDistanceNonVolumetric.Id, _MaxDistanceNonVolumetric);
                t.SetFloat(Params.Anisotropy.Id, _Anisotropy);
                t.SetFloat(Params.BaseHeight.Id, _BaseHeight);
                t.SetFloat(Params.AttenuationBoundarySize.Id, _AttenuationBoundarySize);
                t.SetFloat(Params.FogDensity.Id, _FogDensity);
                t.SetFloat(Params.LightIntensity.Id, _LightIntensity);
                t.SetFloat(Params.ShadowIntensity.Id, _ShadowIntensity);
                t.SetTexture(Params.ColorRamp.Id, _ColorRamp);
                t.SetFloat(Params.ColorRampInfluence.Id, _ColorRampInfluence);
                t.SetTexture(Params.NoiseTexture.Id, _NoiseTexture);
                t.SetInt(Params.Octaves.Id, _Octaves);
                t.SetFloat(Params.NoiseTiling.Id, _NoiseTiling);
                t.SetVector(Params.NoiseWindSpeed.Id, _NoiseWindSpeed);
                t.SetFloat(Params.NoiseIntensityMin.Id, _NoiseIntensityMin);
                t.SetFloat(Params.NoiseIntensityMax.Id, _NoiseIntensityMax);
                t.SetFloat(Params.Lacunarity.Id, _Lacunarity);
                t.SetFloat(Params.Gain.Id, _Gain);

                materialEditor.serializedObject.ApplyModifiedProperties();
            }
        }

        private void SetKeyword(Material m, string keyword, bool value)
		{
            if (value)
            {
                m.EnableKeyword(keyword);
            }
            else
            {
                m.DisableKeyword(keyword);
            }
        }

        private bool KeywordToggle(Material material, Param param)
		{
            return EditorGUILayout.Toggle(param.Content, System.Array.IndexOf(material.shaderKeywords, param.Property) != -1);
        }

        private bool IntToBool(int a)
        {
            return a == 0 ? false : true;
        }

        private int BoolToInt(bool a)
        {
            return a == false ? 0 : 1;
        }


        readonly struct Param
		{
            public Param(string property, string label, string tooltip)
			{
                Id = Shader.PropertyToID(property);
                Property = property;
                Content = new GUIContent(label, tooltip);
			}

            readonly public int Id;
            readonly public string Property;
            readonly public GUIContent Content;
		}

        private static class Params
		{
            public static Param SampleCount = new Param(
                "_SampleCount", 
                "Sample Count", 
                "Defines the number of points used to integrate the volumetric fog volume. Higher values are more computationally expensive, but lower values can result in artifacts."
                );

            public static Param AnimateSamplePosition = new Param(
                "_AnimateSamplePosition",
                "Animate Sample Position",
                "When toggled on, the sample positions will be randomly adjusted each frame. This replaces static noise with dynamic noise."
                );

            public static Param EnableSelfShadowing = new Param(
                "_BUTO_SELF_ATTENUATION_ENABLED",
                "Self Shadowing Enabled",
                "When toggled on, fog will realistically attenuate light from the main directional light. Self Shadowing is computationally expensive but looks more realistic."
                );

            public static Param EnableHorizonShadowing = new Param(
                "_BUTO_HORIZON_SHADOWING_ENABLED",
                "Planet Horizon Shadowing Enabled",
                "When toggled on, the fog will be shadowed when the main light is below the horizon line. Horizon Shadowing is computationally expensive but looks more realistic."
                );

            public static Param MaxDistanceVolumetric = new Param(
                "_MaxDistanceVolumetric",
                "Volumetric Fog Sampling Distance",
                "Distance at which the fog renderer will switch from volumetric to analytic fog. This does not affect performance, but can affect fog rendering quality. Smaller values cause the renderer to traverse a smaller distance between fog samples, resulting in higher quality and less artifacts."
                );

            public static Param AnalyticFogEnabled = new Param(
                "_BUTO_ANALYTIC_FOG_ENABLED",
                "Analytic Fog Enabled",
                "Analytic fog replaces volumetric fog after the Volumetric Fog Sampling Distance. Disabling analytic fog can improve performance."
                );

            public static Param MaxDistanceNonVolumetric = new Param(
                "_MaxDistanceNonVolumetric",
                "Analytic Fog Sampling Distance",
                "Maximum distance that will be simulated for analytic fog. This does not affect performance, but can affect the visual characteristics of your scene."
                );


            public static Param Anisotropy = new Param(
                "_Anisotropy",    
                "Anisotropy", 
                "Defines the direction in which light will scatter. Negative values cause light to scatter backwards. Positive values cause light to scatter forwards."
                );

            public static Param BaseHeight = new Param(
                "_BaseHeight",
                "Base Height",
                "Defines the \"floor\" of the fog volume. Fog falloff only begins after the fog passes this altitude."
                );

            public static Param AttenuationBoundarySize = new Param(
                "_AttenuationBoundarySize",
                "Attenuation Boundary Size (m)",
                "Defines the height at which the fog density will have been attenuated to 36% of the set fog density. This is always calculated on top of the Base Height. For example, if your Base Height is 10 and your Attenuation Boundary Size is 15, your fog will be at full density until a Height of y = 10 units, then it will attenuate to 36% of the original density by a height of y = 25 units."
                );

            public static Param FogDensity = new Param(
                "_FogDensity",
                "Fog Density", 
                "Density of fog in the scene."
                );

            public static Param LightIntensity = new Param(
                "_LightIntensity",
                "Light Intensity",
                "A multiplier that affects the intensity of the fog lighting effects in unshadowed areas. Increasing this causes the fog's lighting to become brighter in lit areas. Should only be used for stylized effects. [Default: 1]"
                );

            public static Param ShadowIntensity = new Param(
                "_ShadowIntensity",
                "Shadowed Fog Density",
                "A multiplier that affects the density of the fog in shadowed areas. Increasing this causes the fog to become more dense in shadowed areas. In effect, this causes shadowed areas to become more apparent. However, be wary that it can also cause shadowed areas to become more opaque. Should only be used for stylized effects. [Default: 1]"
                );

            public static Param ColorRamp = new Param(
                "_ColorRamp",
                "Color Ramp",
                "A texture that can be used to give the fog an emission value and to manipulate the fog color in lit and shadowed areas. Typically used for stylized effects."
                );

            public static Param ColorRampInfluence = new Param(
                "_ColorRampInfluence",
                "Color Ramp Influence",
                "Controls the amount of influence the color ramp exerts over the base physical results. A value of 0 means the Color Ramp is completely ignored. A value of 1 means that we exclusively refer to the Color Ramp."
                );

            public static Param NoiseTexture = new Param(
                "_NoiseTexture",
                "Texture",
                "A 3D Texture that will be used to define the fog intensity. Repeats over the noise tiling domain. A value of 0 means the fog density is attenuated to 0. A value of 1 means the fog density is not attenuated and matches what is set in the Fog Density parameter."
                );

            public static Param Octaves = new Param(
                "_Octaves",
                "Octaves",
                "Progressively resamples the noise texture over smaller tiling domains to increase the level of detail in the final fog presentation. More octaves are more computationally expensive but increase the level of realism."
                );

            public static Param Lacunarity = new Param(
                "_Lacunarity",
                "Lacunarity",
                "Amount by which the texture sampling frequency will increase with each successive octave."
                );

            public static Param Gain = new Param(
                "_Gain",
                "Gain",
                "Amount by which the texture amplitude will decrease with each successive octave."
                );

            public static Param NoiseTiling = new Param(
                "_NoiseTiling",
                "Tiling Domain (m)",
                "The length of each side of the cube that describes the rate at which the noise texture will repeat. In other words, the scale of the noise texture in meters."
                );

            public static Param NoiseWindSpeed = new Param(
                "_NoiseWindSpeed",
                "Wind Velocity (m/s)",
                "The rate at which the noise will be offset over time. Use this to simulate wind. Measured in meters per second."
                );

            public static Param NoiseIntensityMin = new Param(
                "_NoiseIntensityMin",
                "Noise Intensity Min",
                "Remaps the noise input texture from the original [0, 1] range to a new range defined by [NoiseIntensityMin, NoiseIntensityMax]. For example, an mapping of [0.2, 0.8] will remap the noise by clipping any values below 0.2 and any values above 0.8, then remapping the remaining 0.2 to 0.8 range back to 0.0 to 1.0 to retain detail."
                );

            public static Param NoiseIntensityMax = new Param(
                "_NoiseIntensityMax",
                "Noise Intensity Max",
                "Remaps the noise input texture from the original [0, 1] range to a new range defined by [NoiseIntensityMin, NoiseIntensityMax]. For example, an mapping of [0.2, 0.8] will remap the noise by clipping any values below 0.2 and any values above 0.8, then remapping the remaining 0.2 to 0.8 range back to 0.0 to 1.0 to retain detail."
                );
		}
    }
}
