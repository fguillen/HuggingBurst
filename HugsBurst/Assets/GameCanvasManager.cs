using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    public Animator animator;

    void Start () {
        animator = gameObject.GetComponent<Animator>();
    }
    public void ShowHugText(){
        animator.SetTrigger("ShowHugText");
    }
}
