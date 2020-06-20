using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kyvirok : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;

    private bool _sitDowntrigger;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _sitDowntrigger = true;
        }
        else _sitDowntrigger = false;
        //_rb.AddForce( new Vector2(100f,2f),ForceMode2D.Impulse);
        if (_sitDowntrigger)
        {
            _rb.AddForce(new Vector2(40f, 0), ForceMode2D.Impulse);
        }
    }
}
