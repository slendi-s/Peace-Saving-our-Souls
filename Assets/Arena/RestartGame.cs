using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public GameObject player;
    public Image healthbarl;

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        player.SetActive(true);
        healthbarl.fillAmount = 1;
    }
}
