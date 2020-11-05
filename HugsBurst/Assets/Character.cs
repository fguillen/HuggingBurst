using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody rb;

    private float time = 0.0f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
      rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeDirection()
    {
      Debug.Log("ChangeDirection");

      var randVelocity = Random.Range(-10, 10);
      var randCoordinate = Random.Range(0, 2);

      if(randCoordinate == 0)
      {
        velocity = new Vector3(randVelocity, 0, 0);
      } else if(randCoordinate == 1)
      {
        velocity = new Vector3(0, 0, randVelocity);
      }
    }

    void FixedUpdate()
    {
      time = time + Time.fixedDeltaTime;
      if (time > 2.0f)
      {
          ChangeDirection();
          time = 0.0f;
      }

      rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
