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
    [SerializeField] private Transform _camera;


    private bool _rolling = false;
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
        Vector3 direction = new Vector3(horizontalInput,0,verticalInput).normalized;
        if(direction.magnitude >= 0.1f)
        {
            float directionAngle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;
        }
        /*
        if (_characterController.isGrounded && !_rolling)
        {
            if(Input.GetAxis("Vertical") != 0f )
            {
                _moveDirection = (transform.position - _camera.position) * Input.GetAxis("Vertical");
                
            }
            if(Input.GetAxis("Horizontal") != 0f)
            {
                _moveDirection = _moveDirection + new Vector3(Input.GetAxis("Horizontal"),0,0);
            }
            if(Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f)
            {
                _moveDirection = Vector3.zero;
            }
            //_moveDirection *= _moveSpeed;
            if (Input.GetButton("Jump"))
            {
                _animator.SetTrigger("Jump");
                _moveDirection.y = _jumpForce;
            }
            else if (Input.GetButton("Fire1"))
            {
                _animator.SetTrigger("Roll");
            }
        }*/
        if(new Vector3(direction.x,0,direction.z) == Vector3.zero)
        {
            if(new Vector3(_lastLookDiretion.x,0,_lastLookDiretion.z) != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(new Vector3(_lastLookDiretion.x,0,_lastLookDiretion.z));
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
            _lastLookDiretion = direction;
        }
        direction.y -= _GRAVITYFORCE*_gravityFactor * Time.deltaTime;
        _animator.SetFloat("MoveSpeed",Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
        _characterController.Move(direction *_moveSpeed* Time.deltaTime);
    }
    #region Methods
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
