using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Button_New_Game : MonoBehaviour
{
    public SpriteRenderer Logo;
    public GameObject NewGame, Returns, back_menu;

    

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }
    private void OnMouseUp()
    {
        Logo.color = new Color(0f, 0f, 0f, Mathf.Min(Time.time, 0f / 2f));
        transform.localScale = new Vector3(1f, 1f, 1f);
        NewGame.gameObject.SetActive(false);
        Returns.gameObject.SetActive(false);
        back_menu.gameObject.SetActive(false);

    }
    private void Update()
    {
      

    }
}
