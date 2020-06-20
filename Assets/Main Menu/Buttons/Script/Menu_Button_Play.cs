using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu_Button_Play : MonoBehaviour
{ public GameObject _Play,_settings,_exit,_back,_saveload1,_saveload2,_saveload3;
    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }
    private void OnMouseUp()
    {
        SceneManager.LoadScene(1);
        transform.localScale = new Vector3(1f, 1f, 1f);
    //    _Play.gameObject.SetActive(false);
     //   _settings.gameObject.SetActive(false);
     //   _exit.gameObject.SetActive(false);
     //   _back.gameObject.SetActive(true);
     //   _saveload1.gameObject.SetActive(true);
     //   _saveload2.gameObject.SetActive(true);
     //   _saveload3.gameObject.SetActive(true);
    }
}
