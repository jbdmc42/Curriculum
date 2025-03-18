using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public int lifes = 3;
    public Slider slider;
    public Gradient gradient;
    public Image healthFill;
    public TextMeshProUGUI healthText;
    public AudioManager audioManager;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        CurrentHealth(maxHealth);
        MaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth(currentHealth);

        if (currentHealth <= 0f)
        {
            if (isDead != true)
            {
                audioManager.Play("die");
            }
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    private void MaxHealth()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        healthFill.color = gradient.Evaluate(1f);
    }
    public void CurrentHealth(float health)
    {
        if (health <= 0)
        {
            health = 0;
        }

        slider.value = health;
        currentHealth = health;

        healthFill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.SetText(health + "");
    }
    public void TakeDamagePlayer(float damage)
    {
        currentHealth -= damage;
    }
    public bool IsDead
    {
        get { return isDead; }
    }
}
