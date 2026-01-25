// AudioDatabase (ScriptableObject)
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    public List<SceneMusic> sceneMusics;
    public List<AudioClip> sfxClips;

    public AudioClip GetMusicByScene(string sceneName)
    {
        foreach (var item in sceneMusics)
        {
            if (item.sceneName == sceneName)
                return item.backgroundMusic;
        }
        return null;
    }
}
