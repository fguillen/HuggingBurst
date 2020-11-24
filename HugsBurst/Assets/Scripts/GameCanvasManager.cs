using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    Animator animator;
    AudioManager audioManager;


    void Start () {
        animator = gameObject.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ShowHugText(){
        animator.SetTrigger("ShowHugText");
    }

    public void ShowDoubleHugText(){
        animator.SetTrigger("ShowDoubleHugText");
        audioManager.Play("DoubleHug");
    }
}
