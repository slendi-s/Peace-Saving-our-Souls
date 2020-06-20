using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_move : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Rigidbody2D _rb;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _radCir = 0.3f;
    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private int _extraJump;
    [SerializeField] private int _extraJumpValue;
    void Start()
    {
        _extraJump = _extraJumpValue;
        _rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (_isGrounded == true)
        {
            _extraJump = _extraJumpValue;
        }

        //  transform.localScale.y += 0.01f;
      //  transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f, transform.localScale.z);
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f, transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))// && _extraJump>0)
        {
            Debug.Log("Прыжков"+_extraJump);
            // _rb.velocity = Vector2.up * _jumpForce;
           _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _extraJump--;
          //  _rb.velocity = Vector2.up * _jumpForce;
            // _rb.velocity = Vector2.up * _jumpForce;
        }else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && _extraJump == 0 && _isGrounded == true)
        {
          //  _rb.velocity = Vector2.up * _jumpForce;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);

        Debug.Log(_extraJump);

        float _moveX = 0;
        if ((Input.GetKey (KeyCode.A)) || (Input.GetKey(KeyCode.D)))
        {
            _moveX = Input.GetAxis("Horizontal");
            
        }
        _rb.MovePosition(_rb.position + Vector2.right * _speed * _moveX * Time.deltaTime);



    }
}
