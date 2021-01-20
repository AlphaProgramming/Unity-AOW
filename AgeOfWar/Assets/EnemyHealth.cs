using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    public float currentHealth;
    public Animator animator;
    private bool isDead;
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
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Invoke("StopAnimator", 2f);
        this.enabled = false;
    }
    private void StopAnimator()
    {
        animator.enabled = false;
    }
}
