using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPastMove : MonoBehaviour
{
    public float _speed;
    public float _jumpForce;
    private float _moveInput;

    private Rigidbody2D _rb;

    private bool facingRight = true;

    private bool _isGrounded;
    public Transform _groundCheck;
    public float _checkRadius;
    public LayerMask _whatIsGround;
    


    private int _extraJump;
    public int _extraJumpsValue;



    public float _koefCrouch = 0.6f;
    private BoxCollider2D _bc;
    private bool _isCrouch= false;

    private bool _sitDown;

    private bool _evasion;
    private float _speedEvasion = 10f;
    public float _evasionWay = 10f;
    private bool _walk;

    public GameObject _mainGG;
    private void Start()
    {
        _extraJump = _extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _mainGG = GetComponent<GameObject>();
    }
  
    private void FixedUpdate()
    {
       
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _sitDown = true;
        }
        else _sitDown = false;

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        if (_isGrounded && Input.GetKeyDown(KeyCode.S)) 
        {
            _isCrouch = true;
            _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y - _bc.size.y * (1 - _koefCrouch) / 2);
            _bc.size = new Vector2(_bc.size.x, _koefCrouch * _bc.size.y);

        }
        if (!_isGrounded && _isCrouch)
        {
            _isCrouch = false;
            _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y + (_bc.size.y / _koefCrouch - _bc.size.y) / 2);
            _bc.size = new Vector2(_bc.size.x, _bc.size.y / _koefCrouch);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _evasion = true;
          //  _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y - _bc.size.y * (1 - _koefCrouch) / 2);
           // _bc.size = new Vector2(_bc.size.x, _koefCrouch * _bc.size.y);

            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _evasion = false;
          //  _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y + (_bc.size.y / _koefCrouch - _bc.size.y) / 2);
          //  _bc.size = new Vector2(_bc.size.x, _bc.size.y / _koefCrouch);
        }
        if (_evasion)
        {
            _rb.AddForce(new Vector2(4000f, 0), ForceMode2D.Impulse);
        }
        
        if (!_sitDown || !_isGrounded) 
        {

            _moveInput = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);
         //   transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else _rb.velocity = new Vector2(0f, _rb.velocity.y);



        if (facingRight == false && _moveInput > 0)
        {
            _evasionWay = _evasionWay * -1f;
            Flip();
        }
        else if (facingRight == true && _moveInput < 0)
        {
            _evasionWay = _evasionWay * -1f;
            Flip();
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!_isCrouch && _isGrounded)
            {
                _isCrouch = true;
                _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y - _bc.size.y * (1 - _koefCrouch) / 2);
                _bc.size = new Vector2(_bc.size.x, _koefCrouch * _bc.size.y);



            }

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (_isCrouch)
            {
                _isCrouch = false;
                _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y + (_bc.size.y / _koefCrouch - _bc.size.y) / 2);
                _bc.size = new Vector2(_bc.size.x, _bc.size.y / _koefCrouch);
            }
        }
           
        
        if(_isGrounded == true) 
        {
            _extraJump = _extraJumpsValue;
        }  //Начисление прыжков


        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _extraJump > 0)
        {
            if (_isCrouch)
            {
                _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y + (_bc.size.y / _koefCrouch - _bc.size.y) / 2);
                _bc.size = new Vector2(_bc.size.x, _bc.size.y / _koefCrouch);
            }
            _isCrouch = false;

            _rb.velocity = Vector2.up * _jumpForce;
            _extraJump--;
        } else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _extraJump > 0 && _isGrounded == true)
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
        public void CrouchControl(bool _isNeedSitDown)
        {
            if (_isNeedSitDown && !_isCrouch)
            {
                _isCrouch = true;
                _bc.offset = new Vector2(_bc.offset.x, _bc.offset.y - _bc.size.y * (1 - _koefCrouch) / 2);
                _bc.size = new Vector2(_bc.size.x, _koefCrouch * _bc.size.y);

            }
        }
    }

