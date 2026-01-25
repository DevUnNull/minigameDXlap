// AudioManager (CORE)
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioDatabase database;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 1f;
        sfxSource.volume = 1f;

        // Xóa dữ liệu cũ và reset
        PlayerPrefs.DeleteKey("Music");
        PlayerPrefs.DeleteKey("SFX");
        AudioSettings.MusicEnabled = true;
        AudioSettings.SfxEnabled = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }

    public void PlaySceneMusic(string sceneName)
    {
        Debug.Log($"[AudioManager] PlaySceneMusic called for scene: {sceneName}");
        Debug.Log($"[AudioManager] Database assigned: {database != null}");
        Debug.Log($"[AudioManager] MusicEnabled: {AudioSettings.MusicEnabled}");

        if (database == null)
        {
            Debug.LogError("[AudioManager] ERROR: AudioDatabase not assigned in Inspector!");
            return;
        }

        AudioClip clip = database.GetMusicByScene(sceneName);
        if (clip == null)
        {
            Debug.LogWarning($"[AudioManager] No music found for scene: {sceneName}");
            return;
        }

        Debug.Log($"[AudioManager] Found clip: {clip.name}");

        if (musicSource.clip == clip)
        {
            Debug.Log("[AudioManager] Same clip already set, skipping");
            return;
        }

        musicSource.clip = clip;
        Debug.Log($"[AudioManager] Clip set to: {clip.name}");

        if (AudioSettings.MusicEnabled)
        {
            musicSource.Play();
            Debug.Log("[AudioManager] Music playing");
        }
        else
        {
            Debug.Log("[AudioManager] MusicEnabled is false, not playing");
        }
    }


    public void PlaySFX(AudioClip clip)
    {
        if (!AudioSettings.SfxEnabled) return;
        sfxSource.PlayOneShot(clip);
    }

    public void ToggleMusic()
    {
        AudioSettings.MusicEnabled = !AudioSettings.MusicEnabled;

        if (AudioSettings.MusicEnabled)
        {
            if (musicSource.clip != null)
                musicSource.Play();
        }
        else
        {
            musicSource.Pause();
        }
    }



    public void ToggleSFX(bool enable)
    {
        AudioSettings.SfxEnabled = enable;
    }
}
