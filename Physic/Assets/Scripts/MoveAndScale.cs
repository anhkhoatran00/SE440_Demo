using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Vector3.one * 3, 2f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.InQuart).OnComplete(() =>
        {
            Debug.Log("mlem");
        });
        transform.DOScale(Vector3.one * 2, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InQuad);
    }


}
