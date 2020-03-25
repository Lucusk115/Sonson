using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform target;
    public float stoppingDistance;
    public float health;
    bool facingRight = true;

    private LevelManager scoreManager;
    public int itemValue;
    Animator anim;

    public AudioSource aSource;
    public AudioClip enemy;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        if (target)
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards
                    (transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        { 
            SoundManager.instance.PlaySingleSound(enemy, 2.0f);
            health--;
           
            //anim.SetTrigger("LightGreenGuy_Death");
            if (health <= 0)
            {

                print("projectile problem");
                scoreManager.addItems(itemValue);
                Destroy(this.gameObject);

            }

        }
    }
}
