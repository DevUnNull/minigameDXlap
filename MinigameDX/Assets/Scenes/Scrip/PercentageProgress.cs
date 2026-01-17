using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PercentageProgress : MonoBehaviour
{
    [Header("References")]
    public Slider progressSlider; // set min=0, max=100 in Inspector (or we set it in Awake)
    public TextMeshProUGUI percentText;

    [Header("Settings")]
    public float duration = 3f; // nhập tay thời gian trong Inspector (giây)
    public bool playOnAwake = true;
    public bool pauseGameDuringProgress = false; // nếu true, Time.timeScale = 0 trong lúc chạy

    float originalTimeScale = 1f;
    Coroutine runningCoroutine;

    void Awake()
    {
        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = 100;
            progressSlider.value = 0;
        }

        UpdateText(0);
    }

    void Start()
    {
        if (playOnAwake) StartProgress();
    }

    public void StartProgress()
    {
        if (runningCoroutine != null) StopCoroutine(runningCoroutine);
        runningCoroutine = StartCoroutine(RunProgressCoroutine());
    }

    public void StopProgress()
    {
        //GameManager.Instance.WinGame();
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
            if (pauseGameDuringProgress) RestoreTimeScale();
        }
    }

    public void ResetProgress(bool toZero = true)
    {
        StopProgress();
        if (progressSlider != null) progressSlider.value = toZero ? 0 : progressSlider.maxValue;
        UpdateText(progressSlider != null ? progressSlider.value : 0);
    }

    IEnumerator RunProgressCoroutine()
    {
        if (pauseGameDuringProgress)
        {
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0f; // pause other game updates
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float delta = pauseGameDuringProgress ? Time.unscaledDeltaTime : Time.deltaTime;
            elapsed += delta;
            float t = Mathf.Clamp01(duration > 0 ? elapsed / duration : 1f);
            float percent = Mathf.Lerp(0f, 100f, t);
            if (progressSlider != null) progressSlider.value = percent;
            UpdateText(percent);
            yield return null;
        }

        // Ensure final value
        if (progressSlider != null) progressSlider.value = 100f;
        UpdateText(100f);

        if (pauseGameDuringProgress) RestoreTimeScale();

        // 🎯 GỌI WIN GAME KHI ĐẠT 100%
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelComplete();
        }

        runningCoroutine = null;
    }

    void RestoreTimeScale()
    {
        Time.timeScale = originalTimeScale;
    }

    void UpdateText(float percent)
    {
        if (percentText != null)
            percentText.text = $"{Mathf.RoundToInt(percent)}%";
    }
}