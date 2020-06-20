using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public PlayerStats playerStats;

    public GameObject playerCharacter;
    public GameObject enemyCharacter;

    public Image health;
    public Image stamina;

    public GameObject menu;

    public float score=0;
    public float bestscore;
    public Text scoretext;
    public Text bestscoretext;
    public bool openedmenu;

    private GameObject enemy;
    private GameObject mainHero;

    private float beginTimer=5;
    private float timerChanged;

    private float afk;
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        WriteResult(false);
        CreateSingleton();
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
    private void Start()
    {
        




        timerChanged = beginTimer;
        enemy = enemyCharacter;
        mainHero = playerCharacter;
    }
    public void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat("BestScore"));
        if (health.fillAmount <= 0)
        {
            WriteResult(true);
        }

        afk -= Time.deltaTime;

        if (afk<=0)
        {
            RecoverStamina(Time.deltaTime / 2);
        }
        SpawnEnemy();
       
        health.fillAmount = playerStats.currentHealth/100;
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            enemy = Instantiate(enemyCharacter, new Vector3(29.97f, 1, 0), Quaternion.identity); 
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            mainHero=Instantiate(playerCharacter, new Vector3(-3, 1, 0), Quaternion.identity);
        }
    }
    public void RecoverStamina(float vol)
    {
        stamina.fillAmount += vol;
    }
    public void SpawnEnemy()
    {
        timerChanged -= Time.deltaTime;
        if (timerChanged <= 0)
        {
            timerChanged = beginTimer;
            beginTimer -= 0.25f;
            if (beginTimer <= 0)
            {
                beginTimer = 5;
                enemy = Instantiate(enemyCharacter, new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
                enemy = Instantiate(enemyCharacter, new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
                return;
            }
            enemy = Instantiate(enemyCharacter, new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
        }
    }

    public void WriteResult(bool on)
    {
        if (on)
        {
            
            
   
            menu.SetActive(true);
            
            

            openedmenu = true;
            
            if (PlayerPrefs.GetFloat("BestScore") < score)
            {
                PlayerPrefs.SetFloat("BestScore", score);

            }
            bestscoretext.text = string.Format("РЕКОРД: {0}", PlayerPrefs.GetFloat("BestScore"));
            scoretext.text = string.Format("СЧЕТ: {0}", score);
            
            

        }
        if (!on)
        {
            openedmenu = false;
            menu.SetActive(false);
        }
        
    }
    public void HealthCalculate(float damage)
    {
        health.fillAmount -= damage / 100;
    }

    public void SubstractionStamina(float vol)
    {
        afk = 2;
        stamina.fillAmount -= vol;
    }
}
