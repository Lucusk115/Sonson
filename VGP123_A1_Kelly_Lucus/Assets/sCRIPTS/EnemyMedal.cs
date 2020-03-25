using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMedal : MonoBehaviour
{
    public int health;
    Animator anim;
    private LevelManager scoreManager;
    public int itemValue;

    public AudioSource aSource;
    public AudioClip enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
           
            health--;
            SoundManager.instance.PlaySingleSound(enemy, 2.0f);
            if (health < 4)
            {
                anim.SetTrigger("halfLife");
                print("projectile problem");

            }
            if (health <= 0)
            {
                Destroy(this.gameObject);
                scoreManager.addItems(itemValue);
            }
        }
    }
}
