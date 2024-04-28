using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private EnemyController enemy;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        //sets attack damage.
        damage = GetComponentInParent<PlayerController>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Our poly collider is a trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if enemy enters trigger, remove hp, and checks if alive
        if (other.gameObject.CompareTag("Enemy")) 
        {
            Debug.Log("Enemy in range!");
            //might Cause a lagspike if we hit alot of enemies at once.
            enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.health -= damage;
            enemy.CheckAlive();

        }
    }
}
