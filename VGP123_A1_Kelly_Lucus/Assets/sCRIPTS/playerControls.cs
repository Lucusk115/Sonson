using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    public AudioSource aSource;

    public AudioClip gun;
    public AudioClip jump;
    public AudioClip death;
    public AudioClip awesomeness;

    //Bullet variables controls prefab, spawnpoint, and force
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;
    public float bulletForce;

    //show and reference Rigidbody2D in inspector
    Rigidbody2D rb;
    public Rigidbody2D rb2;

    // Player movement speed
    public float speed;
    public float health;

    //Player jump controls
    public float jumpForce;
    public bool isGrounded;
    

    //character checks for ground, colliders and radius
    public LayerMask isGroundlayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    //animation controls for character
    public bool isFacingLeft = false;
    Animator anim;

    public Vector3 respawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;

        if (!bulletSpawnPoint)
            Debug.LogError("Missing bulletSpawn");

        if (!bulletPrefab)
            Debug.LogError("Missing bulletPrefab");

        if (bulletForce <= 0)
        {
            bulletForce = 5.0f;
            Debug.LogError("Missing bulletForce. Defaulting to " + bulletForce);
        }

        rb = GetComponent<Rigidbody2D>();

        // Check if Component exists
        if (!rb) // or if(rb == null)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Rigidbody2D not found on " + name);
        }

        if (!rb2)
        {
            Debug.LogError("Rigidbody2D not found on " + name);
        }

        if (speed <= 0) // check that variable been set 
        {
            speed = 8.0f; //default speed value
            Debug.LogError("Speed is not set on " + name + "Defaulting to " + speed);
        }

        if (jumpForce <= 0) // check that variable been set 
        {
            jumpForce = 5.5f; //sets a default jumpForce value
            Debug.LogError("jumpForce is not set on " + name + "Defaulting to " + jumpForce);
        }

        if (!groundCheck)
        {
            Debug.LogError("groundCheck not found on " + name);
        }

        if (groundCheckRadius <= 0) // check that variable been set 
        {
            groundCheckRadius = 0.2f; //sets a default jumpForce value
            Debug.LogError("groundCheckRadius not set on " + name + "Defaulting to " + groundCheckRadius);
        }

        anim = GetComponent<Animator>();

        if (!anim)
        {
            Debug.LogError("Animator not found on " + name);
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
        // check if left or right has been pressed
        float moveValue = Input.GetAxisRaw("Horizontal");

        //check if 'groundCheck' game object is touching a collider on the Ground Layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, isGroundlayer);

        anim.SetBool("grounded", isGrounded);

        //check if jump is pressed
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            print("Jump");
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SoundManager.instance.PlaySingleSound(jump, 2.0f);
        }

        if (Input.GetButtonDown("Fire1")) //check if left control was pressed
        {
            anim.SetTrigger("Fire1");
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Bullet Fired");
                fire();
            }
        }

        //move character left and right
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        //tell animator to move to the next animation clip
        anim.SetFloat("speed", Mathf.Abs(moveValue));
        anim.SetBool("grounded", isGrounded);

        //flipping the animation
        if ((moveValue < 0 && !isFacingLeft) || (moveValue > 0 && isFacingLeft))
        {
            flip();
        }


        //drop through platforms
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine( dropThrough());
         }
    }

    IEnumerator dropThrough()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

   
    
    /*private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
        {
            Jumper();
            Invoke("Jumper", 0.5f);
        }
    }
        void Jumper()
        {
            gameObject.GetComponent<Collider2D>().enabled =
                !gameObject.GetComponent<Collider2D>().enabled;
        }*/
    
    void fire()
    {
        Bullet b = Instantiate(bulletPrefab, bulletSpawnPoint.position,
            bulletSpawnPoint.rotation);
        
        //fire when facing left or right
        if (isFacingLeft)
        {
            b.speed = -bulletForce;
        }
        else
        {
            b.speed = bulletForce;
        }
        print(SoundManager.instance);
        SoundManager.instance.PlaySingleSound(gun, 2.0f);
    }

    void flip()
    {
        //flip the game object for the player on its x axis
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;

        //flip the FacingLeft Boolean
        isFacingLeft = !isFacingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyProjectile")
        {
            health--;
            SoundManager.instance.PlaySingleSound(death, 2.0f);

            anim.SetTrigger("sonsonDeath");
            //GameManager.instance.restart();
            if (health <= 0)
            {
                Destroy(this.gameObject);
                print("Player dies"); 
                SoundManager.instance.musicSource.clip = awesomeness;
                SoundManager.instance.musicSource.Play();
                GameManager.instance.endGame();
               
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            //Player enters the FallDetector zone
            transform.position = respawnPoint;
        }

        if(other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}
