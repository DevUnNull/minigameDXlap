using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class EditMap : MonoBehaviour
{
    [Header("Danh sách prefab note")]
    public List<GameObject> notePrefabs = new List<GameObject>();

    [Header("Danh sách node X")]
    public List<float> spawnXList = new List<float>();

    public float spawnY = 6f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnNoteRandom();
        }
    }

    private void SpawnNoteRandom()
    {
        if (notePrefabs.Count == 0 || spawnXList.Count == 0)
        {
            Debug.LogWarning("Danh sách prefab hoặc node đang rỗng!");
            return;
        }

        float spawnX = spawnXList[Random.Range(0, spawnXList.Count)];
        GameObject note = notePrefabs[Random.Range(0, notePrefabs.Count)];

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(note, spawnPos, Quaternion.identity);
    }
}
