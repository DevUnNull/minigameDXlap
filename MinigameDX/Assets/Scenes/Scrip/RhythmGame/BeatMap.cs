using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rhythm Game/Beat Map")]
public class BeatMap : ScriptableObject
{
    public AudioClip music;
    public float bpm = 120f;

    [Header("Notes")]
    public List<NoteData> notes = new List<NoteData>();
}
