using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public PlayerComponents playerComponents;
    public PlayerStatements playerStatements;
    public PlayerStats playerStats;


    private float goLeft=-1;
    private float directionA;
    
    private Vector2 wasPositionA;
    private Vector2 wasPositionB;
    private Collider2D[] touchMovePointA;
    private Collider2D[] touchMovePointB;


    private Vector2 beginSeePoint;
    private Vector2 endSeePoint;

    private Transform target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        CalculateDistance(playerStats,playerComponents,gameObject);
        ISeeYou(playerComponents,playerStats);


    }
    private void Update()
    {

        if (playerStatements.isDead)
            return;

        if (Vector2.Distance(transform.position, target.position) < 3)
        {
            UnitAttackSystem.Instance.Attack(playerStatements, playerStats, playerComponents);
            UnitAttackSystem.Instance.AttackComboCD(playerStatements, playerStats);
        }

        if (target.position.x - gameObject.transform.position.x > 0)
        {
            goLeft = 1;
            UnitMovementSystem.Instance.RunAnimation(true, playerComponents);
            UnitMovementSystem.Instance.Move(playerComponents.characterRB, goLeft, playerStats.speed);
        } else if (target.position.x - gameObject.transform.position.x < 0)
        {
            goLeft = -1;
            UnitMovementSystem.Instance.RunAnimation(true, playerComponents);
            UnitMovementSystem.Instance.Move(playerComponents.characterRB, goLeft, playerStats.speed);
        }

        

      //  UnitAttackSystem.Instance.Attack(playerStatements, playerStats, playerComponents);
      //    UnitAttackSystem.Instance.AttackComboCD(playerStatements, playerStats);

        Debug.Log(wasPositionA);

        // MoveEnemy(playerComponents, touchMovePointA, touchMovePointB);


        UnitMovementSystem.Instance.Flip(goLeft,gameObject,playerStats);
        //  Debug.Log(Vector2.Distance(transform.position, playerComponents.playerCharacter.transform.position));
        //  if (Vector2.Distance(transform.position, playerComponents.playerCharacter.transform.position)< 4f)
        //  {
        //      UnitAttackSystem.Instance.Attack(playerStatements, playerStats, playerComponents);
        //      UnitAttackSystem.Instance.AttackComboCD(playerStatements, playerStats);
        //  }    

    }
    public void ISeeYou(PlayerComponents _pComponents, PlayerStats _pStats)
    {
       
        beginSeePoint = new Vector2(_pComponents.groundCheck.position.x, _pComponents.groundCheck.position.y+1.5f);
        endSeePoint = new Vector2(beginSeePoint.x-_pStats.visionDistance, beginSeePoint.y);
       // Gizmos.DrawLine(beginSeePoint, endSeePoint);
        // var sd Physics2D.LinecastAll(playerComponents.groundCheck);
    }
    public void MoveEnemy(PlayerComponents _pComponents,Collider2D[] _touchMovePointA, Collider2D[] _touchMovePointB)
    {
        if (goLeft == -1)
        {
            UnitMovementSystem.Instance.RunAnimation(true, playerComponents);
            UnitMovementSystem.Instance.Move(playerComponents.characterRB, goLeft, playerStats.speed);
            _touchMovePointA = Physics2D.OverlapCircleAll(wasPositionA, 0.5f, _pComponents.whoIam);
            foreach (Collider2D enemy in _touchMovePointA)
            {
                goLeft = 1;
            }
        }else if (goLeft == 1)
        {
            UnitMovementSystem.Instance.RunAnimation(true, playerComponents);
            UnitMovementSystem.Instance.Move(playerComponents.characterRB, goLeft, playerStats.speed);
            _touchMovePointB = Physics2D.OverlapCircleAll(wasPositionB, 0.5f, _pComponents.whoIam);
            foreach (Collider2D enemy in _touchMovePointB)
            {
                goLeft = -1;
            }  
        }
    }
    public void CalculateDistance(PlayerStats _pStats,PlayerComponents _pComponents,GameObject _gameObject)
    {

        
        
        directionA = _pStats.enemyMoveDistance / 2;

       // wasPositionA = new Vector2(_pComponents.groundCheck.transform.localPosition.x - directionA, _pComponents.groundCheck.transform.localPosition.y);
       // wasPositionB = new Vector2(_pComponents.groundCheck.transform.localPosition.x + directionA*2, _pComponents.groundCheck.transform.localPosition.y);

        wasPositionA = new Vector2(_gameObject.transform.position.x - directionA, _pComponents.groundCheck.transform.localPosition.y);
        wasPositionB = new Vector2(_gameObject.transform.position.x + directionA, _pComponents.groundCheck.transform.localPosition.y);

        // playerComponents.enemyTargetMoveA.position = wasPositionA;
        //  playerComponents.enemyTargetMoveB.position = wasPositionB;

        //  
        //  
        // _pComponents.enemyTargetMoveA.transform.localPosition = new Vector2(wasPositionA - distanceA, _pComponents.enemyTargetMoveA.transform.localPosition.y);
        //  var distanceB = _pStats.enemyMoveDistance / 2;
        //  var wasPositionB = _pComponents.enemyTargetMoveB.transform.localPosition.x;
        //  _pComponents.enemyTargetMoveB.transform.localPosition = new Vector2(wasPositionB + distanceB, _pComponents.enemyTargetMoveB.transform.localPosition.y);


    }

}
