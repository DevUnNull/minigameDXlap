using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Timing")]
    public float bpm = 120f;

    [Header("Spawn")]
    public GameObject obstaclePrefab;
    public float spawnY = 6f;
    public float targetY = 0f;
    public float[] laneXPositions;
    public Transform container;

    public float BeatDuration => 60f / bpm;

    public void Spawn(int laneIndex, float beatsToFall)
    {
        if (obstaclePrefab == null) return;
        if (laneIndex < 0 || laneIndex >= laneXPositions.Length) return;

        Vector3 pos = new Vector3(laneXPositions[laneIndex], spawnY, 0f);
        GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, container);

        var move = obj.GetComponent<ObstacleMove>();
        if (move != null)
        {
            float fallTime = BeatDuration * beatsToFall;
            move.SetFall(fallTime, targetY);
        }
    }
}
