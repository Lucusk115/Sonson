using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemy : MonoBehaviour
{
    //[SerializeField]
    public float moveSpeed = 5f;

    //[SerializeField]
    public float frequency = 15f;

    //[SerializeField]
    public float magnitude = 0.5f;

    public bool facingRight = true;
    public int health;

    Vector3 pos, localScale;

    private LevelManager scoreManager;
    public int itemValue;

    public AudioSource aSource;
    public AudioClip enemy;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        pos = transform.position;
        localScale = transform.localScale;
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
        //flip();
        CheckWhereToFace();

        if (facingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    //void flip()
    //{
    //    facingRight = !facingRight;

    //    //flip the game object on the x axis
    //    Vector3 scaleFactor = transform.localScale;
    //    scaleFactor.x *= -1;
    //    transform.localScale = scaleFactor;
    //}
    void CheckWhereToFace()
    {
        if (pos.x < -7f)
        {
            facingRight = true;
        }
        else if (pos.x > 7f)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.instance.PlaySingleSound(enemy, 2.0f);
            health--;
            if (health <= 0)
            {
                scoreManager.addItems(itemValue);
                print("score");

                Destroy(this.gameObject);

            }
        }
    }
}

