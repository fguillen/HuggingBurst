using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour
{
    bool idle;
    bool wantsToHug;
    bool hugging;
    bool enoughHugging;

    Animator anim;
    // public float secondsBetweenHuggingDecission;
    // public float lastTimeHuggingDecission;
    public float speed;
    public float speedNoise;
    GameObject player;

    GameObject huggingPoint;

    public GameObject idleBody;
    public GameObject huggingBody;

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        wantsToHug = false;
        hugging = false;
        enoughHugging = false;

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

    void Hugging()
    {
      print("Hugging");

      idle = false;
      wantsToHug = false;
      hugging = true;
      enoughHugging = false;

      idleBody.SetActive(false);
      huggingBody.SetActive(true);

      anim.SetBool("WantsToHug", false);
      anim.SetBool("Hugging", true);
    }

    void WalkTowardsHuggingPoint(){
      transform.position = Vector3.MoveTowards(transform.position, huggingPoint.transform.position, speed * Time.deltaTime);

      if(transform.position == huggingPoint.transform.position){
        Hugging();
      }
    }

    public void WantsToHug(GameObject _huggingPoint)
    {
      idle = false;
      wantsToHug = true;
      hugging = false;
      enoughHugging = false;

      huggingPoint = _huggingPoint;

      anim.SetBool("WantsToHug", true);
    }

    public bool IsIdle(){
      return idle;
    }
}
