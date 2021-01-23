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
    private Attack attack;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    public bool shield = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        attack = GetComponent<Attack>();
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
            string tagname = gameObject.transform.tag;

            switch (tagname)
            {
                case "Sword":
                    SwordAttack();
                    break;
                case "Shield":
                    ShieldAttack();
                    break;
                case "Second":
                    SecondAttack();
                    break;
                case "Player":
                    Attack();
                    break;
            }
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
        else if (collision.gameObject.transform.tag == "RightE") // il se remet à marcher pour suivre son coop
        {
            canMove = true;
        }
    }

    private void Attack()
    {
        float randint = Random.Range(1, 5);
        if (Time.time >= nextAttackTime)
        {
            switch (randint)
            {
                case 1:
                    attack.AttackAlly();
                    animator.SetTrigger("punch");
                    break;
                case 2:
                    attack.AttackAlly();
                    animator.SetTrigger("punch2");
                    break;
                case 3:
                    attack.AttackAlly(2);
                    animator.SetTrigger("kick");
                    break;
                case 4:
                    attack.AttackAlly(2);
                    animator.SetTrigger("kick2");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void ShieldAttack()
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
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH1");
                    break;
                case 3:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH2");
                    break;
                case 4:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackMH3");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void SecondAttack()
    {
        float randint = Random.Range(1, 4);
        if (Time.time >= nextAttackTime)
        {
            switch (randint)
            {
                case 1:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackBH1");
                    break;
                case 2:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackBH2");
                    break;
                case 3:
                    attack.AttackAlly();
                    animator.SetTrigger("AttackBH3");
                    break;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void SwordAttack()
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

    private void SetShield()
    {
        shield = false;
    }

}
