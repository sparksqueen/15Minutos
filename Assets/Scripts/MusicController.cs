using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public float normalPitch = 1f;
    public float maxPitch = 1.5f;
    public float timeToMaxPitch = 900f; // 15 minutos

    private float timer = 0f;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // la música no se corta entre escenas
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

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
