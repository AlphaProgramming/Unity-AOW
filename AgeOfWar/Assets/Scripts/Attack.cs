using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 5f;
    public float attackDamage = 10f;
   

    public void AttackOpponent()
    {
        int i = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if(i==0) 
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                i++;
            }
        }
    }
    public void AttackOpponent(float crit)
    {
        int i = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (i == 0)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage+crit);
                i++;
            }
        }
    }
    public void AttackAlly()
    {
        int i = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (i == 0)
            {
                enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                i++;
            }
        }
    }
    public void AttackAlly(float crit)
    {
        int i = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (i == 0)
            {
                enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage + crit);
                i++;
            }
        }
    }

    /// <summary>
    /// elle permet de créer un cercle devant le personnage au lancement du script si celui-ci n'est pas déjà créé.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
