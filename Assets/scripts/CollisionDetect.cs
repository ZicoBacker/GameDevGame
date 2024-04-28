using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private Rigidbody2D objRb;
    // Start is called before the first frame update
    void Start()
    {
        objRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player withing object");
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("roc hit floor");
            objRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player outside objecy");
        }
    }
}
