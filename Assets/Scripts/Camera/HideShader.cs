using System.Numerics;
using DG.Tweening;
using UnityEngine;

public class HideShader : MonoBehaviour
{

    [SerializeField] private float hideDistance = 10f;
    
    private void Awake()
    {
    }

    void Update()
    {
        var scale = 1f - Mathf.Clamp01(hideDistance - UnityEngine.Vector3.Distance(Camera.main.transform.position, transform.position));
        transform.DOScale(scale, 0.1f);
        transform.RotateAround(UnityEngine.Vector3.zero, UnityEngine.Vector3.down, Time.deltaTime * 10f);
    }

}
