using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator> ();        
    }

    public void Zoom(){
        animator.SetTrigger("Zoom");
    }

    public void DoubleZoom(){
        animator.SetTrigger("DoubleZoom");
    }
}
