using DG.Tweening;
using System.Collections;
using UnityEngine;

class SwipeInterpolation: MonoBehaviour
{
    [SerializeField, Tooltip("time(s) to return to original position when mouse button is up")]
    float duration;

    float elapsedTime;

    public IEnumerator MoveToOriginalPosition(Vector3 initialPos, Vector3 finalPos)
    {
        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Vector3 newPos = Vector3.Lerp(initialPos, finalPos, elapsedTime / duration);

            transform.position = newPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = finalPos;
    }
}