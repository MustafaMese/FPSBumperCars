using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float rangeX = 1f;
    [SerializeField] float rangeY = 1f;
    [SerializeField] float rangeZ = 1f;
    [SerializeField] float duration;
    [SerializeField] float magnitude;

    Vector3 originalPos;

    public void CameraShake()
    {
        StartCoroutine(Shake(duration, magnitude));
        originalPos = transform.localPosition;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-rangeX, rangeX) * magnitude;
            float y = Random.Range(-rangeY, rangeY) * magnitude;
            float z = Random.Range(-rangeZ, rangeZ) * magnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
