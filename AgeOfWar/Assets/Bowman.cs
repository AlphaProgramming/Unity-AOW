using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Animator animator;
    private bool canMove;
    private bool canAttack;
    private float nextAttackTime;
    private float attackRate = 1f;
    public BowmanAttack bowmanAttack;


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
        animator.SetFloat("velocity", rb.velocity.x); //animation qui s'adapte avec la vitesse de marche du perso

        if (canMove)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y); // avance jusqu'au prochain ennemie/allié
        }
        else if (canAttack && !canMove)
        {
            Attack();
            rb.velocity = new Vector2(0f, rb.velocity.y); // trigger la box collider de l'ennemie
        }
        else if (!canAttack && !canMove)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);// trigger la box collider d'un allié
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Enemy") // il s'arrête de marcher et attaque
        {
            canAttack = true;
            canMove = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Enemy") // il se remet à marcher dès qu'il trigger pas de collider d'un enemy
        {
            canAttack = false;
            canMove = true;
        }
    }
    private void Attack()
    {
        animator.SetBool("shotArrow", false);
        if (Time.time >= nextAttackTime)
        {
            animator.SetBool("shotArrow", true);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

}
