using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  
    public float speed = 25f;
    public float scaleXCharacter;

    public float checkRadius;
    public float jumpForce=5f;
    public float extraJump;
    public int extraJumpsValue;
    public float countattack;
    public float attackComboCD=2f;
    public float attackRange = 0.5f;
    public int attackDamage = 50;
    public float prepareAttack = 0.25f;

    public int maxHeal = 100;
    public float currentHealth;


    public float enemyMoveDistance=4f;
    public float visionDistance = 8f;
    
    private void Awake()
    {
        currentHealth = maxHeal;
        scaleXCharacter = gameObject.transform.localScale.x;
      
    }



}
