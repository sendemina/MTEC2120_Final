using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int maxHealth = 100;
    int maxMana = 70;

    [SerializeField] float health;
    [SerializeField] float mana;

    float regSpeed = 0.5f;



    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        //StartCoroutine(Regenerate());
    }

    
    void Update()
    {
        if (health < maxHealth) { health += regSpeed*Time.deltaTime; }
        if (mana < maxMana) { mana += regSpeed * Time.deltaTime; }
        //Debug.Log("health: " + health + " mana: " + mana);
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
    }

    public void UseMana(float cost)
    {
        mana -= cost;
    }
}
