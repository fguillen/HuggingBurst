using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour
{
    bool idle;
    bool wantsToHug;
    bool hugging;
    bool enoughHugging;
    bool walkingTowardsPoint;

    public List<GameObject> hats;

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

    public float walkingPointDistance;
    public Vector3 walkingPoint;

    public GameObject loveParticlesPrefab;
    private GameObject loveParticles;
    public GameObject headPoint; 

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        wantsToHug = false;
        hugging = false;
        enoughHugging = false;
        walkingTowardsPoint = false;

        huggingBody.SetActive(false);

        anim = GetComponent<Animator>();
        // lastTimeHuggingDecission = Time.time;
        // player = GameObject.Find("Player");
        speed = speed + Random.Range(-speedNoise, speedNoise);

        SetRandomHat();
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

      if(walkingTowardsPoint)
      {
        WalkTowardsPoint();
      }

      if(hugging){
        Hugging();
      }

      if(enoughHugging){
        if(RemainingTimeEnoughHugging() == 0) {
          Idle();
        }
      }

      if(idle) {
        if(Random.Range(0, 100) == 0){
          WalkTowardsRandomPoint();
        }
      }
    }

    void SetRandomHat(){
      // Deactivate all Hats
      foreach (GameObject hat in hats){
        hat.SetActive(false);
      }

      // Activate random one
      int index = Random.Range(0, hats.Count);
      hats[index].SetActive(true);
    }

    void WalkTowardsRandomPoint(){
      idle = false;
      wantsToHug = false;
      hugging = false;
      enoughHugging = false;
      walkingTowardsPoint = true;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);

      anim.SetBool("Walking", true);
      anim.SetBool("Hugging", false);

      Vector2 randomDirection = Random.insideUnitCircle.normalized;
      float distance = Random.Range(0, walkingPointDistance);
      walkingPoint = new Vector3(transform.position.x + (randomDirection.x * distance), transform.position.y, transform.position.z + (randomDirection.y * distance));

      print("transform.position: " + transform.position);
      print("randomDirection: " + randomDirection);
      print("distance: " + distance);
      print("Walking Point: " + walkingPoint);
    }

    void Idle(){
      idle = true;
      wantsToHug = false;
      hugging = false;
      enoughHugging = false;
      walkingTowardsPoint = false;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);

      anim.SetBool("Walking", false);
      anim.SetBool("Hugging", false);
    }

    void StopHugging(){
      lastHugFinishedAt = Time.time;

      huggingPoint.GetComponent<HuggingPoint>().LiberateHuggingPoint();
      Destroy(loveParticles);

      WalkTowardsRandomPoint();
    }

    void Hugging(){
      transform.position = huggingPoint.transform.position;

      if(RemainingTimeHugging() == 0) {
        StopHugging();
      }
    }

    void StartHugging()
    {
      print("StartHugging");

      hugStartedAt = Time.time;

      idle = false;
      wantsToHug = false;
      hugging = true;
      enoughHugging = false;

      idleBody.SetActive(false);
      huggingBody.SetActive(true);

      transform.localScale = huggingPoint.transform.localScale; // flip the object if needed

      anim.SetBool("Walking", false);
      anim.SetBool("Hugging", true);
    }

    void WalkTowardsHuggingPoint(){
      transform.position = Vector3.MoveTowards(transform.position, huggingPoint.transform.position, speed * Time.deltaTime);

      if(transform.position == huggingPoint.transform.position){
        StartHugging();
      }
    }

    void WalkTowardsPoint(){
      transform.position = Vector3.MoveTowards(transform.position, walkingPoint, speed * Time.deltaTime);

      if(transform.position == walkingPoint){
        Idle();
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
      walkingTowardsPoint = false;

      huggingPoint = _huggingPoint;

      anim.SetBool("Walking", true);

      // Love Particles
      Vector3 position = headPoint.transform.position;
      position.y = position.y + 0.1f;
      loveParticles = Instantiate(loveParticlesPrefab, position, Quaternion.identity, headPoint.transform);
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

      // WalkingPoint
      if(walkingTowardsPoint) {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(walkingPoint, 0.1f);
        Gizmos.DrawLine(transform.position, walkingPoint);
      }
    }
}
