using System;
using UnityEngine;

[Serializable]
public class NoteData
{
    [Tooltip("Thời điểm note xuất hiện (giây, theo AudioSource.time)")]
    public float time;

    [Tooltip("Lane index")]
    public int laneIndex;

    [Tooltip("Số beat để rơi xuống target")]
    public float beatsToFall = 1f;
}
