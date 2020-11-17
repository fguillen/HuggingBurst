using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    Animator anim;
    public List<HuggingPoint> huggingPoints;

    bool walking;

    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      walking = false;
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

      if(rb.velocity != Vector3.zero){
        if(!walking) {
          walking = true;
          anim.SetBool("Walking", true);
          Invoke("SeparateFromHuggers", 0.3f);
        }
        
      } else {
        if(walking) {
          walking = false;
          anim.SetBool("Walking", false);
        }
      }
    }

    void SeparateFromHuggers() {
      foreach (HuggingPoint huggingPoint in huggingPoints)
      {
          huggingPoint.LiberateHugger();
      }
    }
}
