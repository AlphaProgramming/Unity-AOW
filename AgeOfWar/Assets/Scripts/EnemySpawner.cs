using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public int level; //level 1 = 3 puisque 3 personnages par level donc pour le niveau 5 level doit être égal à 15
    IEnumerator Spawn()
    {
        int randomEnemy = Random.Range(0, level);
        int randomSecond = Random.Range(3, 15);
        Instantiate(enemies[randomEnemy], transform.position, Quaternion.Euler(0f, 180f, 0f)) ;
        yield return new WaitForSeconds(randomSecond);
        StartCoroutine("Spawn");
    }
    private void Start()
    {
        StartCoroutine("Spawn");
    }
}
