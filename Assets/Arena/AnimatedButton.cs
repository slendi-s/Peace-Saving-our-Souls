using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class AnimatedButton : MonoBehaviour
{
    public Vector3 ScaledSize = new Vector3(1.15f, 1.15f, 1.15f);
    private void OnMouseDown()
    {
        transform.localScale = ScaledSize;
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
