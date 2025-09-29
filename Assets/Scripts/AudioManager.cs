using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    // Singleton
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
            }
        }
    }

    public void PlaySoundSequence(string first, string next)
    {
        StartCoroutine(PlaySequenceRoutine(first, next));
    }

    private IEnumerator PlaySequenceRoutine(string first, string next)
    {
        Sound firstSound = GetSound(first);
        Sound nextSound = GetSound(next);

        if (firstSound == null || nextSound == null) yield break;

        firstSound.source.Play();

        // wait for clip length
        yield return new WaitForSeconds(firstSound.clip.length);

        nextSound.source.Play();
    }
    
    public Sound GetSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name) return s;
        }
        Debug.LogWarning("Sound not found: " + name);
        return null;
    }
}
