using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(UnitController))]
public class GameProcess : MonoBehaviour
{
    [SerializeField] private UnitController _unitController;
     public int _score=0;

    public void ScoreValue(int score)
    {
        _score = _score + score;
    }

    public void ScoreShow()
    {
       
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
