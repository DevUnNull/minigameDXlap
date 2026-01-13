using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float destroyY = -6f; // Tọa độ Y để hủy object (thấp hơn đáy màn hình)

    private void Update()
    {
        // Di chuyển xuống dưới
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Kiểm tra hủy
//        if (transform.position.y < destroyY)
//        {
//            Destroy(gameObject);
//        }
    }

    // Hàm tiện ích để thiết lập vị trí Gap (Khe hở)
    // Gọi hàm này nếu muốn chỉnh vị trí khe hở bằng cách di chuyển 2 thanh con trái/phải
    // Hoặc đơn giản là di chuyển cả Object này qua trái/phải nếu thanh ngang đủ dài.
    public void RandomizeGapPosition(float minX, float maxX)
    {
        // Cách đơn giản nhất: Di chuyển cả object Obstacle theo trục X
        float randomX = Random.Range(minX, maxX);
        Vector3 newPos = transform.position;
        newPos.x = randomX;
        transform.position = newPos;
    }
}
