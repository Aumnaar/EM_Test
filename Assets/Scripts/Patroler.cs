using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public GameObject rightBorder;
    public GameObject leftBorder;
    public Rigidbody2D rb;

    public bool movingRight = true;
    public bool patrol = true;

    Animator animator;

    Transform player;



    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != null)
            animator.SetBool("Patrol", true);
        if (patrol)
        {
            if (movingRight)
            {
                rb.velocity = Vector2.right * speed;
                transform.localScale = Vector3.one;
                if (transform.position.x > rightBorder.transform.position.x)
                {
                    movingRight = !movingRight;
                }
            }
            else
            {
                rb.velocity = Vector2.left * speed;
                transform.localScale = new Vector3(-1, 1, 1);
                if (transform.position.x < leftBorder.transform.position.x)
                {
                    movingRight = !movingRight;
                }
            }
        }
        else
        {
            animator.SetBool("Patrol", false);
        }




    }

}
