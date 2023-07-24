using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStutter : MonoBehaviour
{
    Light light;
    const float fpsExpression = 0.0167f;
    float randomTimer;
    const float minimumInterval = .3f;
    const int maximumInterval = 1; 
    const int minimumIntensity = 1;
    const int MaximumIntensity = 5;
    float randomIntensity; 
    Coroutine stutterRoutine; 
    private void Awake()
    {
        light = GetComponent<Light>();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        stutterRoutine = StartCoroutine(stutterEffect());
    }

    IEnumerator stutterEffect()
    {
        while (true)
        {
            randomTimer = UnityEngine.Random.Range(minimumInterval, maximumInterval);
            randomIntensity = UnityEngine.Random.Range(minimumIntensity, MaximumIntensity); 
            while (randomTimer > 0)
            {
                randomTimer -= fpsExpression; 
                yield return null;
            }
            light.intensity = randomIntensity;
            yield return null;
        }
    }

}
