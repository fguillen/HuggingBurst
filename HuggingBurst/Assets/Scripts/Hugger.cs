﻿using System.Collections;
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
    public float speedModifierWhenWantingToHug;

    Player player;

    GameObject huggingPoint;

    public GameObject idleBody;
    public GameObject huggingBody;

    public float secondsHugging;
    public float hugStartedAt; 

    public float secondsBetweenHugs;
    public float lastHugFinishedAt; 

    public float walkingPointDistance;
    public Vector3 walkingPoint;

    public GameObject headPoint; 

    public ParticleSystem lovingEffect;
    public ParticleSystem walkingEffect;

    public AudioSource footstepsAudioSource;
    public AudioSource huggingAudioSource;

    [HideInInspector]
    public GameCanvasManager gameCanvasManager;

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        wantsToHug = false;
        hugging = false;
        enoughHugging = false;
        walkingTowardsPoint = false;

        idleBody.SetActive(true);
        huggingBody.SetActive(false);
        WalkingEffectEmissionActive(false);
        LovingEffectEmissionActive(false);

        anim = GetComponent<Animator>();
        // lastTimeHuggingDecission = Time.time;
        player = GameObject.Find("Player").GetComponent<Player> ();
        speed = speed + Random.Range(0, speedNoise);

        gameCanvasManager = FindObjectOfType<GameCanvasManager>();

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

    void CheckLookDirection(Vector3 targetPoint){
      if(transform.position.x > targetPoint.x){
        if(transform.localScale.x == -1) {
          transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
          var shape = walkingEffect.shape;
          shape.rotation = new Vector3(shape.rotation.x, -90, shape.rotation.z);
        }
      } else {
        if(transform.localScale.x == 1) {
          transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
          var shape = walkingEffect.shape;
          shape.rotation = new Vector3(shape.rotation.x, 90, shape.rotation.z);
        }
      }
    }

    public void WalkTowardsRandomPoint(){
      idle = false;
      wantsToHug = false;
      hugging = false;
      enoughHugging = false;
      walkingTowardsPoint = true;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);
      WalkingEffectEmissionActive(true);

      footstepsAudioSource.Play();

      anim.SetBool("Walking", true);
      anim.SetBool("Hugging", false);

      Vector2 randomDirection = Random.insideUnitCircle.normalized;
      float distance = Random.Range(0, walkingPointDistance);
      walkingPoint = new Vector3(transform.position.x + (randomDirection.x * distance), transform.position.y, transform.position.z + (randomDirection.y * distance));

      // print("transform.position: " + transform.position);
      // print("randomDirection: " + randomDirection);
      // print("distance: " + distance);
      // print("Walking Point: " + walkingPoint);
    }

    void Idle(){
      idle = true;
      wantsToHug = false;
      hugging = false;
      enoughHugging = false;
      walkingTowardsPoint = false;

      idleBody.SetActive(true);
      huggingBody.SetActive(false);
      WalkingEffectEmissionActive(false);

      footstepsAudioSource.Stop();

      anim.SetBool("Walking", false);
      anim.SetBool("Hugging", false);
    }

    public void StopHugging(){
      lastHugFinishedAt = Time.time;

      huggingPoint.GetComponent<HuggingPoint>().LiberateHuggingPoint();
      LovingEffectEmissionActive(false);

      huggingAudioSource.Stop();

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
      hugStartedAt = Time.time;

      idle = false;
      wantsToHug = false;
      hugging = true;
      enoughHugging = false;

      idleBody.SetActive(false);
      huggingBody.SetActive(true);
      WalkingEffectEmissionActive(false);

      footstepsAudioSource.Stop();
      huggingAudioSource.Play();

      transform.localScale = huggingPoint.transform.localScale; // flip the object if needed

      anim.SetBool("Walking", false);
      anim.SetBool("Hugging", true);

      CheckIfSimpleOrDoubleHug();
    }

    void CheckIfSimpleOrDoubleHug()
    {
      HuggingPoint otherHuggingPoint = player.huggingPoints.Find(hp => hp.hugger != this);

      if(otherHuggingPoint.hugger != null && otherHuggingPoint.hugger.IsHugging())
      {
        gameCanvasManager.ShowDoubleHugText();
      } else
      {
        gameCanvasManager.ShowHugText();
      }
    }

    void WalkTowardsHuggingPoint(){
      transform.position = Vector3.MoveTowards(transform.position, huggingPoint.transform.position, speed * speedModifierWhenWantingToHug * Time.deltaTime);

      CheckLookDirection(huggingPoint.transform.position);

      if(transform.position == huggingPoint.transform.position){
        StartHugging();
      }

      // if Hugger is too far from Player we forget Player
      if(Vector3.Distance(transform.position, huggingPoint.transform.position) > huggingPoint.GetComponent<HuggingPoint>().attractionDistance) {
        StopHugging();
      }
    }

    void WalkTowardsPoint(){
      transform.position = Vector3.MoveTowards(transform.position, walkingPoint, speed * Time.deltaTime);

      CheckLookDirection(walkingPoint);

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
      anim.SetBool("Hugging", false);
      WalkingEffectEmissionActive(true);
      LovingEffectEmissionActive(true);

      footstepsAudioSource.Play();
    }

    public bool IsIdle(){
      return idle;
    }

    public bool IsAvailableForHug(){
      return idle || walkingTowardsPoint;
    }

    public bool IsHugging() {
      return hugging;
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

    void WalkingEffectEmissionActive(bool active){
      var emission = walkingEffect.emission;
      emission.enabled = active;
    }

    void LovingEffectEmissionActive(bool active){
      var emission = lovingEffect.emission;
      emission.enabled = active;
    }
}
