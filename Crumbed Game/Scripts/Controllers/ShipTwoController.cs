using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipTwoController : MonoBehaviour, IPointerDownHandler
{
    public float speedMax = 5f;
    Rigidbody2D rb2d;
    SpriteRenderer sprRender;
    bool winScore = false;
    bool lostScore = false;
    Animator anim;
    public AudioSource hitSound;
    public float clickInterval = 1f;
    private float lastClick = -1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.up = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb2d.velocity = transform.up * speedMax;
        anim.SetBool("Amassar", true);
    }

    void Update()
    {
        if(Time.time >= lastClick + clickInterval)
        {
            lastClick = -1f;
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = rb2d.velocity.normalized * speedMax;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            hitSound.Play();

            transform.up = Vector2.Reflect(transform.up, other.contacts[0].normal);

            rb2d.velocity = transform.up * speedMax;

            anim.SetBool("Amassar", true);
        }
        else
        {
            transform.up = rb2d.velocity.normalized;
            rb2d.velocity = transform.up * speedMax;
        }
        if (other.collider.CompareTag("Wall") && winScore)
        {
            lostScore = true;
            if (lostScore)
            {
                FindObjectOfType<LevelManager>().LossPoints();
                winScore = false;
                //hitSound.Play();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (lastClick < 0)
        {
            lastClick = Time.time;
        }
        else
        {
            print("Double");
            if (winScore == false)
            {
                hitSound.Play();
                FindObjectOfType<LevelManager>().WinPoints();
                winScore = true;
                lostScore = false;
                anim.SetBool("Amassar", false);
            }
        }
    }
}