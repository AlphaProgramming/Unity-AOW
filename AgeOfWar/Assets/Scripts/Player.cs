using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("velocity", characterVelocity);
    }
}
