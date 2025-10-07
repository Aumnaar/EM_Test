using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] private float _default_speed;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] public bool _canWalk;
    [SerializeField] int glideSpeed = 2;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Additional Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Ground Checks")]
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    [SerializeField] private Health _health;
    public Health Health { get { return _health; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _default_speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        ////MOVEMENT//////
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);

        if (horizontalInput != 0) 
        animator.SetBool("Walking", true);
        else
        animator.SetBool("Walking", false);

        if (horizontalInput > 0)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        ////JUMPING/////

        animator.SetFloat("yVelocity", rb.velocity.y);

        if (rb.velocity.y < 1 && isGrounded())
        {
            animator.SetBool("Jump", false);
        }

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
            
        }


        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            animator.SetBool("Jump", true);
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isGrounded())
        {
            Jump2();
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }

        ///ATTACK///
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Attack");
            _speed = 0;
        }

    }

    private void FixedUpdate()
    {
        /////GLIDING///////

        if (rb.velocity.magnitude > 5 && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, glideSpeed);
            animator.SetBool("Jump", false);
            animator.Play("Gliding");
        }

     }



    ////JUMPING SYSTEM//////




    private void Jump()
    {
        if (!isGrounded() && coyoteCounter < 0 && jumpCounter <= 0) return;


        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);

        }
    }

    private void Jump2()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!isGrounded() && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(horizontalInput * _speed, _jumpPower);

        }
        else if (!isGrounded() && coyoteCounter <= 0 && jumpCounter > 0)
        {
            rb.velocity = new Vector2(horizontalInput * _speed, _jumpPower);
            jumpCounter--;

        }
    }


    ////GROUND CHECK////

    public bool isGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }


    /////ATTACK//////////

        public void Attack()
    {
        _speed = _default_speed;
    }

}
