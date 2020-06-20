using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(GameProcess))]
//[RequireComponent(typeof(Rigidbody2D))]
public class UnitController : MonoBehaviour
{
    [SerializeField] private GameProcess gameProcess;

    public Empty_Save_File empty_Save_File;

    public float speed = 3f;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private float goright = 1;
    public bool facingRight = true;
    public SpriteRenderer sr;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    public bool jumpstatus;
    public int extraJump;
    public int extraJumpsValue;



    public float koefCrouch = 0.6f;
    private BoxCollider2D bc;
    public bool isCrouch = false;

    private bool sitDown;

    private bool evasion;
    private float speedEvasion = 10f;
    public float evasionWay = 10f;
    private bool walk;

    public GameObject mainGG;


    public bool attacking = false;
    public float attackTimer = 0;
    public float attackCd = 0.3f;
    public Collider2D attackTrigger;

    public Transform walltrigger;
    public bool wallCheck;


    public BoxCollider2D climbTrigger;
    public bool climbCheck;

    private Vector3 triggerrotate;

    public bool dodgetrigger;
    private float dodgeTimer = 0;
    [SerializeField] public float dodgeCd = 0.3f;

    private float staminarecoveryCD = 1f;
    private float staminarecoveryTimer= 0;
    public bool staminarecovery;

    private float healplayer;
    public Image healbar;

    public float heal;
    public Image stamina;

    private int numberEnemy;
    public GameObject enemybot;
    public GameObject respawn;
    public GameObject player;

    private int score = 0;
    public TMP_Text scoretext;

    public GameObject escMenu;

    public Animator animatorcharacter;
    void Start()
    {
        stamina = GameObject.Find("StaminaBarValue").GetComponent<Image>();
        respawn = GameObject.Find("Respawn");
    //   _enemybot = Resources.Load<GameObject>("Enemy");
        heal = 100f;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        attackTrigger.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        climbTrigger = GetComponentInChildren<BoxCollider2D>();



        
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        wallCheck = Physics2D.OverlapCircle(walltrigger.position, checkRadius, whatIsGround);

        if (!staminarecovery)
        {
            if (staminarecoveryTimer > 0)
            {
                staminarecoveryTimer -= Time.deltaTime;
            }
            else if (staminarecoveryTimer <= 0)
            {
                staminarecovery = true;
               
                //
            }

        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;             
            }
        }
        if (dodgetrigger)
        {
            if (dodgeTimer > 0)
            {

                dodgeTimer -= Time.deltaTime;
                if (facingRight)
                {
                    Vector3 direction = transform.right * 2f;
                    transform.position = Vector3.Lerp(transform.position, transform.position + direction * goright, speed * Time.deltaTime * 1.5f);
                }
                else
                {
                    Vector3 direction = transform.right * -2f;
                    transform.position = Vector3.Lerp(transform.position, transform.position + direction * goright, speed * Time.deltaTime * 1.5f);
                }
                
            }
            else
            {
                // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _enemybot.GetComponent<Collider2D>(), ignore: false);
                Physics2D.IgnoreLayerCollision(9, 10, false);
                dodgetrigger = false;
            }
        }
       
    }
    public void Climb(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Ground"))
        {
            Debug.Log("Удар нанесен");
        }
    }
    public void WallStay(float distance )
    {
        if (wallCheck && !isGrounded)
        {
            
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            Vector3 direction = transform.right * distance;
        //    Move(0);
            //transform.position = Vector3.Lerp(transform.position, transform.position - direction, _speed * Time.deltaTime);
            //   transform.position = Vector2.Lerp(_rb.transform.position,_rb.transform.position, _speed * Time.deltaTime);

            // _rb.freezePosition;
            //  transform.position = Vector3.Lerp(transform.position, transform.position,0);

            //  transform.position new Vector2(_rb.transform.localPosition.x, _rb.transform.localPosition.y);
        }
        

        if (!wallCheck && facingRight)
        {
            extraJump = 2;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Vector3 direction = transform.right * distance;
            //transform.position = Vector3.Lerp(transform.position, transform.position + direction, _speed * Time.deltaTime);
            Move(0);

        } 
        
    }
    public void Move(float _direction)
    {
        if ((isCrouch) ||(dodgetrigger))
        {
            return;
        }
        
        Vector3 direction = transform.right * _direction;
        if (!wallCheck)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + direction * goright, speed * Time.deltaTime);
        }
        moveInput = _direction;
    }

    public void Jump()
    {
       

        if (isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump = extraJumpsValue - 1;
            jumpstatus = false;
        }
        else
        {
            if (extraJump > 0)
            {
                jumpstatus = true;
                rb.velocity = Vector2.up * jumpForce;
                extraJump--;
            }
            
        }
        
    }


   public void Crouch(bool isCrouch)
    {

        this.isCrouch = isCrouch;
        
        if (this.isCrouch)
        {
            bc.offset = new Vector2(bc.offset.x, bc.offset.y - bc.size.y * (1 - koefCrouch) / 2);
            bc.size = new Vector2(bc.size.x, koefCrouch * bc.size.y);

            
        }
        else
        {
            bc.offset = new Vector2(bc.offset.x, bc.offset.y + (bc.size.y / koefCrouch - bc.size.y) / 2);
            bc.size = new Vector2(bc.size.x, bc.size.y / koefCrouch);
        }

    }


    public void Dodge(float dodgedirection)
    {

        
        
        dodgetrigger = true;

        
        Physics2D.IgnoreLayerCollision(9,10, true);
        // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _enemybot.GetComponent<Collider2D>());
        // Debug.Log(GetComponent<Collider2D>());
        // Debug.Log(_enemybot.GetComponent<Collider2D>());

        dodgeTimer = dodgeCd;
         //   Vector3 direction = transform.right * 5f;
        //    transform.position = Vector3.Lerp(transform.position, transform.position + direction, _speed * Time.deltaTime);
        
        

        //  Vector3 direction = transform.right * dodgedirection;
        
        //_rb.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
        
    }
    public void Attack()
    {
        attacking = true;
        attackTimer = attackCd;
        attackTrigger.enabled = true;
        
       // _attackTriggerRight.enabled = true;
       
    }
    public void DamagePlayer(float damage)
    {
        healbar.fillAmount = healbar.fillAmount - damage;
        if (healbar.fillAmount <= 0)
        {
            GameOver();
        }
        animatorcharacter.SetTrigger("Damage");
    }

    public void Damage(float damage)
    {
        Debug.Log("Удар нанесен");
        heal = heal - damage;
        Debug.Log(heal);
        if (heal <= 0)
        {
            Dead();
        }
       
    }
   public void Dead()
    {
        gameProcess.ScoreValue(1);
      //  _scoretext.text = "Кол-во" + _score + 1;
        numberEnemy = numberEnemy - 1;
        if (numberEnemy <= 0)
        {
            enemybot.transform.position = respawn.transform.position;
            heal = 100;
           // Instantiate(Resources.Load<GameObject>("Enemy"), _respawn.transform.position, Quaternion.identity);
           
            

            numberEnemy = numberEnemy + 1;
            //Destroy(gameObject);
           // Physics2D.IgnoreCollision(_enemybot.GetComponent<Collider2D>(), _enemygate.GetComponent<Collider2D>(), ignore: true);
        }
      
        
       // _enemybot.gameObject.SetActive(false);
        
    }
    public void Flip()
    {
        if (!facingRight && moveInput > 0)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
            goright = goright * -1;
        }else if (facingRight && moveInput < 0)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
            goright = goright * -1;
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x)< 0.3f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    public void CooldownStamina()
    {
        staminarecovery = false;
        staminarecoveryTimer = staminarecoveryCD;
    }
    
    public void GameOver()
    {
        //_scoretext.text = "Количество очков равно очку " + _score;
        animatorcharacter.SetBool("Dead",true);
        var highscore = PlayerPrefs.GetInt("HighScore",0); 
        if (gameProcess._score > highscore)
        {
            highscore = gameProcess._score;
            PlayerPrefs.SetInt("HighScore", highscore);
            
        }

          scoretext.text = "Кол-во " + gameProcess._score + "\n" + "Лучший:" + highscore;
        escMenu.gameObject.SetActive(true);
        
      //  Debug.Log(_score);
        player.gameObject.SetActive(false) ;

    }
}

