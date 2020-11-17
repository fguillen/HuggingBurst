using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject huggerTemplate;
    public float secondsBetweenHuggerCreation;
    float lastHuggerCreatedAt;
    GroundController activeGround;
    public float maxNumberOfHuggers;
    List<GameObject> huggers;

    // Start is called before the first frame update
    void Start()
    {
      lastHuggerCreatedAt = Time.time;
      huggers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
      if(huggers.Count < maxNumberOfHuggers) {
        if((Time.time - lastHuggerCreatedAt) > secondsBetweenHuggerCreation) {
          CreateHugger();
        }
      }
    }

    void CreateHugger()
    {
      lastHuggerCreatedAt = Time.time;
      int randomIndex = Random.Range(0, activeGround.huggersNests.Count);
      GameObject hugger = Instantiate(huggerTemplate, activeGround.huggersNests[randomIndex].transform.position, activeGround.huggersNests[randomIndex].transform.rotation);
      huggers.Add(hugger);
    }

    public void DestroyHugger(GameObject hugger) {
      huggers.Remove(hugger);
      Destroy(hugger);
    }

    public void SetActiveGround(GroundController groundController){
      activeGround = groundController;
    }
}
