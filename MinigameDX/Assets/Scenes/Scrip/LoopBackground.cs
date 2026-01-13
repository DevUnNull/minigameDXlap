using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    public float speed = 2f;
    public float resetY = -10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= resetY)
        {
            transform.position = startPos;
        }
    }
}
