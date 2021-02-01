using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBallAlly : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject impactEffect;
    public CircleCollider2D bc;
    private Rigidbody2D rb;
    private Attack enemy;
    public int damage;

    void Start()
    {
        enemy = GetComponent<Attack>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // Layer de l'enemy
        {
            enemy.AttackOpponent(damage);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(this.bc);
            Destroy(this.gameObject);
            Destroy(impactEffect);
        }        
    }
}
