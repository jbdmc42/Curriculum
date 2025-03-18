using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] zombieSounds;
    [SerializeField] AudioMixer gameMixer;

    // Awake is called for the initialization
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach (Sound z in zombieSounds)
        {
            z.source = gameObject.AddComponent<AudioSource>();
            z.source.clip = z.clip;
            z.source.volume = z.volume;
            z.source.pitch = z.pitch;
            z.source.spatialBlend = 1;
            z.source.minDistance = 1;
            z.source.maxDistance = 15;
            z.source.rolloffMode = AudioRolloffMode.Linear;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound z = Array.Find(zombieSounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
        else if (z != null)
        {
            z.source.Play();
        }
        else
        {
            Debug.Log("Não tem esse clip");
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound z = Array.Find(zombieSounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else if (z != null)
        {
            z.source.Stop();
        }
        else
        {
            Debug.Log("Não tem esse clip");
        }
    }
}
