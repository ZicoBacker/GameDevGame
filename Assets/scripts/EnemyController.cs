using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float damage;


    public GameObject Player;
    private Rigidbody2D SelfRb;
    public Vector2 playerLocation;
    public bool facingRight;

    public float speed = 400;
    // Start is called before the first frame update
    void Start()
    {
        SelfRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = Player.transform.position - gameObject.transform.position;

        if (playerLocation.x < 0)
        {
            SelfRb.velocity = new Vector2(-speed,0);
            facingRight = false;
        } else
        {
            if (playerLocation.x > 0)
            {
                SelfRb.velocity = new Vector2(speed,0);
                facingRight = true;
            }
        }

        Turn();
    }

    private void Turn()
    {
        if (facingRight)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void CheckAlive()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
