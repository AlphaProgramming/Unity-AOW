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
    public Collider2D left;
    public Collider2D right;
    public GameObject TextDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        MakeTextDamage(damage);
        Debug.Log(currentHealth);
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
        hitbox.enabled = false;
        right.enabled = false;
        left.enabled = false;
        Invoke("StopAnimator", 2.1f);
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
    }
}
