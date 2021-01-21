using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity = 5f;
    public Animator animator;
    public bool canMove;
    public bool canAttack;
    private float nextAttackTime;
    private float attackRate = 0.9f;

    public Attack attack;


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
        else if (!canAttack && canMove)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);// untrigger la box collider d'un allié
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player") // il s'arrête de marcher
        {
            canMove = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "LeftE") // il s'arrête de marcher et attaque
        {
            canAttack = true;
            canMove = false;
        }
        else if (collision.gameObject.transform.tag == "Left") // il s'arrête de marcher 
        {
            canMove = false;
            canAttack = false;
        }
    }
        void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "LeftE") // il se remet à marcher dès qu'il trigger pas de collider d'un enemy
        {
            canAttack = false;
            canMove = true;
        }
        else if (collision.gameObject.transform.tag == "Left") // il s'arrête d'attaquer et marque
        {
            canMove = true;
        }
    }
    private void Attack()
    {
        float randint = Random.Range(1, 4);
        if (Time.time >= nextAttackTime)
        {
            switch (randint)
            {
                case 1:
                    attack.AttackOpponent();
                    animator.SetTrigger("AttackBH1");
                    break;
                case 2:
                    attack.AttackOpponent();
                    animator.SetTrigger("AttackBH2");
                    break;
                case 3:
                    attack.AttackOpponent();
                    animator.SetTrigger("AttackBH3");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

}

