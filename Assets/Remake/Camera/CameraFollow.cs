using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    private Vector3 followCamera;
    private float limit=100;

    private void Start()
    {
        followCamera = transform.position - followObject.transform.position;
        
    }


    private void Update()
    {
       
        transform.position = followObject.transform.position + followCamera;
    }
}
