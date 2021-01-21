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
    public Collider2D trigger;
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
        Vector2 pos = transform.position;
        pos.y = 15;
        GameObject tempTextBox = (GameObject)Instantiate(TextDamage, pos, Quaternion.identity);
        TextMesh theText = tempTextBox.transform.GetComponent<TextMesh>();


        Color color1 = new Color(96, 0, 0);
        Color color2 = new Color(255, 96, 0);
        Color color3 = new Color(255, 0, 0);
        if (damage < 33)
        {
            theText.color = color1;
            theText.text = damage.ToString();
        }
        else if (damage > 33 && damage < 66)
        {
            theText.color = color2;
            theText.text = damage.ToString();
        }
        else
        {
            theText.color = color3;
            theText.text = damage.ToString();
        }
    }
}
