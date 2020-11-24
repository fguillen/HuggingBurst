using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    Animator animator;
    AudioManager audioManager;

    [SerializeField] VirtualCameraController virtualCameraController;

    void Start () {
        animator = gameObject.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ShowHugText(){
        animator.SetTrigger("ShowHugText");
        virtualCameraController.Zoom();
    }

    public void ShowDoubleHugText(){
        animator.SetTrigger("ShowDoubleHugText");
        audioManager.Play("DoubleHug");
        virtualCameraController.DoubleZoom();
    }
}
