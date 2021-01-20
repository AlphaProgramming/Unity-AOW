using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    public float currentHealth;
    public Animator animator;
    private bool isDead;
    public Collider2D hitbox;
    public Collider2D trigger;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetBool("dead", true);
        Destroy(GetComponent<Rigidbody2D>());
        trigger.enabled = false;
        hitbox.enabled = false;
        Invoke("StopAnimator", 2.1f);
        this.enabled = false;
    }
    private void StopAnimator()
    {
        animator.enabled = false;
    }
}
