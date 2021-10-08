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
    [SerializeField] private float _turnSmoothTime = 0.1f;

    [SerializeField] private float _turnSmoothVelocity = 0.1f;
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _camera;


    private bool _rolling = false;
    private bool _jumping = false;

    private float _moveSpeedOriginal;
    private float _moveSpeedRolling;

    private Vector3 _lastLookDiretion;
    private Vector3 _moveDirection;
    #endregion

    #region Constants

    private float _GRAVITYFORCE = 9.8f;

    #endregion
    

    void Start()
    {
        _moveSpeedOriginal = _moveSpeed;
        _moveSpeedRolling = _moveSpeed *1.7f;
        _rolling = false;
        _moveDirection = Vector3.zero;
        _lastLookDiretion = Vector3.up;
    }

    private void FixedUpdate() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float directionAngle = 0f; 
        Vector3 direction = new Vector3(horizontalInput,0,verticalInput).normalized;
        if (_characterController.isGrounded && !_rolling)
        {
            if(direction.magnitude >= 0.1f && !_jumping)
            {
                directionAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,directionAngle,ref _turnSmoothVelocity,_turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                _moveDirection = Quaternion.Euler(0f,directionAngle,0f) * Vector3.forward;
                
            }
            else if(!_jumping)
            {
                _moveDirection = Vector3.zero;
            }
            if (!_jumping && Input.GetButton("Jump"))
            {
                _animator.SetTrigger("Jump");
                //_moveDirection.y = _jumpForce;
            }
            else if (Input.GetButton("Fire1"))
            {
                _animator.SetTrigger("Roll");
            }
        }
        _animator.SetFloat("MoveSpeed",Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
        _moveDirection.y -= _GRAVITYFORCE*_gravityFactor * Time.deltaTime;
        _characterController.Move(_moveDirection *_moveSpeed* Time.deltaTime);
        
    }
    #region Methods

    public void StartJumping()
    {
        _jumping = true;
    }
    public void MidJumping()
    {
        //_moveDirection.y = _jumpForce;
    }
    public void EndJumping()
    {
        _animator.ResetTrigger("Jump");
        _jumping = false;
    }
    public void StartRolling()
    {
        _moveSpeed = _moveSpeedRolling;
        _rolling = true;
        Debug.Log("start");

    }
    public void MidRolling()
    {
        _moveSpeed = 0f;
        Debug.Log("mid");

    }
    public void EndRolling()
    {
        _moveSpeed = _moveSpeedOriginal;
        _rolling = false;
        _animator.ResetTrigger("Roll");
        Debug.Log("End");
    }
         
    #endregion
}
