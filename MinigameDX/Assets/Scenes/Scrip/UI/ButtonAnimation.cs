using UnityEngine;
using DG.Tweening;
public class ButtonAnimation : MonoBehaviour
{
    public void OnHoverEnter()
    {
        transform.DOScale(1.1f, 0.2f);
    }

    public void OnHoverExit()
    {
        transform.DOScale(1f, 0.2f);
    }

}
