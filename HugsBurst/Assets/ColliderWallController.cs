using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderWallController : MonoBehaviour
{
    void OnCollisionStay(Collision collision) {
        Collider other = collision.collider;
        if(other.CompareTag("Hugger")) {
            other.gameObject.GetComponent<Hugger>().WalkTowardsRandomPoint();
        }
    }
}
