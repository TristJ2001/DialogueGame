using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager _instance;
    
    [SerializeField] private AudioClip[] audioClips;
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();
    
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        audioSource = GetComponent<AudioSource>();
        PopulateDictionary();
    }

    public void PlaySound(string name)
    {
        if (soundEffects.ContainsKey(name))
        {
            audioSource.PlayOneShot(soundEffects[name]);
        }
        else
        {
            Debug.LogWarning("Sound effect does not exist in SFX Manager");
        }
    }
    private void PopulateDictionary()
    {
        foreach (AudioClip clip in audioClips)
        {
            if (soundEffects.ContainsKey(clip.name))
            {
                Debug.LogWarning("A sound effect with the same name already exists. AudioClip Ignored");
            }
            else
            {
                soundEffects.Add(clip.name, clip);
            }
        }
    }
}
