using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public PlayerComponents playerComponents;
    public PlayerStatements playerStatements;
    public PlayerStats playerStats;

    private void Awake()
    {
        
    }
    void Update()
    {
        UnitAttackSystem.Instance.AttackComboCD(playerStatements, playerStats);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GameController.Instance.stamina.fillAmount < 0.20f)
            {
                return;
            }
            UnitAttackSystem.Instance.Attack(playerStatements,playerStats,playerComponents);
            GameController.Instance.SubstractionStamina(0.20f);
        }
       
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            var axis = Input.GetAxis("Horizontal");
            UnitMovementSystem.Instance.Flip(axis, gameObject, playerStats);
            UnitMovementSystem.Instance.RunAnimation(true,playerComponents);
            UnitMovementSystem.Instance.Move(playerComponents.characterRB, axis, playerStats.speed);
        }
        else
        {
            UnitMovementSystem.Instance.RunAnimation(false,playerComponents);
        }
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W))
        {
            if (GameController.Instance.stamina.fillAmount < 0.25f)
            {
                return;
            }
            UnitMovementSystem.Instance.JumpAnimation(playerStatements,playerComponents);
            UnitMovementSystem.Instance.Jump(playerComponents.characterRB,playerStatements,playerStats,playerComponents);
            GameController.Instance.SubstractionStamina(0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            UnitMovementSystem.Instance.CrouchAnimation(true);
            UnitMovementSystem.Instance.Crouch(true,playerStatements,playerComponents);
        }
        else
        {
            UnitMovementSystem.Instance.CrouchAnimation(false);
            UnitMovementSystem.Instance.Crouch(false,playerStatements,playerComponents);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UnitMovementSystem.Instance.Dodge(playerComponents.characterRB, playerStats.speed, Input.GetAxis("Horizontal"), 2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.Instance.openedmenu)
            {
                GameController.Instance.WriteResult(false);
            }else if (!GameController.Instance.openedmenu)
            {
                GameController.Instance.WriteResult(true);
            }
        }
    }
   
}


