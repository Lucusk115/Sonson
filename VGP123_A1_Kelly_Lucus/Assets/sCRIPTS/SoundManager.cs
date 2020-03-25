using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance = null;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySingleSound(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.Play();
    }

    public static SoundManager instance
    {
        get { return _instance; }   
        set { _instance = value; }  
    }
}
