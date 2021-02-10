using System.Collections;
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
    public GameObject storm;
    public GameObject mageRange;
    public Transform stormPoint;
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
            MageAttack();
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

    void OnTriggerEnter2D(Collider2D collision)
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

    public void MageAttack()
    {
        int i = 2;
        var number = Random.Range(1, 5);
        string anim, attack = "";
        if (number > 3)
        {
            anim = "MageStorm";
            attack = "Spike";
        }
        else
        {
            anim = "MageShot";//sinon MageShot
            attack = "Shoot";
        }

        animator.SetBool(anim, false);
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            animator.SetBool(anim, true);
            if (i % 2 == 0)
            {
                StartCoroutine(attack);
                nextAttackTime += 0.8f;
            }
            else
            {
                StartCoroutine(attack);
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

    IEnumerator Spike()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(storm, stormPoint.position, Quaternion.Euler(-90f, 0f, 0f));
    }
}
