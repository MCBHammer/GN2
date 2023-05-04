using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float bounceStrength = 6f;

    private bool isDashing = false;
    [SerializeField] private float dashSpeed = 11f;
    private float dashStop = 0.4f;
    private float currentDashStop;

    private float currentMoveSpeed;

    private bool isWallSliding = false;
    private float wallSlidingSpeed = 2f;
    private Vector2 wallJumpStrength;
    private bool isWallJumping = false;
    private float wallJumpDuration = 0.25f;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask stompableObjects;

    [SerializeField] private GameObject playerArt;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(dirX * currentMoveSpeed, rb.velocity.y);
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(0, jumpStrength, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            isDashing = true;
            currentMoveSpeed = dashSpeed;
            currentDashStop = dashStop;
        }

        if(isDashing && rb.velocity.x == 0)
        {
            currentDashStop -= Time.deltaTime;
        } else
        {
            currentDashStop = dashStop;
        }

        if (currentDashStop <= 0)
        {
            isDashing = false;
            currentMoveSpeed = moveSpeed;
            currentDashStop = dashStop;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        WallSlide(dirX);
        WallJump(dirX);

        if(dirX > 0)
        {
            playerArt.transform.localScale = new Vector3(1, 1, 1);
        }
        if(dirX < 0)
        {
            playerArt.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void WallSlide(float horizontal)
    {
        if(IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } else
        {
            isWallSliding = false;
        }
    }

    private void WallJump(float horizontal)
    {
        wallJumpStrength = new Vector2(currentMoveSpeed, jumpStrength);
        if(Input.GetButtonDown("Jump") && isWallSliding)
        {
            isWallJumping = true;
            float wallJumpDirection = -1 * Mathf.Sign(horizontal);
            rb.velocity = new Vector2(wallJumpDirection * wallJumpStrength.x, wallJumpStrength.y);
            Invoke(nameof(StopWallJumping), wallJumpDuration);
        } 
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size * 0.5f, 0, Vector2.down, .8f, jumpableGround);
    }

    private bool IsWalled()
    {
        bool left = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size * 0.5f, 0, Vector2.left, .3f, jumpableGround);
        bool right = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size * 0.5f, 0, Vector2.right, .3f, jumpableGround);
        if(left || right)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool IsStomping()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size * 0.5f, 0, Vector2.down, .8f, stompableObjects);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard") && this.gameObject.TryGetComponent(out PlayerData player))
        {
            if (IsStomping())
            {
                Destroy(other.transform.parent.gameObject);
                rb.velocity = new Vector3(0, bounceStrength, 0);
            } else
            {
                EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
                int damage = enemyDamage.Damage;
                player.healthChange(damage);
            }
        }
    }
}
