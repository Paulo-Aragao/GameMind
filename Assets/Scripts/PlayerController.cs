using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _coins = 0;
    [Header("References")]
    [SerializeField] private GameObject _ragdoll;
    [SerializeField] private CinemachineFreeLook _cameraVitual;
    [SerializeField] private Animator _animator;

    #endregion

    #region Methods
    public void TakeDamage(int damage)
    {
        if(_animator.GetBool("MS_Stumbling")) return;
        _hp -= damage;
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
        ResetAllTriggers();
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
    }
    #endregion
}
