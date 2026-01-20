using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitionAnimator : MonoBehaviour
{
    [Header("UI Shrink Animation")]
    [SerializeField] private ShrinkAndRestoreDOTween uiShrinkAnimation;

    [Header("Transition Image (UI)")]
    [SerializeField] private bool useTransitionImage = true;
    [SerializeField] private bool coverScreenFirst = false;
    [SerializeField] private RectTransform transitionImage;

    [Header("Positions (SET IN INSPECTOR)")]
    [SerializeField] private Vector2 startAnchoredPos;
    [SerializeField] private Vector2 targetAnchoredPos;

    [Header("Wave Animation")]
    [SerializeField] private float moveDuration = 0.9f;
    [SerializeField] private float waveOvershoot = 80f;
    [SerializeField] private float waveElasticity = 0.35f;
    [SerializeField] private Ease firstMoveEase = Ease.InOutSine;

    [Header("Timing")]
    [SerializeField] private float totalTransitionTime = 1.0f;

    private bool isPlaying;

    // ================= PUBLIC =================

    /// <summary>
    /// Chạy animation chuyển cảnh (AN TOÀN CHO UI + LAYOUT)
    /// </summary>
    public void PlayTransition(Action onComplete = null)
    {
        if (!gameObject.activeInHierarchy || isPlaying) return;
        StartCoroutine(PlaySafeRoutine(onComplete));
    }

    /// <summary>
    /// Gọi ở scene mới nếu coverScreenFirst = true
    /// </summary>
    public void RevealScene()
    {
        if (!useTransitionImage || !coverScreenFirst || transitionImage == null)
            return;

        transitionImage.DOKill();
        transitionImage
            .DOAnchorPos(startAnchoredPos, moveDuration)
            .SetEase(Ease.InOutSine);
    }

    // ================= CORE =================

    private IEnumerator PlaySafeRoutine(Action onComplete)
    {
        isPlaying = true;

        // 1️⃣ Thu nhỏ UI hiện tại
        if (uiShrinkAnimation != null)
            uiShrinkAnimation.ShrinkAll();

        // 2️⃣ ĐỢI UI + LAYOUT + CANVAS update XONG
        yield return new WaitForEndOfFrame();

        if (useTransitionImage && transitionImage != null && !coverScreenFirst)
        {
            // 🔒 ÉP rebuild layout cha (nếu có)
            RectTransform parentRect = transitionImage.parent as RectTransform;
            if (parentRect != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);

            // 🔥 ÉP vị trí start trước khi tween
            transitionImage.anchoredPosition = startAnchoredPos;

            PlayWaveImage();
        }

        // 3️⃣ Chờ animation hoàn tất
        yield return new WaitForSeconds(totalTransitionTime);

        isPlaying = false;
        onComplete?.Invoke();
    }

    // ================= IMAGE =================

    private void PlayWaveImage()
    {
        transitionImage.DOKill();

        float firstMoveTime = moveDuration * 0.65f;
        float bounceTime = moveDuration * 0.35f;

        Sequence seq = DOTween.Sequence();

        // 🌊 Sóng đánh – đi quá vị trí
        seq.Append(
            transitionImage.DOAnchorPos(
                targetAnchoredPos + Vector2.down * waveOvershoot,
                firstMoveTime
            ).SetEase(firstMoveEase)
        );

        // 🌊 Dật ngược lại
        seq.Append(
            transitionImage.DOAnchorPos(
                targetAnchoredPos,
                bounceTime
            ).SetEase(Ease.OutElastic, 1f, waveElasticity)
        );

        seq.Play();
    }
}
