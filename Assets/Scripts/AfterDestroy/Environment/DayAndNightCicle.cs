using UnityEngine;

namespace AfterDestroy.Environment
{
    public class DayAndNightCicle : MonoBehaviour
    {
        [SerializeField] private Light sun;
        [SerializeField] private float fullDaySeconds = 120f;
        [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0;
        
        private float _timeMultiplier = 1f;
        private float _sunInitialIntensity = 1.5f;

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

            float intencityMultiplier = 1;

            if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75)
            {
                intencityMultiplier = 0;
            }
            else if (currentTimeOfDay <= 0.25f)
            {
                intencityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
            } else if (currentTimeOfDay >= 0.73f)
            {
                intencityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            }

            sun.intensity = _sunInitialIntensity * intencityMultiplier;
        }
    }
}