using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Parameters")]
    [SerializeField] private float _jumpForce = 7.0f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _moveSpeedRun = 10f;

    [SerializeField] private float _gravityFactor = 1f;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    [SerializeField] private float _turnSmoothVelocity = 0.1f;
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineFreeLook _cameraVitual;
    [SerializeField] private Transform _cameraBrain;

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
        _animator.SetBool("MS_Rolling",false);
        _animator.SetBool("MS_Jumping",false);
        _moveDirection = Vector3.zero;
        _lastLookDiretion = Vector3.up;
    }

    private void FixedUpdate() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float directionAngle = 0f; 
        Vector3 direction = new Vector3(horizontalInput,0,verticalInput).normalized;
        //Verificação de maquina de estados e se o player está no chão
        if (_characterController.isGrounded && !_animator.GetBool("MS_Rolling") && !_animator.GetBool("MS_Jumping") )
        {
            if(direction.magnitude >= 0.1f && ! _animator.GetBool("MS_Jumping"))
            {
                directionAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + _cameraBrain.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,directionAngle,ref _turnSmoothVelocity,_turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                _moveDirection = Quaternion.Euler(0f,directionAngle,0f) * Vector3.forward;
                _animator.ResetTrigger("Roll");
            }
            else
            {
                _moveDirection = Vector3.zero;
            }
            if (Input.GetButton("TriggerR2"))
            {
                _animator.SetBool("Running",true);
                _moveSpeed = _moveSpeedRun;
            }
            else
            {
                _animator.SetBool("Running",false);
                _moveSpeed = _moveSpeedOriginal;
            }
            if (Input.GetButton("Jump"))
            {
                _animator.SetTrigger("Jump");
                //_moveDirection.y = _jumpForce;
            }
            else if (Input.GetButton("Fire1"))
            {
                _animator.SetTrigger("Roll");
            }
        }
        if(_animator.GetBool("MS_Stumbling"))
        {
            _moveDirection = new Vector3(_moveDirection.x/2,_moveDirection.y,_moveDirection.z/2);
        }
        _animator.SetFloat("MoveSpeed",Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
        _moveDirection.y -= _GRAVITYFORCE*_gravityFactor * Time.deltaTime;
        _characterController.Move(_moveDirection *_moveSpeed* Time.deltaTime);
        
    }
    #region Methods

    public void StartJumping()
    {
        _animator.SetBool("MS_Jumping",true);
    }
    public void MidJumping()
    {
        _moveDirection.y = _jumpForce;
    }
    public void EndJumping()
    {
        _animator.ResetTrigger("Jump");
        _animator.SetBool("MS_Jumping",false);
    }
    public void StartRolling()
    {
        _characterController.center =  _characterController.center/2;
        _characterController.height = 0f;
        _moveSpeed = _moveSpeedRolling;
        _animator.SetBool("MS_Rolling",true);
        Debug.Log("start");

    }
    public void MidRolling()
    {
        _moveSpeed = 0f;
        Debug.Log("mid");

    }
    public void EndRolling()
    {
        _characterController.center =  _characterController.center*2;
        _characterController.height = 1.63f;
        _moveSpeed = _moveSpeedOriginal;
        _animator.SetBool("MS_Rolling",false);
        _animator.ResetTrigger("Roll");
        Debug.Log("End");
    }
         
    #endregion
}
