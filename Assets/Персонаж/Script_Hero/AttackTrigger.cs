using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitController))]
[RequireComponent(typeof(Bot_move))]
public class AttackTrigger : MonoBehaviour
{
    private float _dmgEnemy = 0.1f;
    private float _dmgPlayer = 25f;
    private UnitController _UnitController;
    public bool _comboattackfirst;

    private void Start()
    {
        _UnitController = GetComponent<UnitController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            
            collision.SendMessageUpwards("Damage", _dmgPlayer);
        //    _comboattackfirst = true;
         //   _UnitController.Damage(50);
        }

       if(collision.isTrigger !=true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("DamagePlayer", _dmgEnemy);
        }
    }
}
