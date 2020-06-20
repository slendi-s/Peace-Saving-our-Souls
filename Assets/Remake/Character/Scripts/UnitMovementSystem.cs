using UnityEngine;

public class UnitMovementSystem : MonoBehaviour
{
    public PlayerComponents playerComponents;
    public PlayerStatements playerStatements;
    public PlayerStats playerStats;
   

    public static UnitMovementSystem Instance { get; private set; }
    private void Awake()
    {
        CreateSingleton();
      //  CheckDirection();
    }

    private bool CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }

    public void FixedUpdate()
    {
        CheckGround(playerStatements,playerStats,playerComponents);
    }
    public void Move(Rigidbody2D targetRB, float direction, float speed)
    {
        if (playerStatements.isCroutch)
        {
            return;
        }
        
        targetRB.velocity = new Vector2(direction*speed,targetRB.velocity.y);

    }

    public void Flip(float direction, GameObject character,PlayerStats _pStats)
    {
        
        if (direction < 0)
        {
            character.transform.localScale = new Vector2(-_pStats.scaleXCharacter, character.transform.localScale.y);
        }
        else
        {
            character.transform.localScale = new Vector2(_pStats.scaleXCharacter, character.transform.localScale.y);
        }
    }
    private void CheckDirection()
    {
        if (gameObject.transform.rotation.y == 0)
        {
            playerStatements.isRight = true;
        }
        else
        {
            playerStatements.isRight = false;
        }
            
    }

    public void Jump(Rigidbody2D targetRB,PlayerStatements _pStatements,PlayerStats _pStats,PlayerComponents _pComponents)
    {
        CheckGround(_pStatements,_pStats,_pComponents);
        if (_pStatements.isCroutch)
        {
            return;
        }
        if (_pStatements.isGrounded)
        {
            
            targetRB.velocity = Vector2.up * _pStats.jumpForce;
            _pStats.extraJump = _pStats.extraJumpsValue - 1 ;
            _pStatements.isJump = false;
        }
        else
        {
            if (_pStats.extraJump > 1)
            {
                _pStatements.isJump = true;
                targetRB.velocity = Vector2.up * _pStats.jumpForce;
                _pStats.extraJump--;
            }

        }
    }
    private void CheckGround(PlayerStatements _pStatements,PlayerStats _pStats, PlayerComponents _pComponents)
    {
        _pStatements.isGrounded = Physics2D.OverlapCircle(_pComponents.groundCheck.position,
        _pStats.checkRadius, _pComponents.whatIsGround);
    }
    public void Crouch(bool isCrouch,PlayerStatements _pStatements,PlayerComponents _pComponents)
    {
        _pStatements.isCroutch = isCrouch;
        if (isCrouch)
        {
            _pComponents.stayCharacter.enabled = false;
            _pComponents.croutchCharacter.enabled = true;
        }
        else
        {
            _pComponents.stayCharacter.enabled = true;
            _pComponents.croutchCharacter.enabled = false;
        }
         
        
    }
    
    public void Dodge(Rigidbody2D targetRB,float speed, float direction, float timedodge)
    {
        while (timedodge>0)
        {
            Debug.Log("хай");
            timedodge -= timedodge;
        }
        if (timedodge>0)
        {
            Debug.Log(timedodge);
            targetRB.velocity = new Vector2(direction * speed, targetRB.velocity.y);
        }else
        {
            targetRB.velocity = new Vector2(direction * 0, targetRB.velocity.y);
        }
        
        
    }
    public void RunAnimation(bool isRinning,PlayerComponents _pComponents)
    {
        _pComponents.animatorCharacter.SetBool("isRunning", isRinning);
    }
    public void JumpAnimation(PlayerStatements _pStatements, PlayerComponents _pComponents)
    {
        if (_pStatements.isCroutch)
        {
            return;
        }
        if (_pStatements.isGrounded)
        {
            _pComponents.animatorCharacter.SetTrigger("jump");
        }
        
    }
    public void CrouchAnimation(bool isCrouch)
    {
        playerComponents.animatorCharacter.SetBool("isCrouch", isCrouch);
    }
}