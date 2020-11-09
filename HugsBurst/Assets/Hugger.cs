using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour
{
    private bool wantsToHug;
    private Animator anim;
    // public float secondsBetweenHuggingDecission;
    // public float lastTimeHuggingDecission;
    public float speed;
    public float speedNoise;
    private GameObject player;

    private GameObject huggingPoint;

    // Start is called before the first frame update
    void Start()
    {
        wantsToHug = false;
        anim = GetComponent<Animator>();
        // lastTimeHuggingDecission = Time.time;
        // player = GameObject.Find("Player");
        speed = speed + Random.Range(-speedNoise, speedNoise);
    }

    // Update is called once per frame
    void Update()
    {
      // if(!wantsToHug && (Time.time > (lastTimeHuggingDecission + secondsBetweenHuggingDecission)))
      // {
      //   print("Deciding to Hug?");
      //   lastTimeHuggingDecission = Time.time;
      //   var rand = Random.Range(0, 10);

      //   if(rand == 0)
      //   {
      //     print("Wants to Hug");
      //     WantsToHug();
      //   } else
      //   {
      //     print("Doesn't want to Hug");
      //   }
      // }

      if(wantsToHug)
      {
        WalkTowardsHuggingPoint();
      }
    }

    void WalkTowardsHuggingPoint(){
      transform.position = Vector3.MoveTowards(transform.position, huggingPoint.transform.position, speed * Time.deltaTime);
    }

    public void WantsToHug(GameObject _huggingPoint)
    {
      huggingPoint = _huggingPoint;
      wantsToHug = true;
      anim.SetBool("WantsToHug", true);
    }
}
