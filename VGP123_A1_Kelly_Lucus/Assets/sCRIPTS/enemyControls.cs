using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControls : MonoBehaviour
{
    public int health;
    public Rigidbody2D rb;
    public bool facingRight;
    public float speed;

    private LevelManager scoreManager;
    public int itemValue;

    Animator anim;

    public AudioSource aSource;
    public AudioClip enemy;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();

        if (!aSource)
        {
            aSource = gameObject.AddComponent<AudioSource>();
            aSource.loop = false;
            aSource.playOnAwake = false;
        }


        if (health <= 0)
        {
            health = 1;
        }

        if(speed == 0)
        {
            speed = 1;
        }

        anim = GetComponent<Animator>();

        if (!anim)
        {
            Debug.LogError("Animator not found on " + name);
        }

        rb = GetComponent<Rigidbody2D>();

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
        if (facingRight)
        {
            rb.velocity = new Vector2(speed, 0);

        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
    void flip()
    {
        facingRight = !facingRight;

        //flip the game object on the x axis
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
          
           health--;
           anim.SetTrigger("StoneGuyDeath");
           SoundManager.instance.PlaySingleSound(enemy, 2.0f);
            
            if (health <= 0)
            {
                scoreManager.addItems(itemValue); 
                print("projectile problem");
                
                Destroy(this.gameObject);
                
            }
           
        }
        else if (collision.gameObject.tag != "Ground")
        {
            flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyObstacleBarrier")
        {
            flip();
        }

     
    }
}
