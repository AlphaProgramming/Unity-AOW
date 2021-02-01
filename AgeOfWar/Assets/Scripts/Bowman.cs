using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 6f;
    private Animator animator;
    public bool canMove;
    public bool canAttack;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    public GameObject arrow;
    public float launchForce;
    private Transform shotPoint;

    // Start is called before the first frame update
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
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("velocity", characterVelocity);
        if (canMove)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y); // avance jusqu'au prochain ennemie/allié
        }
        else if (canAttack && !canMove)
        {
            BowAttack();
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
    private void BowAttack()
    {
        int i = 2;
        animator.SetBool("shotArrow", false);
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            animator.SetBool("shotArrow", true);
            if (gameObject.layer == 9)
            {
                if (i % 2 == 0)
                {
                    StartCoroutine("ShootAlly");
                    nextAttackTime += 0.8f;
                }
                else
                {
                    StartCoroutine("ShootAlly");
                }
            }
            else if (gameObject.layer == 8)
            {
                if (i % 2 == 0)
                {
                    StartCoroutine("ShootEnemy");
                    nextAttackTime += 0.8f;
                }
                else
                {
                    StartCoroutine("ShootEnemy");
                }
            }
            
            i++;

        }
    }

    IEnumerator ShootAlly()
    {
        yield return new WaitForSeconds(1f);
        GameObject newArrow = Instantiate(arrow, shotPoint.position, Quaternion.Euler(0f, 0f, -90f));
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
    IEnumerator ShootEnemy()
    {
        yield return new WaitForSeconds(1f);
        GameObject newArrow = Instantiate(arrow, shotPoint.position, Quaternion.Euler(0f, 0f, -270f));
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

}
