using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Animator animator;
    public bool canMove;
    public bool canAttack;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    public bool shield;

    private Attack attack;


    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        shield = false;
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
        float randint = Random.Range(1, 3);
        if (Time.time >= nextAttackTime)
        {
            switch (randint)
            {
                case 1:
                    shield = true;
                    animator.SetTrigger("Shield");
                    Invoke("SetShield", 1f);
                    break;
                case 2:
                    attack.AttackOpponent();
                    animator.SetTrigger("AttackMH1");
                    break;
                case 3:
                    attack.AttackOpponent(10);
                    animator.SetTrigger("AttackMH2");
                    break;
                case 4:
                    attack.AttackOpponent(10);
                    animator.SetTrigger("AttackMH3");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void SetShield()
    {
        shield = false;
    }
}
