﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMan : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 6f;
    private Animator animator;
    public bool canMove;
    public bool canAttack;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    public GameObject mageBall;
    private Transform shotPoint;
    public float launchForce;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        shotPoint = transform.Find("ShotPoint");
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
            FireMageAttack();
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
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "LeftE") // il se remet à marcher dès qu'il trigger pas de collider d'un enemy
        {
            canAttack = false;
            canMove = true;
        }
    }

    public void FireMageAttack()
    {
        int i = 2;
        animator.SetBool("MageShot", false);
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            animator.SetBool("MageShot", true);
            if (i % 2 == 0)
            {
                StartCoroutine("Shoot");
                nextAttackTime += 0.8f;
            }
            else
            {
                StartCoroutine("Shoot");
            }
            i++;
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1.15f);
        GameObject newBall = Instantiate(mageBall, shotPoint.position, Quaternion.Euler(0f, 0f, 0f));
        newBall.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}