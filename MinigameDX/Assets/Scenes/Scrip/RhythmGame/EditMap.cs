using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditMap : MonoBehaviour
{
    public GameObject notePrefab_1;
    public GameObject notePrefab_2;
    // khi bấn chuột sẽ spawn ngẫu nghiên tại 2 vị trí x 
    public float leftX = -1.5f;
    public float rightX = 1.5f;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnNoteAtRandomPosition();
        }
    }
    private void SpawnNoteAtRandomPosition()
    {
        float spawnX = Random.value < 0.5f ? leftX : rightX;
        Vector3 spawnPos = new Vector3(spawnX, 6f, 0);
        GameObject note = Random.value <0.5f ? notePrefab_1 : notePrefab_2; 
        Instantiate(note, spawnPos, Quaternion.identity);
    }
    
}

