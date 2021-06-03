using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipOneController : MonoBehaviour, IPointerDownHandler
{
    public float speedMax = 5f;
    int colldownTime;
    Rigidbody2D rb2d;
    SpriteRenderer sprRender;
    bool winScore = false;
    bool lostScore = false;
    Animator anim;
    public AudioSource hitSound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.up = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb2d.velocity = transform.up * speedMax;
        anim.SetBool("Amassar", true);
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
        if (winScore == false)
        {
            Debug.Log(eventData.clickCount);

            hitSound.Play();
            FindObjectOfType<LevelManager>().WinPoints();
            winScore = true;
            lostScore = false;
            anim.SetBool("Amassar", false);
        }
        else
        {
            Debug.Log(eventData.clickCount);

            eventData.clickCount = 1;
        }
    }
}