using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement:")]
    public float speed = 4f;
    private float InputX;
    private float InputY;
    private Vector2 movement;
    private Vector2 lastMovement;
    [SerializeField]
    private Rigidbody2D rb;
  

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        movement = new Vector2(InputX, InputY);
        rb.velocity = movement.normalized * speed;


    }
}
