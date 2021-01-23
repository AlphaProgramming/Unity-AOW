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
    private float attackRate = 0.5f;
    public BowmanAttack bowmanAttack;
    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;


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
            nextAttackTime = Time.time + 1f / attackRate;
            animator.SetBool("shotArrow", true);
            StartCoroutine("Shoot");

        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.54f);
        GameObject newArrow = Instantiate(arrow, shotPoint.position, Quaternion.Euler(0f,0f,-90f));
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}
