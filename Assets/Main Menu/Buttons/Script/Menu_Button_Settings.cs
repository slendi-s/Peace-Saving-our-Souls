using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Button_Settings : MonoBehaviour
{
    public GameObject _windowsettings,_back,_play,_settings,_exit;
    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }
    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        _windowsettings.gameObject.SetActive(true);
        _back.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        _settings.gameObject.SetActive(false);
        _exit.gameObject.SetActive(false);
    }
}
