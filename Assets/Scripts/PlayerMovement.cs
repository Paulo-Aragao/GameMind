using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Parameters")]
    [SerializeField] private float _jumpForce = 7.0f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _gravityFactor = 1f;
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;


    #endregion

    #region Constants

    private float _GRAVITYFORCE = 9.8f;

    #endregion
    private Vector3 _lastLookDiretion;
    private Vector3 _moveDirection;

    void Start()
    {
        _moveDirection = Vector3.zero;
        _lastLookDiretion = Vector3.up;
    }

    private void FixedUpdate() 
    {
        if (_characterController.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= _moveSpeed;
            if (Input.GetButton("Jump"))
            {
                _animator.SetTrigger("Jump");
                _moveDirection.y = _jumpForce;
            }
        }
        if(_moveDirection == Vector3.zero)
        {
            if(_lastLookDiretion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(new Vector3(_lastLookDiretion.x,0,_lastLookDiretion.z));
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_moveDirection.x,0,_moveDirection.z));
            _lastLookDiretion = _moveDirection;
        }
        _moveDirection.y -= _GRAVITYFORCE*_gravityFactor * Time.deltaTime;
        _animator.SetFloat("MoveSpeed",Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
