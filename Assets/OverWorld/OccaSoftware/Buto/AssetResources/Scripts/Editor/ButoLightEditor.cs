using UnityEngine;
using UnityEditor;

namespace OccaSoftware.Buto.Editor
{
    [CustomEditor(typeof(ButoLight))]
    [CanEditMultipleObjects]
    public class ButoLightEditor : UnityEditor.Editor
    {
        ButoLight _target;

        private static class Props
        {
            public static SerializedProperty inheritDataFromLightComponent;
            public static SerializedProperty _lightColor;
            public static SerializedProperty _lightIntensity;
            public static SerializedProperty lightComponent;
        }

        private void OnEnable()
        {
            Props.inheritDataFromLightComponent = serializedObject.FindProperty(nameof(Props.inheritDataFromLightComponent));
            Props._lightColor = serializedObject.FindProperty(nameof(Props._lightColor));
            Props._lightIntensity = serializedObject.FindProperty(nameof(Props._lightIntensity));
            Props.lightComponent = serializedObject.FindProperty(nameof(Props.lightComponent));

            _target = (ButoLight)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            

            if(_target.LightCount > 8)
            {
                GUIStyle style = EditorStyles.wordWrappedLabel;
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.yellow;
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("WARNING \nThere are too many enabled Buto Lights in scene. Some lights will not be rendered with Buto. Remove or disable some lights to avoid undefined behavior.", style);
                EditorGUILayout.Space();
            }
                
            EditorGUILayout.PropertyField(Props.inheritDataFromLightComponent);
            if (Props.inheritDataFromLightComponent.boolValue)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(Props.lightComponent, new GUIContent("Light"));
                if(Props.lightComponent.objectReferenceValue != null)
                {
                    Light light = (Light)Props.lightComponent.objectReferenceValue;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ColorField("Color", light.color);
                    EditorGUILayout.FloatField("Intensity", light.intensity);
                    EditorGUI.indentLevel--;
                }

                EditorGUI.EndDisabledGroup();
            }
            if (!Props.inheritDataFromLightComponent.boolValue || Props.lightComponent.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(Props._lightColor, new GUIContent("Color"));
                EditorGUILayout.PropertyField(Props._lightIntensity, new GUIContent("Intensity"));
            }

            if(Props.inheritDataFromLightComponent.boolValue && Props.lightComponent.objectReferenceValue == null)
            {
                EditorGUILayout.Space();
                if(GUILayout.Button("Check or Add Light Component"))
                {
                    ForceGetLight();
                    _target.CheckForLight();
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void ForceGetLight()
        {
            if(!_target.TryGetComponent(out Light l))
            {
                l = Undo.AddComponent<Light>(_target.gameObject);
                ButoEditorCommon.SetupLight(l, Props._lightIntensity.floatValue, Props._lightIntensity.colorValue);
            }
        }

    }
}
