using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Tooltip("Tham chiếu đến script ColorAttributes của chính object này")]
    public ColorAttributes myAttributes;

    [Tooltip("Tham chiếu đến PlayerController cha để gọi hàm tính điểm")]
    public PlayerController playerController;

    private void Start()
    {
        if (myAttributes == null)
        {
            myAttributes = GetComponent<ColorAttributes>();
        }
        
        // Tự động tìm PlayerController ở cha nếu chưa gán
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra va chạm với Vật cản
        if (other.CompareTag("Obstacle"))
        {
            ColorAttributes otherAttributes = other.GetComponent<ColorAttributes>();
            
            if (otherAttributes != null)
            {
                // Kiểm tra màu (dựa trên TypeA/TypeB)
                if (myAttributes.colorType == otherAttributes.colorType)
                {
                    // Cùng màu -> Đi qua (Pass)
                    Debug.Log("Cùng màu! Pass.");
                    // xóa Obstacle đi 
                    Destroy(other.gameObject);
                }
                else
                {
                    // Khác màu -> Thua
                    Debug.Log("Khác màu! Game Over.");
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.GameOver();
                        
                        // Disable player control
                        if (playerController != null)
                        {
                            playerController.enabled = false;
                        }
                    }
                }
            }
            else
            {
                // Nếu vật cản không có script màu => Xử lý như vật cản thường (Thua)
                // Hoặc bỏ qua. Ở đây ta để mặc định là Thua cho an toàn.
                 if (GameManager.Instance != null)
                 {
                     GameManager.Instance.GameOver();
                 }
            }
        }
        // Kiểm tra ScoreZone
        else if (other.CompareTag("ScoreZone"))
        {
            if (playerController != null)
            {
                playerController.TriggerScore();
            }
        }
    }
}
