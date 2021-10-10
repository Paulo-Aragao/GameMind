using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _coins = 0;
    [Header("References")]
    [SerializeField] private GameObject _ragdoll;
    [SerializeField] private CinemachineFreeLook _cameraVitual;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _lifeBar;
    [SerializeField] private GameObject _deathScreen;

    [SerializeField] private TextMeshProUGUI _stars;

    private int _collectables;


    #endregion
    #region MonoBehavour
    private void Start() 
    {
        _lifeBar.maxValue = _hp;
        _lifeBar.value = _hp;
        _collectables = PlayerPrefs.GetInt("Stars",_collectables);
        _stars.text = _collectables.ToString();
    }     
    private void Update() 
    {
        if(transform.position.y < -20)
        {
            Die();
        }
    }
    #endregion
    #region Methods
    public void TakeDamage(int damage)
    {
        if(_animator.GetBool("MS_Stumbling")) return;
        _hp -= damage;
        _lifeBar.value = _hp;
        if(_hp < 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("Damage");
            _animator.SetBool("MS_Stumbling",true);
        }
    }
    public void EndStumbling()
    {
        _animator.SetBool("MS_Stumbling",false);
        _animator.SetBool("MS_Jumping",false);
        _animator.SetBool("MS_Rolling",false);
        ResetAllTriggers();
    }
    public void Collect()
    {
        _collectables++;
        int aux = 0;
        try{
            aux = PlayerPrefs.GetInt("Stars",_collectables);
        }
        catch{
            Debug.Log("First Stars Colleted, start saved in the disc");
        }
        PlayerPrefs.SetInt("Stars",aux + 1);
        _stars.text = _collectables.ToString();
    }
    private void ResetAllTriggers()
    {
        foreach (var param in _animator.parameters)
        {
        if (param.type == AnimatorControllerParameterType.Trigger)
        {
        _animator.ResetTrigger(param.name);
        }
    }
}
    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        _animator.enabled = false;
        _cameraVitual.LookAt = _ragdoll.transform;
        _cameraVitual.Follow = _ragdoll.transform;
        Invoke("EnableDieScreen",2f);
    }
    private void EnableDieScreen()
    {
        _deathScreen.SetActive(true);
    }
    #endregion
}
