using DG.Tweening;
using UnityEngine;

public class HideShader : MonoBehaviour
{

    [SerializeField] private float hideDistance = 10f;

    void Update()
    {
        var scale = 1f - Mathf.Clamp01(hideDistance - Vector3.Distance(Camera.main.transform.position, transform.position));
        transform.DOScale(scale, 0.1f);
        // transform.RotateAround(Vector3.zero, Vector3.down, Time.deltaTime * 10f);
    }
}