using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public AudioSource sfxSource;
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

        // Obtener o crear AudioSource
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    public void PlayBrushSound()
    {
        if (brushSound == null)
        {
            Debug.LogWarning("SoundManager: brushSound no está asignado!");
            return;
        }
        
        if (sfxSource == null)
        {
            Debug.LogWarning("SoundManager: sfxSource no está asignado!");
            return;
        }
        
        sfxSource.PlayOneShot(brushSound);
        Debug.Log("SoundManager: Reproduciendo sonido de cepillo");
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
