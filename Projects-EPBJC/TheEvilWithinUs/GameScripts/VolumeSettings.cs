using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider UISlider;
    [SerializeField] private Slider SFXSlider;


    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        gameMixer.SetFloat("MASTER", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        gameMixer.SetFloat("MUSIC", Mathf.Log10(volume)*20);
    }

    public void SetUIVolume()
    {
        float volume = UISlider.value;
        gameMixer.SetFloat("UI", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        gameMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }
}
