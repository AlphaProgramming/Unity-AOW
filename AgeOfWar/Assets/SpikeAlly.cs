using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAlly : MonoBehaviour
{
    public BoxCollider2D bc;
    private Attack enemy;
    public int damage;

    void Start()
    {
        enemy = GetComponent<Attack>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("dqzd");
        if (collision.gameObject.layer == 8) // Layer de l'enemy
        {
            enemy.AttackOpponent(damage);
        }
    }
}
