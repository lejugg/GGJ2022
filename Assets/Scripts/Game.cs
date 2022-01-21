using DG.Tweening;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Vector3.back, 1f);
    }

}
