using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Button_Exit : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }
    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        Application.Quit();
    }
}
