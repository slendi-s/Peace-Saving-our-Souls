using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_back : MonoBehaviour
{

    public GameObject _play, _settings, _exit, _settingback,_settingwindow;

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        _play.gameObject.SetActive(true);
        _settings.gameObject.SetActive(true);
        _exit.gameObject.SetActive(true);
        _settingback.gameObject.SetActive(false);
        _settingwindow.gameObject.SetActive(false);
    }
}
