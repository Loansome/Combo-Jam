using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds; // creates array of all the sounds

    public static AudioManager instance;

    void Awake()
    {
        /*if (instance == null) // keeps the same audio manager across all scenes of the game
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }*/
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) // creates audio sources for every sound so they can play, and carries over certain attributes made from the Sound class script that are checkmarked in scene
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() // play a sound when first loading in, typically music
    {
        //Play("Scroll 1");
    }

    public void Play(string name) // makes audio playable from other scripts
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Pause();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }
    public bool isPlaying(string name) // checks if audio is currently playing
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return true;
        }
        return s.source.isPlaying;
    }
    public double length(string name) // checks how long the audio is
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return 0;
        }
        return s.clip.length;
    }
}
