using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitController))]
public class Trigger_Camera_and_Gate : MonoBehaviour
{
    public BoxCollider2D _gateEnemy;
    public BoxCollider2D _gate;
    public bool _gatetrigger;
    private UnitController _UnitController;
    private bool _spawnbots;
    public GameObject gatesprite;

    void Start()
    {
      
        _spawnbots = true;
        _UnitController = GetComponent<UnitController>();
        _gateEnemy.isTrigger = true;
        _gate.isTrigger = true;
    }
    public void OnTriggerEnter2D(Collider2D triggerArenaStart)
    {
        if (triggerArenaStart.isTrigger)
        {
            gatesprite.transform.position = new  Vector2 (-0.01f, 0.96f);
            _gateEnemy.isTrigger = false;
            _gate.isTrigger=false;
            _gatetrigger = true;
            if (_spawnbots)
            { 
                _spawnbots = false;
            }
        }
    }
}
