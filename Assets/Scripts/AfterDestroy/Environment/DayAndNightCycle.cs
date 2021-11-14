using UnityEngine;

namespace AfterDestroy.Environment
{
    public class DayAndNightCycle : MonoBehaviour
    {
        [SerializeField] private Light sun;
        [SerializeField] private float fullDaySeconds = 120f;
        [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0;
        [SerializeField] private Color dayColor;
        [SerializeField] private Color nightColor;
        [SerializeField] private Color sunsetColor;

        private float _timeMultiplier = 1f;
        private float _sunInitialIntensity = 1.5f;
        private Color _color;

        private void OnValidate()
        {
            if (sun == null)
            {
                sun = FindObjectOfType<Light>();
            }
        }

        void Update()
        {
            UpdateSun();

            currentTimeOfDay += (Time.deltaTime / fullDaySeconds) * _timeMultiplier;

            if (currentTimeOfDay >= 1)
            {
                currentTimeOfDay = 0;
            }
        }

        private void UpdateSun()
        {
            sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

            float intensityMultiplier = 1;
            _color = dayColor;

            if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75)
            {
                intensityMultiplier = 0;
                _color = nightColor;
            }
            else if (currentTimeOfDay <= 0.25f)
            {
                _color = sunsetColor;
                intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (currentTimeOfDay >= 0.73f)
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
                _color = sunsetColor;
            }

            RenderSettings.ambientLight = _color;
            RenderSettings.ambientIntensity = intensityMultiplier;
            RenderSettings.reflectionIntensity = intensityMultiplier;
            RenderSettings.skybox.color = _color;
            sun.intensity = _sunInitialIntensity * intensityMultiplier;
        }
    }
}