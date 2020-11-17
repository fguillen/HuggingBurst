using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCoordinateZ : MonoBehaviour
{
    float originalZ;

    // Start is called before the first frame update
    void Start()
    {
        originalZ = transform.position.z;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = originalZ;
        transform.position = currentPosition;
    }
}
