using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger_Camera_and_Gate))]
[RequireComponent(typeof(UnitController))]
public class MoveEnemy : MonoBehaviour
{
    private Collider2D _Triggergate;
    public Transform[] _points = new Transform[2];
    private UnitController _enemycontroll;
   // public GameObject _maincharacter;
    private float _distans = 5f;
    private Transform _target;
    private float _checkRadius;
    private Collider2D _enemy;
    public Animator animatorenemy;
    void Start()
    {
        _enemy = GetComponent<Collider2D>();
        _Triggergate = GameObject.Find("GateLockedRoom").GetComponent<Collider2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemycontroll = GetComponent<UnitController>();
    }

    void Update()
    {
        //  _direction = _target.transform.position.x - transform.position.x;
        if (_Triggergate.isTrigger) 
        {
            Debug.Log(_Triggergate.isTrigger);
            return;
        }
        if (Vector2.Distance(transform.position, _target.position) > 3)
        {
            animatorenemy.SetBool("isRunning",true);
            if ((_target.transform.position.x - transform.position.x < 0) && (_target.transform.position.y == transform.position.y))
            {
                _enemycontroll.Move(-1);
            }
            else if ((_target.transform.position.x - transform.position.x > 0) && (_target.transform.position.y == transform.position.y))
            {
                _enemycontroll.Move(1);
            }else if ((_target.transform.position.y < transform.position.y) && (!_enemycontroll.jumpstatus))
            {
                if (Vector2.Distance(transform.position, _points[0].transform.position) > Vector2.Distance(transform.position, _points[1].transform.position))
                {
                    _enemycontroll.Move(1);
                }
                else if (Vector2.Distance(transform.position, _points[0].transform.position) < Vector2.Distance(transform.position, _points[1].transform.position))
                {
                    _enemycontroll.Move(-1);
                }
            }else if ((_target.transform.position.y > transform.position.y) && (!_enemycontroll.jumpstatus))
            {
                animatorenemy.SetBool("isRunning", false);
                if ((Vector2.Distance(transform.position, _points[0].transform.position) > Vector2.Distance(transform.position, _points[1].transform.position)) && (!_enemycontroll.jumpstatus))
                {
                    transform.position = Vector2.MoveTowards(transform.position, _points[1].transform.position, _enemycontroll.speed * Time.deltaTime);
                }
                else if ((Vector2.Distance(transform.position, _points[0].transform.position) < Vector2.Distance(transform.position, _points[1].transform.position)) && (!_enemycontroll.jumpstatus))
                {
                    transform.position = Vector2.MoveTowards(transform.position, _points[0].transform.position, _enemycontroll.speed * Time.deltaTime);
                }
            }
            
            //transform.position = Vector2.MoveTowards(transform.position, _target.position, _enemycontroll._speed * Time.deltaTime);
            _enemycontroll.Flip();

        }

        Debug.Log(_Triggergate.isTrigger);
        //_enemycontroll.Move(1f);
        // _enemycontroll.Flip();
    }
}
