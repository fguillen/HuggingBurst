using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    Animator animator;
    AudioManager audioManager;

    [SerializeField] UseCursorsTextController useCursorsTextController;

    [SerializeField] VirtualCameraController virtualCameraController;

    bool useCursorsTextHidden;

    void Start () {
        animator = gameObject.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        useCursorsTextHidden = false;
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

    public void HideUseCursorsText() {
        if(!useCursorsTextHidden)
        {
            useCursorsTextController.Hide();
            useCursorsTextHidden = true;
        }
    }
}
