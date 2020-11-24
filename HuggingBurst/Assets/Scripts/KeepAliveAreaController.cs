using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAliveAreaController : MonoBehaviour
{
    public LevelManager levelManager;

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Hugger")) {
            levelManager.DestroyHugger(other.gameObject);
        }
    }
}
