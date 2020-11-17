using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform groundAnchorEast;
    public Transform groundAnchorWest;
    public GameObject groundTemplate;
    public GameObject groundEast;
    public GameObject groundWest;

    public void CreateGroundEast(){
        if(groundEast == null){
            print("CreateGroundEast");
            groundEast = Instantiate(groundTemplate, groundAnchorEast.position, groundTemplate.transform.rotation);
            groundEast.GetComponent<GroundController>().groundWest = this.gameObject;
        }
    }
    public void CreateGroundWest(){
        if(groundWest == null){
            print("CreateGroundWest");
            groundWest = Instantiate(groundTemplate, groundAnchorWest.position, groundTemplate.transform.rotation);
            groundWest.GetComponent<GroundController>().groundEast = this.gameObject;
        }
    }
}
