using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //gameobjects.
    public GameObject player;
    private GameObject attackArea = default;

    //floats
    public float health;
    public float damage;
    public float speed;
    public float jumpPower;
    public float KbBoundry = 0.8f;
    public float knockbackMultiplier = 15f;

    //bools
    public bool IsStunned = false;
    public bool IsDead = false;
    public bool canJump;
    private bool attacking = false;

    //player components
    private Rigidbody2D playerRb;
    private Vector2 move;
    private Animator playerAnim;

    //movement inputs
    private float horizontalInput;
    private float verticalInput;

    //attack props
    public float TTA;
    private float attackTimer = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        attackArea = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        if ((move.x != 0 || move.y != 0) && !IsStunned && !IsDead)
        {
            playerRb.velocity = new Vector2(move.x * speed * Time.deltaTime, playerRb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        move = new Vector2(horizontalInput, verticalInput);

        if (Input.GetKey(KeyCode.Space) && canJump && !IsStunned && !IsDead)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 10 * jumpPower);
            Debug.Log("jump!!");
            canJump = false;
        }

        //checks for left click input.
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        } 

        if (attacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= TTA)
            {
                attackTimer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }


        Animations();
        Turn();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //jump reset when floor is hit and can't jump.
        if (other.gameObject.CompareTag("Floor") && !canJump)
        {
            canJump = true;
            Debug.Log("Jump reset");
        }

        //Lose hp if you're not dead and hit an enemy.
        if (other.gameObject.CompareTag("Enemy") && !IsDead)
        {
            IsStunned = true;
            health -= 10;
            Debug.Log("Player got hit!");
            
            //This works, But I would like the knockback to not be based on the difference,
            //and rather a static amount. Problem is that you can go into the minus coords
            //^ I think this has been fixed, although not perfectly. -26-4

            Vector2 difference = (other.transform.position - player.transform.position).normalized;
            GetKb(difference);
        }
    }

    //turns player sprite left or right based on which direction it is moving
    //Refactored!
    private void Turn()
    {
        if (horizontalInput > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    //gives the player knockback based on the difference between the enemy and the player.
    private void GetKb(Vector2 difference)
    {
        // add a little extra kb for the player to not get stuck ontop of the enemy
        if (difference.x < 0)
        {
            difference.x -= 2;
        }
        else
        {
            difference.x += 2;
        }
        Debug.Log(difference);
        playerRb.AddRelativeForce(new Vector2(difference.x * -knockbackMultiplier, 1f * knockbackMultiplier), ForceMode2D.Impulse);
        StartCoroutine(GotHitCoolDown());
    }

    //A cooldown after getting hit so the player is unable to move until velocity is below Knockback Boundry.
    private IEnumerator GotHitCoolDown()
    {
        yield return new WaitUntil( () => playerRb.velocity.x < KbBoundry && playerRb.velocity.x > -KbBoundry);
        Debug.Log("hello!!!!");
        IsStunned = false;
    }

    // Will handle all animations.
    private void Animations()
    {
        if (horizontalInput != 0)
        {
            playerAnim.SetBool("IsWalking", true);
        }
        else
        {
            playerAnim.SetBool("IsWalking", false);
        }
    }

    //sets player attackArea.
    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        
    }

}