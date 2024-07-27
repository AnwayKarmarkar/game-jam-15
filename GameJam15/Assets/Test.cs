using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            /*          Vector2 difference = (transform.position - collision.transform.position).normalized;
                      Vector2 force = difference * knockbackForce;*/
            Vector2 difference = (transform.position - playerPos.position).normalized * 1;

            rb.velocity = difference * 5;

        }

    }
}
