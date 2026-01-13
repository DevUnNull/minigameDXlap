using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject obstaclePrefab; // Prefab chướng ngại vật
    public float spawnRate = 2f;      // Khoảng thời gian giữa các lần spawn (giây)
    public float spawnY = 6f;         // Tọa độ Y spawn (phía trên màn hình)
    
    [Header("Gap Randomization")]
    public float minX = -2f; // Giới hạn trái của khe hở
    public float maxX = 2f;  // Giới hạn phải của khe hở

    private float timer;

    private void Start()
    {
        timer = spawnRate;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObstacle();
            timer = spawnRate;
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        // Tạo object tại vị trí mặc định (spawnY)
        // X sẽ được set ngẫu nhiên
        Vector3 spawnPos = new Vector3(0, spawnY, 0);
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Random vị trí khe hở
        // Cách 1: Gọi hàm trong script Obstacle (nếu có logic phức tạp)
        Obstacle obstacleScript = newObstacle.GetComponent<Obstacle>();
        if (obstacleScript != null)
        {
            obstacleScript.RandomizeGapPosition(minX, maxX);
        }
        else
        {
            // Cách 2: Tự di chuyển object nếu script Obstacle không xử lý
            float randomX = Random.Range(minX, maxX);
            Vector3 pos = newObstacle.transform.position;
            pos.x = randomX;
            newObstacle.transform.position = pos;
        }

        // Random Màu (TypeA/TypeB) cho Obstacle
        ColorAttributes colorAttr = newObstacle.GetComponent<ColorAttributes>();
        if (colorAttr != null)
        {
            // Random: 50/50 cơ hội nhận ColorA hoặc ColorB
            ColorType randomType = (Random.value > 0.5f) ? ColorType.ColorA : ColorType.ColorB;
            colorAttr.SetColorType(randomType);
            
            // Debug.Log("Spawned Obstacle with Color: " + randomType);
        }
        else
        {
            Debug.LogWarning("Obstacle Prefab chưa có script ColorAttributes! Vui lòng gắn script này vào Prefab.");
        }
    }
}
