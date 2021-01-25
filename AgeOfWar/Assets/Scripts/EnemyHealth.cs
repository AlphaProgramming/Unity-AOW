using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    public float currentHealth;
    private Animator animator;
    private bool isDead;
    public GameObject TextDamage;
    private BoxCollider2D[] triggers;
    private Enemy enemy;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        triggers = GetComponentsInChildren<BoxCollider2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        Vector2 pos = transform.position;
        pos.y = 8;
        
        if (enemy.shield)
        {
            damage -= damage;
            animator.SetTrigger("ShieldBlock");
        }
        else
        {
            GameObject blood = Instantiate(particle, pos, Quaternion.identity);
            currentHealth -= damage;
            MakeTextDamage(damage);
            Destroy(blood, 0.6f);
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        animator.SetBool("dead", isDead);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        DisableTriggers();
        Invoke("StopAnimator", 2.1f);
        Destroy(gameObject, 2.5f);
        this.enabled = false;

    }
    private void StopAnimator()
    {
        animator.enabled = false;
    }

    private void MakeTextDamage(float damage)
    {
        GameObject tempTextBox = (GameObject)Instantiate(TextDamage, transform.position, Quaternion.identity);
        TextMesh theText = tempTextBox.transform.GetComponent<TextMesh>();
        if (damage < 33)
        {
            theText.color = Color.white;
            theText.text = "-" + damage.ToString();
        }
        else if (damage > 33 && damage < 66)
        {
            theText.fontSize = 245;
            theText.color = Color.red;
            theText.text = "-" + damage.ToString();
        }
        else
        {
            theText.fontSize = 285;
            theText.color = Color.yellow;
            theText.text = "-" + damage.ToString();
        }
        Destroy(tempTextBox,0.5f);
        Destroy(theText,0.5f);
    }
    private void DisableTriggers()
    {
        foreach (BoxCollider2D col in triggers)
        {
            col.enabled = false;
        }
    }
}
