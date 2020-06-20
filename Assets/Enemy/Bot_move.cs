using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Trigger_Camera_and_Gate))]
[RequireComponent(typeof(UnitController))]
public class Bot_move : MonoBehaviour

{
    private Collider2D _Triggergate;
    public Transform[] _points = new Transform[2];
    private UnitController _enemycontroll;
    // public GameObject _maincharacter;
    private float _distans = 4f;
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
        if (_enemycontroll.heal < 0)
        {
            animatorenemy.SetTrigger("Dead");
            _enemycontroll.Dead();
        }
        
        if (_enemycontroll.attacking)
        {
            if (_enemycontroll.attackTimer > 0)
            {
                _enemycontroll.attackTimer -= Time.deltaTime;
            }
            else
            {
                _enemycontroll.attacking = false;
                _enemycontroll.attackTrigger.enabled = false;


            }
        }


        //  _direction = _target.transform.position.x - transform.position.x;
        if (_Triggergate.isTrigger)
        {
           
            return;
        }
        if (Vector2.Distance(transform.position, _target.position) > _distans)
        {
            if (!_enemycontroll.isGrounded)
            {
                animatorenemy.SetBool("isFalling", true);
            }
            if (_enemycontroll.isGrounded)
            {
                animatorenemy.SetBool("isJumping", false);
                animatorenemy.SetBool("isFalling", false);
            }
            if (_target.transform.position.x - transform.position.x < 0 && _target.position.y < transform.position.y)
            {
                _enemycontroll.Move(-0.7f);
                animatorenemy.SetBool("isRunning",true);

            }
            else if (_target.transform.position.x - transform.position.x > 0 && _target.position.y < transform.position.y)
            {
                _enemycontroll.Move(0.7f);
                animatorenemy.SetBool("isRunning", true);

            }
            else if (_target.transform.position.x - transform.position.x < 0)
            {
                _enemycontroll.Move(-0.7f);
                animatorenemy.SetBool("isRunning", true);
                if (Mathf.Abs(_target.transform.position.y - transform.position.y) < 3)
                {
                    animatorenemy.SetBool("isRunning", false);
                    animatorenemy.SetBool("isJumping", true);
                    _enemycontroll.Jump();
                }
                    


            }
            else if (_target.transform.position.x - transform.position.x > 0)
            {
                _enemycontroll.Move(0.7f);
                animatorenemy.SetBool("isRunning", true);
                if (Mathf.Abs(_target.transform.position.y - transform.position.y) > 3)
                {
                    animatorenemy.SetBool("isRunning", false);
                    animatorenemy.SetBool("isJumping", true);
                    _enemycontroll.Jump();
                   
                }
                    
            }
            //if (!_pc._isGrounded)
          //  {
           //     animationcharacter.SetBool("isFalling", true);
          // }
           // if (_pc._isGrounded)
           // {
          //      animationcharacter.SetBool("isJumping", false);
          //      animationcharacter.SetBool("isFalling", false);
          //  }

            //transform.position = Vector2.MoveTowards(transform.position, _target.position, _enemycontroll._speed * Time.deltaTime);
            _enemycontroll.Flip();

        }else if (!_enemycontroll.attacking)
        {
            _enemycontroll.attackCd = 2f;
            _enemycontroll.Attack();
            animatorenemy.SetTrigger("Attack");
        }
        
        
        //_enemycontroll.Move(1f);
        // _enemycontroll.Flip();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            Debug.Log("Получен удар");
        }
    }
}
