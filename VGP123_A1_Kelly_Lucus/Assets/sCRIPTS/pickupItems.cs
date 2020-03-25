using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItems : MonoBehaviour  
{
    private LevelManager scoreManager;
    public int itemValue;
    public AudioSource aSource;
    public AudioClip item;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<LevelManager>();

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
        
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySingleSound(item, 2.0f);
            scoreManager.addItems(itemValue);
            Destroy(this.gameObject);
            
        }

    }
}
