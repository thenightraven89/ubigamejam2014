using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] clips;

    public AudioSource source;

    void Awake()
    {
        instance = this;
    }

    public void Play(int wave)
    {
        source.PlayOneShot(clips[wave % clips.Length]);
    }
}