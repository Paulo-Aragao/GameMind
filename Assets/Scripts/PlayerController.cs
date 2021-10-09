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


    #endregion

    #region Methods
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if(_hp < 0)
        {
            Die();
        }
    }
    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<Animator>().enabled = false;
        _cameraVitual.LookAt = _ragdoll.transform;
        _cameraVitual.Follow = _ragdoll.transform;
    }
    #endregion
}
