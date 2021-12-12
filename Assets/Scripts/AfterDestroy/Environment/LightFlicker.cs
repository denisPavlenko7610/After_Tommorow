using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light _light;
    [SerializeField] float _intensity;
    [SerializeField] float _frequency;

    void Update()
    {
        _light.intensity = Mathf.PerlinNoise(Time.time * _frequency , 0f) * _intensity;
    }
}
