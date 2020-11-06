using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour
{
    private bool wantsToHug;
    private Animator anim;
    public float secondsBetweenHuggingDecission;
    public float lastTimeHuggingDecission;
    public float speed;
    public float speedNoise;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastTimeHuggingDecission = Time.time;
        player = GameObject.Find("Player");
        speed = speed + Random.Range(-speedNoise, speedNoise);
    }

    // Update is called once per frame
    void Update()
    {
      if(!wantsToHug && (Time.time > (lastTimeHuggingDecission + secondsBetweenHuggingDecission)))
      {
        print("Deciding to Hug?");
        lastTimeHuggingDecission = Time.time;
        var rand = Random.Range(0, 10);

        if(rand == 0)
        {
          print("Wants to Hug");
          WantsToHug();
        } else
        {
          print("Doesn't want to Hug");
        }
      }

      if(wantsToHug)
      {
        WalkTowardsPlayer();
      }
    }

    void WalkTowardsPlayer(){
      transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void WantsToHug()
    {
      wantsToHug = true;
      anim.SetBool("WantsToHug", true);
    }
}
