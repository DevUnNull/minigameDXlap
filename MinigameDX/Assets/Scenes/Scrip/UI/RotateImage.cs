using DG.Tweening;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    RectTransform rt;
    Tween rotateTween;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void StartAnimationEnter()
    {
        // Tránh tạo tween chồng
        if (rotateTween != null && rotateTween.IsActive())
            rotateTween.Kill();

        rotateTween = rt.DORotate(
            new Vector3(0, 0, 360),
            1.5f,
            RotateMode.FastBeyond360
        )
        .SetLoops(-1)
        .SetEase(Ease.Linear);
    }

    public void Exit()
    {
        if (rotateTween != null)
        {
            rotateTween.Kill();   // dừng ngay
            rotateTween = null;
        }

        // Optional: reset rotation
        rt.rotation = Quaternion.identity;
    }
}
