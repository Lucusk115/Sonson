using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float fireRate;
    public Rigidbody2D projectile;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public bool shootLeft;
    public GameObject target = null;
    float timeSinceLastFire = 0;
    float distanceCheck;
    public float range;
    private LevelManager scoreManager;
    public int health;
    public int itemValue;
    Animator anim;

    public AudioSource aSource;
    public AudioClip enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scoreManager = FindObjectOfType<LevelManager>();

        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }

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
            distanceCheck = Vector2.Distance(transform.position, target.transform.position);
            if (distanceCheck <= range)
            {
                timeSinceLastFire += Time.deltaTime;
                if (timeSinceLastFire >= fireRate)
                {
                    timeSinceLastFire = 0;
                    fire();
                }
            }
        }
    }

    void fire()
    {
        shootDirectionCheck();
        if (shootLeft)
        {
            print("left");
            Instantiate(projectile, leftSpawnPoint.position, leftSpawnPoint.rotation);
            //Instantiate(projectile, leftSpawnPoint);
        }
        else
        {
            print("right");
            Instantiate(projectile, rightSpawnPoint.position, rightSpawnPoint.rotation);
        }
    }
    void shootDirectionCheck()
    {
        if (target.transform.position.x < transform.position.x)
        {
            shootLeft = true;
        }
        else
        {
            shootLeft = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            SoundManager.instance.PlaySingleSound(enemy, 2.0f);
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
