using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera : MonoBehaviour
{
    private float _slidecamera;
    public Collider2D _triggerCamera_Arena;    
    private Trigger_Camera_and_Gate _trigcag;
    public GameObject _gg;
    private Vector3 _camera;
   public void Start()
    {
        
        _trigcag = GetComponent<Trigger_Camera_and_Gate>();
        _camera = transform.position - _gg.transform.position;
      
    }

  
   public void Update()
    {
      //  transform.position = _gg.transform.position + _camera;
        if (!_triggerCamera_Arena.isTrigger)
        {
            _slidecamera = Time.deltaTime;
           // transform.position = _gg.transform.position + _camera ;
            transform.position = _gg.transform.position + _camera;
        }
    }
}
