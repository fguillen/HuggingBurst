using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform groundAnchorEast;
    public Transform groundAnchorWest;
    public GameObject groundTemplate;
    GameObject groundEast;
    GameObject groundWest;
    public LevelManager levelManager;
    public List<GameObject> huggersNests;

    public void CreateGroundEast(){
        if(groundEast == null){
            groundEast = Instantiate(groundTemplate, groundAnchorEast.position, groundTemplate.transform.rotation);
            groundEast.GetComponent<GroundController>().groundWest = this.gameObject;
        }

        if(groundWest != null) {
            Destroy(groundWest);
        }
    }
    public void CreateGroundWest(){
        if(groundWest == null){
            groundWest = Instantiate(groundTemplate, groundAnchorWest.position, groundTemplate.transform.rotation);
            groundWest.GetComponent<GroundController>().groundEast = this.gameObject;
        }

        if(groundEast != null) {
            Destroy(groundEast);
        }
    }

    public void SetActiveGround(){
        levelManager.SetActiveGround(this);
    }
}
