using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    float startY;
    float targetY;
    float fallTime;
    float timer;

    public void SetFall(float fallTime, float targetY)
    {
        this.fallTime = fallTime;
        this.targetY = targetY;
        startY = transform.position.y;
        timer = 0f;
    }

    void Update()
    {
        if (timer >= fallTime) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fallTime);
        float y = Mathf.Lerp(startY, targetY, t);

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
