using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject huggerTemplate;
    public int numOfHuggers;
    public Transform limitNorth;
    public Transform limitSouth;
    public Transform limitEast;
    public Transform limitWest;

    // Start is called before the first frame update
    void Start()
    {
      CreateHuggers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateHuggers()
    {
      for(int i = 0; i < numOfHuggers; i++)
      {
        CreateHugger();
      }
    }

    void CreateHugger()
    {
      var x = Random.Range(limitWest.position.x, limitEast.position.x);
      var z = Random.Range(limitNorth.position.z, limitSouth.position.z);

      Instantiate(huggerTemplate, new Vector3(x, 2, z), huggerTemplate.transform.rotation);
    }
}
