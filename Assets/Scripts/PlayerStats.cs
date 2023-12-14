using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    int maxHealth = 100;
    int maxStamina = 70;

    [SerializeField] Slider healthSlider;
    [SerializeField] Slider staminaSlider;
    [SerializeField] GameObject deathMessage;


    [SerializeField] float health;
    [SerializeField] float stamina;

    float regSpeed = 0.5f;



    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
        //StartCoroutine(Regenerate());
        deathMessage.SetActive(false);
    }

    
    void Update()
    {
        if (health < maxHealth) { health += regSpeed*Time.deltaTime; }
        if (stamina < maxStamina) { stamina += regSpeed * Time.deltaTime; }
        //Debug.Log("health: " + health + " mana: " + mana);
        //text.text = "health: " + health + " mana: " + mana;
        healthSlider.value = health;
        staminaSlider.value = stamina;

        if (health <= 0) { deathMessage.SetActive(true); }

    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        GetComponent<AudioSource>().Play();
    }

    public void UseStamina(float cost)
    {
        stamina -= cost;
    }
}
