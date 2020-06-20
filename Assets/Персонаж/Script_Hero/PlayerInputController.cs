using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(UnitController))]
public class PlayerInputController : MonoBehaviour
{
    private GameObject _enemy;
    public Image _healbar;
    public Image _staminabar;
    private float _healbarfill;
    private float _staminabarfill;
  
    private bool _firstjump;
    private UnitController _pc;

    private float _jumpvalue = 0.1f;
    private float _dodgevalue = 0.3f;
    private float _attackvalue = 0.2f;
    private bool OnOffMenu = true;

    public Animator animationcharacter;
    void Start()
    {
        _enemy = GameObject.Find("Enemy");
        _pc = GetComponent<UnitController>();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnOffMenu)
            {
                _pc.escMenu.gameObject.SetActive(true);
                OnOffMenu = !OnOffMenu;
            }
            else
            {
                _pc.escMenu.gameObject.SetActive(false);
                OnOffMenu = !OnOffMenu;
            }

               

        }
        if (_pc.staminarecovery)
        {
            _pc.stamina.fillAmount = _pc.stamina.fillAmount + Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Instantiate(_enemy,transform.position,Quaternion.identity);
        }
        
        var axis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(axis) > 0.01f)
        {
            if (_pc.attacking)
            {
                return;
            }
            _pc.Move(axis);
            _pc.Flip();
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_staminabar.fillAmount > _dodgevalue)
                {
                    animationcharacter.SetTrigger("Dodge");
                }
                else
                {
                    animationcharacter.SetTrigger("Idle");
                }
            }
           if (_pc.isGrounded)
            {
                animationcharacter.SetBool("isRunning", true);
            }
           
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D)))
            {
                _pc.Move(0);
                _pc.Flip();
                if (!_pc.isGrounded)
                {
                    return;
                }
                if (_pc.isGrounded)
                {
                    animationcharacter.SetBool("isRunning", true);
                }
                
            }
            else
            {
                animationcharacter.SetBool("isRunning", false);
            }
        }
        
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (_pc.attacking)
            {
                return;
            }
            if (_staminabar.fillAmount > _jumpvalue)
            {
                _pc.Jump();
                animationcharacter.SetBool("isFalling", false);
                animationcharacter.SetBool("isJumping", true);
                if (_pc.extraJump > 0)
                {
                    _staminabar.fillAmount -= _jumpvalue;
                    _firstjump = true;
                    _pc.CooldownStamina();
                    

                }else if ((_pc.extraJump == 0) && (_firstjump))
                {
                    _staminabar.fillAmount -= _jumpvalue;
                    _firstjump = false;
                    _pc.CooldownStamina();
                    
                }
            }
        }
        else 
        {
            if (!_pc.isGrounded)
            {
                animationcharacter.SetBool("isFalling", true);
            }
            if (_pc.isGrounded)
            {
                animationcharacter.SetBool("isJumping", false);
                animationcharacter.SetBool("isFalling", false);
            }    
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _pc.Crouch(true);
            animationcharacter.SetBool("isSitDown", true);
            

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            _pc.Crouch(false);
            animationcharacter.SetBool("isSitDown", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_pc.attacking)
            {
                return;
            }
            if (_pc.isCrouch)
            {
                return;
            }
            if (_staminabar.fillAmount > _dodgevalue)
            {
                _pc.Dodge(axis);
                _staminabar.fillAmount -= _dodgevalue;
                _pc.CooldownStamina();
                animationcharacter.SetTrigger("Dodge");
            }
            else
            {
                animationcharacter.SetTrigger("Idle");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_pc.attacking)
        {
            if ((_pc.isCrouch) || (_pc.dodgetrigger) || (!_pc.isGrounded))
            {
                return;
            }
           
            if (_staminabar.fillAmount > _attackvalue)
            {

                _pc.Attack();
                _staminabar.fillAmount -= _attackvalue;
                _pc.CooldownStamina();
                animationcharacter.SetTrigger("AttackComboOne");
            }
            else
            {
                animationcharacter.SetTrigger("Idle");
            }
        }
      //  if (_pc._wallCheck)
     //   {
     //       _pc.WallStay(axis);
     //   }
        
    }
    

}
