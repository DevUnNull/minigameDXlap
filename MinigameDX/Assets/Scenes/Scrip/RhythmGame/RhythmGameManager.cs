using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{
    public BeatMap beatMap;
    public SpawnManager spawnManager;
    public AudioSource audioSource;

    int nextNoteIndex = 0;

    void Start()
    {
        if (beatMap == null) return;

        spawnManager.bpm = beatMap.bpm;

        audioSource.clip = beatMap.music;
        audioSource.Play();
    }

    void Update()
    {
        if (beatMap == null) return;
        if (nextNoteIndex >= beatMap.notes.Count) return;

        float songTime = audioSource.time;
        NoteData note = beatMap.notes[nextNoteIndex];

        if (songTime >= note.time)
        {
            spawnManager.Spawn(note.laneIndex, note.beatsToFall);
            nextNoteIndex++;
        }
    }
}
