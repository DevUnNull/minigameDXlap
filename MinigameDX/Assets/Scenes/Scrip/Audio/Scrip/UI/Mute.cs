using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mute : MonoBehaviour
{
    public Button button;
    public Sprite spriteA;
    public Sprite spriteB;

    private bool isA = true;
    private bool isAnimating = false;

    public void Swap()
    {
        if (isAnimating) return; // tránh spam click
        isAnimating = true;

        // Kill tween cũ nếu có
        button.transform.DOKill();

        // Animation
        button.transform
            .DOScale(0.8f, 0.15f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                // Swap sprite ở giữa animation
                button.image.sprite = isA ? spriteB : spriteA;
                isA = !isA;

                // Phóng to + xoay nhẹ
                button.transform
                    .DOScale(1f, 0.15f)
                    .SetEase(Ease.OutBack);

                button.transform
                    .DORotate(new Vector3(0, 0, 10f), 0.15f)
                    .From(Vector3.zero)
                    .OnComplete(() =>
                    {
                        button.transform.rotation = Quaternion.identity;
                        isAnimating = false;
                    });
            });
    }
}
