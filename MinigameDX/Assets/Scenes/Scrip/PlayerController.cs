using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 100f;
    public Transform rotationCenter; // Tâm xoay (nếu null sẽ dùng transform.position)

    [Header("State")]
    private float currentSpeed;
    private int direction = -1; // -1: Clockwise (Cùng chiều kim đồng hồ vì z-axis rotation), 1: Counter-Clockwise
    private float lastScoreTime = -1f;

    // Note: In Unity 2D, negative Z rotation is clockwise.

    private void Start()
    {
        currentSpeed = rotationSpeed;
        if (rotationCenter == null)
        {
            rotationCenter = transform;
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

        if (inputDetected)
        {
            ChangeDirection();
        }

        // Apply Rotation
        // Rotate around the center point.
        // If this script is on the "Container" of the dots and the Container is at the center, we just rotate transform.
        // User prompt: "Có một tâm điểm cố định, 2 điểm tròn (Player Dots) sẽ xoay quanh tâm này"
        
        transform.Rotate(0, 0, direction * currentSpeed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        direction *= -1;
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
