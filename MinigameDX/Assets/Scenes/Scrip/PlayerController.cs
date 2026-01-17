using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float swapDuration = 0.3f; // Thời gian để swap (animation)

    [Header("Children References")]
    private Transform child1;
    private Transform child2;

    [Header("State")]
    private bool isSwapping = false;
    private float swapProgress = 0f;
    private Vector3 child1StartPos;
    private Vector3 child2StartPos;
    private float lastScoreTime = -1f;

    private void Start()
    {
        // Lấy 2 đối tượng con
        if (transform.childCount >= 2)
        {
            child1 = transform.GetChild(0);
            child2 = transform.GetChild(1);
        }
        else
        {
            Debug.LogError("PlayerController cần ít nhất 2 đối tượng con để swap!");
        }
    }

    private void Update()
    {
        // Handle Input (Support New Input System)
        bool inputDetected = false;

        // Check Mouse
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            inputDetected = true;
        }
        // Check Touchscreen
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            inputDetected = true;
        }
        // Optional: Check Keyboard (Space)
        else if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            inputDetected = true;
        }

        if (inputDetected && !isSwapping)
        {
            StartSwap();
        }

        // Update swap animation
        if (isSwapping)
        {
            UpdateSwap();
        }
    }

    private void StartSwap()
    {
        if (child1 == null || child2 == null) return;

        isSwapping = true;
        swapProgress = 0f;
        
        // Lưu vị trí ban đầu
        child1StartPos = child1.localPosition;
        child2StartPos = child2.localPosition;
    }

    private void UpdateSwap()
    {
        swapProgress += Time.deltaTime / swapDuration;

        if (swapProgress >= 1f)
        {
            // Hoàn thành swap
            swapProgress = 1f;
            isSwapping = false;
            
            // Set vị trí cuối cùng chính xác
            child1.localPosition = child2StartPos;
            child2.localPosition = child1StartPos;
        }
        else
        {
            // Lerp giữa 2 vị trí với easing
            float t = EaseInOutQuad(swapProgress);
            child1.localPosition = Vector3.Lerp(child1StartPos, child2StartPos, t);
            child2.localPosition = Vector3.Lerp(child2StartPos, child1StartPos, t);
        }
    }

    // Hàm easing để animation mượt hơn
    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
    }

    // Logic va chạm đã được chuyển sang script PlayerCollision.cs gắn trên từng phần tử con
    public void TriggerScore()
    {
        if (GameManager.Instance != null)
        {
            if (Time.time - lastScoreTime >= 0.5f)
            {
                GameManager.Instance.AddScore(1);
                lastScoreTime = Time.time;
            }
        }
    }
}