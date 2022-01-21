using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Button test;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(UnityEngine.Vector3.back, 1f);
        test.OnClickAsObservable().Subscribe(OnClick);
    }

    private void OnClick(Unit unit)
    {
        throw new System.NotImplementedException();
    }
}
