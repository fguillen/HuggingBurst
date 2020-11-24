using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuggingPoint : MonoBehaviour
{
    bool taken;
    public Hugger hugger;

    public float attractionDistance;
    public LayerMask huggersLayer;
    public int radarPower; // maximum 100

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
          hugger = availableHugger;
          hugger.WantsToHug(this.gameObject);
          taken = true;
        }
      }
    }

    Hugger FindAvailableHugger(){
      Collider[] nearHuggers = Physics.OverlapSphere(transform.position, attractionDistance, huggersLayer);
      Hugger selectedHugger = null;

      foreach(var hugger in nearHuggers){
        Hugger huggerScript = hugger.GetComponent<Hugger>();

        if(huggerScript.IsAvailableForHug()) { // only if is idle now
          var random = Random.Range(0, 1000 - radarPower);
          if(random == 0) {
            selectedHugger = huggerScript;
            break;
          }
        }
      }

      return selectedHugger;
    }

    public void LiberateHugger() {
      if(hugger != null && hugger.IsHugging()) {
        hugger.StopHugging();
      }
    }

    public void LiberateHuggingPoint(){
      taken = false;
      hugger = null;
    }

    void OnDrawGizmos()
    {
      // print("Drawing Gizmos: " + transform.position + " -- " + attractionDistance);
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, attractionDistance);
    }
}
