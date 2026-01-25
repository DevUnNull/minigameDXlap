// Lưu trạng thái bật / tắt nhạc (PlayerPrefs)
using UnityEngine;

public static class AudioSettings
{
    public static bool MusicEnabled
    {
        get => PlayerPrefs.GetInt("Music", 1) == 1;
        set => PlayerPrefs.SetInt("Music", value ? 1 : 0);
    }

    public static bool SfxEnabled
    {
        get => PlayerPrefs.GetInt("SFX", 1) == 1;
        set => PlayerPrefs.SetInt("SFX", value ? 1 : 0);
    }
}
