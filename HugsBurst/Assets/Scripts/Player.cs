using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      Move();
    }

    void Move()
    {
      float horizontalMove = -Input.GetAxis("Horizontal");
      float verticalMove = -Input.GetAxis("Vertical");

      rb.velocity =
        new Vector3(
          speed * horizontalMove,
          0,
          speed * verticalMove
        );
    }
}
