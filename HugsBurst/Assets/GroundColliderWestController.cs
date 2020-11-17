using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColliderWestController : MonoBehaviour
{
    public GroundController groundController;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            groundController.CreateGroundWest();
        }
    }
}
