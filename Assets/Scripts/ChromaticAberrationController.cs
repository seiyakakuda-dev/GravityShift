using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticAberrationController : MonoBehaviour
{
    public static ChromaticAberrationController Instance { get; private set; }

    private Volume volume;
    private ChromaticAberration chromaticAberration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        volume = GetComponent<Volume>();
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out chromaticAberration);
        }
    }

    /// <summary>
    /// 色収差を一瞬強く演出する
    /// </summary>
    public void TriggerEffect(float peakIntensity = 1.0f, float duration = 0.25f)
    {
        if (chromaticAberration == null) return;

        StopAllCoroutines();
        StartCoroutine(DoEffect(peakIntensity, duration));
    }

    private IEnumerator DoEffect(float peakIntensity, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            chromaticAberration.intensity.value = Mathf.Lerp(peakIntensity, 0f, t);
            yield return null;
        }

        chromaticAberration.intensity.value = 0f;
    }
}