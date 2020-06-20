using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public LayerMask whoIam;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Rigidbody2D characterRB;
    public PolygonCollider2D stayCharacter;
    public PolygonCollider2D croutchCharacter;
    public Animator animatorCharacter;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Transform enemyTargetMoveA;
    public Transform enemyTargetMoveB;


}
