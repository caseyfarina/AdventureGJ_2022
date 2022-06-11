using System.Collections.Generic;
using UnityEngine;

namespace OccaSoftware.Buto
{
    [ExecuteAlways]
    public class ButoLight : MonoBehaviour
    {
        public int LightCount
        {
            get
            {
                return Lights.Count;
            }
        }
        [SerializeField] private bool inheritDataFromLightComponent = false;
        [SerializeField] private Light lightComponent = null;

        [SerializeField] [ColorUsage(false, false)] private Color _lightColor = Color.white;
        public Vector4 LightColor
        {
            get 
            {
                if (inheritDataFromLightComponent && lightComponent != null)
                    return lightComponent.color;

                return _lightColor; 
            }
        }

        [SerializeField] [Min(0)] private float _lightIntensity = 10f;
        public float LightIntensity
        {
            get 
            {
                if (inheritDataFromLightComponent && lightComponent != null)
                    return lightComponent.intensity;

                return _lightIntensity; 
            }
        }

        private static List<ButoLight> _Lights = new List<ButoLight>();
        public static List<ButoLight> Lights
        {
            get { return _Lights; }
        }

        private void Reset()
        {
            ButoCommon.CheckLightCount(Lights.Count, ButoFogComponent._MAXLIGHTS, this);
        }

        private void OnValidate()
        {
            lightComponent = GetComponent<Light>(); 
        }

        private void OnEnable()
        {
            lightComponent = GetComponent<Light>();
            _Lights.Add(this);
        }

        private void OnDisable()
        {
            _Lights.Remove(this);
        }

        public void CheckForLight()
        {
            lightComponent = GetComponent<Light>();
        }

        public void SetInheritance(bool state)
        {
            inheritDataFromLightComponent = state;
        }
    }

}


