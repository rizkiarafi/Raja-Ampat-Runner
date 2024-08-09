using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 1.5f;
    public float shakeMagnitude = 0.3f;
    private Vector3 originalPosition;
    private AudioSource rockfallAudioSource;

    void Awake()
    {
        if (GetComponent<Camera>() != null)
        {
            originalPosition = transform.localPosition;
        }

        rockfallAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsed = 0.0f;

        if (!rockfallAudioSource.isPlaying)
        {
            rockfallAudioSource.Play();
        }

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
