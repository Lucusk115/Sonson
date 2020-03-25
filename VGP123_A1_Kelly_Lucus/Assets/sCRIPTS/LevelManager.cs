using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public Text scoreText;
    public int itemScore;
    
    public Image[] livesDisplay;
    public bool paused = false;
    public GameObject PauseOverlay;

    public AudioSource aSource;
    public AudioClip awesomeness;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE " + itemScore;

        if (!aSource)
        {
            aSource = gameObject.AddComponent<AudioSource>();
            aSource.loop = false;
            aSource.playOnAwake = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;

            if (paused)
            {
                pauseStart();
                SoundManager.instance.musicSource.clip = awesomeness;
                SoundManager.instance.musicSource.Play();
            }
            else
            {
                pauseStop();
                Debug.LogError("Pause button not working");
            }
        }
    }

    private void pauseStart()
    {
        PauseOverlay.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void pauseStop()
    {
        PauseOverlay.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

public void addItems(int numberOfItems)
    {
        itemScore += numberOfItems;
        
        scoreText.text = "SCORE " + itemScore;

    }


}
