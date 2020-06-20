using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_start_value : MonoBehaviour
{
    public GameObject _start,_settings,_exit,_windowsetting,_windowsettingback, _saveload1,_saveload2,_saveload3,_buttonback;
    
    void Start()
    {
        _windowsetting.gameObject.SetActive(false);
        _windowsettingback.gameObject.SetActive(false);
        _saveload1.gameObject.SetActive(false);
        _saveload2.gameObject.SetActive(false);
        _saveload3.gameObject.SetActive(false);
        _buttonback.gameObject.SetActive(false);
    }
}
