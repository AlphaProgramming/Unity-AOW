using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Animator animator;
    private bool canMove;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (canMove)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("velocity", characterVelocity);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player")
        {
            canMove = false;
            //Attack();
        }
        else
        {
            canMove = true;
        }
    }

}
