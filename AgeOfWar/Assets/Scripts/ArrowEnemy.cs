using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    PolygonCollider2D bc;
    private Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<PolygonCollider2D>();
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHit == false)
        {
            Vector2 direction = rb.velocity;
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        }
        else
        {
            attack.AttackAlly();
            Destroy(this.bc);
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
