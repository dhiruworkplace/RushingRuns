using UnityEngine;
using DG.Tweening;

public class BalonControl : MonoBehaviour
{
    // 1 = RED  2 = GREEN 3 = BLUE
    public int color;

    private void Start() 
    {
        transform.DOLocalMoveY(transform.localPosition.y + -0.15f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}