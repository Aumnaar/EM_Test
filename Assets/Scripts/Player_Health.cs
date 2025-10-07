using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    [SerializeField] public Rigidbody2D rb;
    public bool manaShield;

    public GameObject player;
    public Slider healthSlider;
    public Slider secondSlider;
    public float lerpSpeed = 0.05f;

    public float maxHealth = 80;
    public float currentHealth;

    public float maxStagger = 100;
    public float currentStagger;

    public bool isAlive = true;
    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentStagger = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;

        }

        if (healthSlider.value != secondSlider.value)
        {
            secondSlider.value = Mathf.Lerp(secondSlider.value, currentHealth, lerpSpeed);
        }

        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }


    public void TakeDamage(int damage)
    {
        if (isAlive && !isInvincible)
        {
            currentHealth -= damage;
            SetHealth(currentHealth);
            isInvincible = true;

        }

    }

    public void TakeHeal(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;

            SetHealth(currentHealth);
        }
    }



    void SetHealth(float health)
    {
        healthSlider.value = health;

    }

    private void Die()
    {

        SceneManager.LoadScene(0);

    }


}
