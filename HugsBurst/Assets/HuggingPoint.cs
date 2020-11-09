using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuggingPoint : MonoBehaviour
{
    bool taken;
    Hugger hugger;

    public float attractionDistance;
    public LayerMask huggersLayer;
    public int radarPower; // maximum 10000

    // Start is called before the first frame update
    void Start()
    {
      taken = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(!taken)
      {
        Hugger availableHugger = FindAvailableHugger();
        if(availableHugger){
          print("Available Hugger found");
          hugger = availableHugger;
          hugger.WantsToHug(this.gameObject);
          taken = true;
        }
      }
    }

    Hugger FindAvailableHugger(){
      print("FindAvailableHugger");

      Collider[] nearHuggers = Physics.OverlapSphere(transform.position, attractionDistance, huggersLayer);
      Hugger selectedHugger = null;

      foreach(var hugger in nearHuggers){
        print("Checking Hugger");
        Hugger huggerScript = hugger.GetComponent<Hugger>();

        if(huggerScript.IsIdle()) { // only if is idle now
          if(Random.Range(0, 10000 - radarPower) == 0) {
            selectedHugger = huggerScript;
            break;
          }
        }
      }

      return selectedHugger;
    }

    void OnDrawGizmos()
    {
      // print("Drawing Gizmos: " + transform.position + " -- " + attractionDistance);
      Gizmos.color = Color.yellow;
      Gizmos.DrawSphere(transform.position, attractionDistance);
    }
}
