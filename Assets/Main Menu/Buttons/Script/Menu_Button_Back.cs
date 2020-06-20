using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Button_Back : MonoBehaviour
{
    public GameObject _play, _settings, _exit,_saveload1, _saveload2, _saveload3,_back;

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
        _saveload1.gameObject.SetActive(false);
        _saveload2.gameObject.SetActive(false);
        _saveload3.gameObject.SetActive(false);
        _back.gameObject.SetActive(false);
    }
}
