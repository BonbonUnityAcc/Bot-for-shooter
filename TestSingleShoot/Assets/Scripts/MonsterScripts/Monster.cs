using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;

    int curHealth;
    public bool takingDamage = false;

    private void Start()
    {
        curHealth = maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
            Destroy(gameObject);
        Debug.Log(gameObject.name + " осталось здоровья " + curHealth);
        takingDamage = true;
        StartCoroutine(Wait());
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        takingDamage = false;
    }
}
