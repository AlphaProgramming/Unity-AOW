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
    public bool canKick;
    private float nextAttackTime;
    private float attackRate = 0.9f;
    public GameObject arrow;
    public float launchForce;
    private Transform shotPoint;
    private BoxCollider2D shotRange;
    private BoxCollider2D kickRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        shotPoint = transform.Find("ShotPoint");
        shotRange = transform.Find("BowmanRange").GetComponent<BoxCollider2D>();
        kickRange = transform.Find("Right").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("velocity", characterVelocity);
        if (canMove) // avance jusqu'au prochain ennemie/allié
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        else if (canKick)//trigger la box collider d'un ennemi au cac
        {
            Kick();
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (canAttack)//trigger la box collider d'un ennemi en étant derrière un allié
        {
            BowAttack();
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (!canAttack && !canMove && !canKick)// trigger la box collider d'un allié (sans qu'il y ait d'ennemi)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (shotRange.CompareTag("LeftE") && kickRange.CompareTag("Left"))
        {
            canAttack = true;
            canKick = false;
            canMove = false;
        }
        else if (kickRange.CompareTag("LeftE"))
        {
            canAttack = false;
            canKick = true;
            canMove = false;
        }
        else if (kickRange.CompareTag("Left"))
        {
            canMove = false;
            canAttack = false;
            canKick = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (shotRange.CompareTag("LeftE"))
        {
            canAttack = false;
        }
        else if (kickRange.CompareTag("Left"))
        {
            canMove = true;
        }
        else if (kickRange.CompareTag("LeftE"))
        {
            canMove = true;
            canKick = false;
        }
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

    private void Kick()
    {

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
