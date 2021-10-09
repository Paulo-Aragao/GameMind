using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleNumbers : MonoBehaviour
{
    #region Variables
    [Header("Parameters")]
    [SerializeField] private float _coolDownChangerNumber = 2f;
    [SerializeField] private float _coolDownChangerPlane = 2f;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _number;
    [SerializeField] private GameObject _oddPlane;
    [SerializeField] private GameObject _pairPlane;

    private int _numberInt = 0;
    private int _rounds = 0;
    private float _timeToChangeNumber = 0f;
    private float _timeToChangePlane = 0f;
    private bool _inChoiceTime = false;
    #endregion
    #region MonoBehaviour
    private void Start() 
    {
        _timeToChangeNumber = _coolDownChangerNumber;        
    }

    private void Update() 
    {
        if(!_inChoiceTime && Time.time > _timeToChangeNumber)
        {
            ChoiceTime();
        }
        if(!_inChoiceTime)
        {
            _numberInt = Random.Range(0,100);
            _number.text = _numberInt.ToString();
        }
        if(_inChoiceTime && Time.time > _timeToChangePlane)
        {
            Result();
        }
    }
    private void ChoiceTime()
    {
        _rounds++;
        _inChoiceTime = true;
        _timeToChangePlane = Time.time + _coolDownChangerPlane;
    }
    private void Result()
    {
        if(_numberInt % 2 != 0)
        {
            _oddPlane.SetActive(false);
        }
        else
        {
            _pairPlane.SetActive(false);
        }
        Invoke(nameof(Restart),1f);
    }

    private void Restart()
    {
        _pairPlane.SetActive(true);
        _oddPlane.SetActive(true);
        _inChoiceTime = false;
        _timeToChangeNumber = Time.time + _coolDownChangerNumber;
        if(_rounds == 5)
        {
            _coolDownChangerPlane = 1;
        }
        if(_rounds == 10)
        {
            //TODOwin
        }
    }
    #endregion
}
