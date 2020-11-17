using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject huggerTemplate;
    public float secondsBetweenHuggerCreation;
    float lastHuggerCreatedAt;
    public List<GameObject> huggersNests;
    GroundController activeGround;

    // Start is called before the first frame update
    void Start()
    {
      lastHuggerCreatedAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
      if((Time.time - lastHuggerCreatedAt) > secondsBetweenHuggerCreation) {
        CreateHugger();
      }
    }

    void CreateHugger()
    {
      lastHuggerCreatedAt = Time.time;
      int randomIndex = Random.Range(0, activeGround.huggersNests.Count);
      Instantiate(huggerTemplate, activeGround.huggersNests[randomIndex].transform.position, activeGround.huggersNests[randomIndex].transform.rotation);
    }

    public void SetActiveGround(GroundController groundController){
      activeGround = groundController;
    }
}
