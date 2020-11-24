using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColliderTotalController : MonoBehaviour
{
    public GroundController groundController;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            groundController.SetActiveGround();
        }
    }
}
