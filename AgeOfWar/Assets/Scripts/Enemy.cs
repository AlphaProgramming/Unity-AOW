using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Animator animator;
    public bool canMove;
    public bool canAttack;
    public Attack attack;
    private float nextAttackTime;
    private float attackRate = 0.9f;



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
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("velocity", characterVelocity);

        if (canMove)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y); // avance jusqu'au prochain ennemie/allié
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
            rb.velocity = new Vector2(-velocity, rb.velocity.y);// trigger la box collider d'un allié
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Right") // il s'arrête de marcher et attaque
        {
            canAttack = true;
            canMove = false;
        }
        else if (collision.gameObject.transform.tag == "RightE") // il s'arrête de marcher
        {
            canMove = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Right") // il se remet à marcher dès qu'il trigger pas de collider d'un enemy
        {
            canAttack = false;
            canMove = true;
        }
        else if (collision.gameObject.transform.tag == "RightE") // il s'arrête d'attaquer et marque
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
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH1");
                    break;
                case 2:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH2");
                    break;
                case 3:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH3");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

}
