using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlaySound(Sounds.Background);
    }

    public void PlaySound(Sounds type)
    {
        Sound s = Array.Find(sounds, sound => sound.type == type);
        s.source.Play();
    }
    public void StopSound(Sounds type)
    {
        Sound s = Array.Find(sounds, sound => sound.type == type);
        s.source.Stop();
    }
}
public enum Sounds
{
    explosion,
    Background,
    Playerdeath,
    EndGoal,
    shoot,
    enemyHit,
    playerHit,
    battleSound
}
