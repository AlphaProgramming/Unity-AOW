﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunmanAlly : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 6f;
    private Animator animator;
    public bool canMove;
    public bool canAttack;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    private Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        shotPoint = transform.Find("ShotPoint");
    }

    private void FixedUpdate()
    {
        animator.SetFloat("velocity", rb.velocity.x); //animation qui s'adapte avec la vitesse de marche du perso

        if (canMove)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y); // avance jusqu'au prochain ennemie/allié
        }
        else if (canAttack && !canMove)
        {
            GunAttack();
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
        //else if (collision.gameObject.transform.tag == "Left") // il s'arrête de marcher 
        //{
        //    canMove = false;
        //    canAttack = false;
        //}

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "LeftE") // il se remet à marcher dès qu'il trigger pas de collider d'un enemy
        {
            canAttack = false;
            canMove = true;
        }
        //else if (collision.gameObject.transform.tag == "Left") // il s'arrête d'attaquer et marque
        //{
        //    canMove = true;
        //}
    }

    private void GunAttack()
    {
        int i = 2;
        animator.SetTrigger("shotGun");
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            if (i % 2 == 0 && i %3 != 0)
            {
                animator.SetTrigger("shotMainHand");
            }
            else if(i%3==0)
            {
                animator.SetTrigger("shotBothHands");
            }
            else
            {
                animator.SetTrigger("shotOffHand");
            }
            nextAttackTime += 0.8f;
            i++;

        }
    }


}