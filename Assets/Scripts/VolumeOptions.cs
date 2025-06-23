using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    private MusicController music;

    void Start()
    {
        music = FindObjectOfType<MusicController>();
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = savedVolume;
        ApplyVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(ApplyVolume);
    }

    void ApplyVolume(float volume)
    {
        if (music != null)
            music.SetVolume(volume);

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
