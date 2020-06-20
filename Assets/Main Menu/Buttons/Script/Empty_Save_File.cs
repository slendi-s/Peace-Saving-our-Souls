using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty_Save_File : MonoBehaviour
{
    public GameObject _saveload1, _saveload2, _saveload3;

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }

    private void OnMouseUp()
    {
        
        transform.localScale = new Vector3(1f, 1f, 1f);
        Application.LoadLevel("Arena");

    }
}
