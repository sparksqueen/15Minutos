using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource soundEffectSource;
    public AudioClip brushSound;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Create AudioSource if it doesn't exist
        if (soundEffectSource == null)
        {
            soundEffectSource = gameObject.AddComponent<AudioSource>();
            soundEffectSource.playOnAwake = false;
        }
    }

    public void PlayBrushSound()
    {
        if (brushSound != null && soundEffectSource != null)
        {
            soundEffectSource.PlayOneShot(brushSound);
        }
    }
}
