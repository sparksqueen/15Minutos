using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    public AudioSource musicSource;
    public float normalPitch = 1f;
    public float maxPitch = 1.5f;
    public float timeToMaxPitch = 900f;

    private float timer = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Se conserva entre escenas
    }

    void Start()
    {
        musicSource.pitch = normalPitch;
    }

    void Update()
    {
        if (timer < timeToMaxPitch)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / timeToMaxPitch);
            musicSource.pitch = Mathf.Lerp(normalPitch, maxPitch, t);
        }
    }

    public void ResetPitch()
    {
        timer = 0f;
        musicSource.pitch = normalPitch;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
