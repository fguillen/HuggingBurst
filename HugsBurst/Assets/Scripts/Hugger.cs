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

    public float secondsHugging;
    public float hugStartedAt; 

    public float secondsBetweenHugs;
    public float lastHugFinishedAt; 

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        wantsToHug = false;
        hugging = false;
        enoughHugging = false;

        huggingBody.SetActive(false);

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

      if(hugging){
        if(RemainingTimeHugging() == 0) {
          StopHugging();
        }
      }

      if(enoughHugging){
        if(RemainingTimeEnoughHugging() == 0) {
          Idle();
        }
      }
    }

    void Idle(){
      idle = true;
      wantsToHug = false;
      hugging = false;
      enoughHugging = false;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);

      anim.SetBool("WantsToHug", false);
      anim.SetBool("Hugging", false);
    }

    void StopHugging(){
      lastHugFinishedAt = Time.time;

      idle = false;
      wantsToHug = false;
      hugging = false;
      enoughHugging = true;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);

      anim.SetBool("WantsToHug", false);
      anim.SetBool("Hugging", false);

      huggingPoint.GetComponent<HuggingPoint>().LiberateHuggingPoint();
    }

    void Hugging()
    {
      print("Hugging");

      hugStartedAt = Time.time;

      idle = false;
      wantsToHug = false;
      hugging = true;
      enoughHugging = false;

      idleBody.SetActive(false);
      huggingBody.SetActive(true);

      transform.localScale = huggingPoint.transform.localScale; // flip the object if needed

      anim.SetBool("WantsToHug", false);
      anim.SetBool("Hugging", true);
    }

    void WalkTowardsHuggingPoint(){
      transform.position = Vector3.MoveTowards(transform.position, huggingPoint.transform.position, speed * Time.deltaTime);

      if(transform.position == huggingPoint.transform.position){
        Hugging();
      }
    }

    public float RemainingTimeHugging(){
      float result = secondsHugging - (Time.time - hugStartedAt) ;
      if(result < 0) result = 0;

      return result;
    }

    public float RemainingTimeEnoughHugging(){
      float result = secondsBetweenHugs - (Time.time - lastHugFinishedAt) ;
      if(result < 0) result = 0;

      return result;
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

    void OnDrawGizmos(){
      // RemainingTimeHugging
      Gizmos.color = new Color(1, 0, 0, 0.5f);
      Vector3 position = transform.position;
      position.y = position.y - 2f;
      Gizmos.DrawCube(position, new Vector3(RemainingTimeHugging(), 0.1f, 0.1f));

      // RemainingTimeEnoughHugging
      Gizmos.color = new Color(0, 1, 0, 0.5f);
      position = transform.position;
      position.y = position.y - 2.2f;
      Gizmos.DrawCube(position, new Vector3(RemainingTimeEnoughHugging(), 0.1f, 0.1f));
    }
}
