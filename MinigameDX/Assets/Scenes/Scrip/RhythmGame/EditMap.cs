using UnityEngine;
using UnityEngine.InputSystem;

public class EditMap : MonoBehaviour
{
    public GameObject notePrefab;

    // khi bấn chuột sẽ spawn ngẫu nghiên tại 2 vị trí x 
    public float leftX = -2f;
    public float rightX = 2f;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnNoteAtRandomPosition();
        }
    }
    private void SpawnNoteAtRandomPosition()
    {
        if (notePrefab == null) return;

        float spawnX = Random.value < 0.5f ? leftX : rightX;
        Vector3 spawnPos = new Vector3(spawnX, 6f, 0); 
        Instantiate(notePrefab, spawnPos, Quaternion.identity);
    }
}

