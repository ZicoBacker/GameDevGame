using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    public GameObject roc;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

    }

    private void OnBecameVisible()
    {
        Debug.Log("Hello");
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
    }
}
