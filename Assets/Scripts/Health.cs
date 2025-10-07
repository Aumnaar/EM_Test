using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UI _ui;
    public Animator animator;
    public Transform player;
    public Rigidbody2D rb;


    [Header("Base parameters")]
    public int maxHealth = 100;
    public int currenthealth;

    [Header("Primary bool")]
    public bool isAlive = true;
    public bool isStunned = false;

    [Header("Secondary bool")]
    public bool isInvincible = false;
    public bool isHit = false;

    private float timeSinceHit = 0;

    public float invincibilityTime = 0.25f;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
    }

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
    }

       

      
    public void TakeDamage(int damage)
    {
        

        if (isAlive && !isInvincible) 
        {
            currenthealth -= damage;
            isInvincible = true;
            isHit = true;

        }
       

        if (currenthealth <= 0)
        {
           
            isAlive = false;
            Die();

        }
    }

    public void SetHealth (int health)
    {
        currenthealth += health;

        if (health > 100)
        {
            health = 100;
        }
    }


    private void Die()
    {
        if (!isAlive && CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            //Destroy(this.gameObject);
            Time.timeScale = 0f;
            _ui._panel.SetActive(true);


        }
        else if (!isAlive)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }
    }


}
