using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Tooltip("Tham chiếu đến PlayerController cha để gọi hàm tính điểm")]
    public PlayerController playerController;

    private void Start()
    {
        // Tự động tìm PlayerController ở cha nếu chưa gán
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Chỉ xử lý khi va chạm với Obstacle
        if (other.CompareTag("do") || other.CompareTag("xanh"))
        {
            // 🔹 So sánh tag của Player và Obstacle
            if (other.tag == gameObject.tag)
            {
                // ✅ Cùng tag -> + điểm
                Debug.Log("Cùng tag! + Score");
                Destroy(other.gameObject);
                GameManager.Instance.AddScore(1);
            }
            else
            {
                // ❌ Khác tag -> Miss / trừ điểm
                Debug.Log("Khác tag! Miss");
                GameManager.Instance.UnAddScore(0);
                GameManager.Instance.AddMiss(1);
            }
        }
        // ScoreZone (giữ nguyên)
        else if (other.CompareTag("ScoreZone"))
        {
            if (playerController != null)
            {
                playerController.TriggerScore();
            }
        }
    }
}
