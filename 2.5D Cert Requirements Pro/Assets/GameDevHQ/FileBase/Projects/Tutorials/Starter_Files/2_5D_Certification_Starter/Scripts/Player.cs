using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private bool _onLedge;


    //Ladder Mechanic
    [SerializeField] private bool _isClimbingUpLadder;
    [SerializeField] private bool _isClimbingDownLadder;
    [SerializeField] private bool _canClimbLadder;
    [SerializeField] private int _ladderPosition;
    [SerializeField] private float _ladderClimbSpeed;
    [SerializeField] private Transform[] _ladder1Waypoints;

    private Ledge _activeLedge;
    private Vector3 _direction, _velocity;

    private float _yVelocity;
    private float _horizontalInput;
    private float _verticalInput;

    


    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();  
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        CalculateMovement();
        HandleLadderclimb();
        HandleRoll();

        if (_onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger("ClimbUp");
            }
        }
    }

    private void FixedUpdate()
    {
        if (_characterController.isGrounded == false)
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        if (_isClimbingUpLadder == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _ladder1Waypoints[1].position, _ladderClimbSpeed * Time.deltaTime);

            if (transform.position == _ladder1Waypoints[1].position)
            {
                _characterController.enabled = true;
                _isClimbingUpLadder = false;
                _animator.SetBool("ClimbingLadder", false);
            }
        }

        if (_isClimbingDownLadder == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _ladder1Waypoints[0].position, _ladderClimbSpeed * Time.deltaTime);

            if (transform.position == _ladder1Waypoints[0].position)
            {
                _characterController.enabled = true;
                _isClimbingDownLadder = false;
                _animator.SetBool("ClimbingLadder", false);
            }
        }
    }

    void CalculateMovement()
    {
        _direction = new Vector3(0, 0, _horizontalInput);
        _velocity = _direction * _movementSpeed;

        if (_characterController.isGrounded == true && _characterController.enabled == true)
        {
            _animator.SetBool("isJumping", false);
            _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));

            if (_horizontalInput == 1)
            {
                _playerModel.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (_horizontalInput == -1)
            {
                _playerModel.transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetBool("isJumping", true);
                _yVelocity = _jumpHeight;
            }
        }
        _velocity.y = _yVelocity;

        if (_characterController.enabled == true && _isClimbingUpLadder == false && _isClimbingDownLadder == false)
        {
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }

    private void HandleLadderclimb()
    {
        if (_canClimbLadder == true && _characterController.isGrounded)
        {
            if (_ladderPosition == 1)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.position = _ladder1Waypoints[0].position;
                    _playerModel.transform.eulerAngles = new Vector3(0, 0, 0);
                    _characterController.enabled = false;
                    _isClimbingUpLadder = true;
                    _animator.SetBool("ClimbingLadder", true);
                }
            }
            else if (_ladderPosition == 2)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = _ladder1Waypoints[1].position;
                    _playerModel.transform.eulerAngles = new Vector3(0, 0, 0);
                    _characterController.enabled = false;
                    _isClimbingDownLadder = true;
                    _animator.SetBool("ClimbingLadder", true);
                }
            }
        }
    }


    //Ledge Climb Mechanic
    public void GrabLedge(GameObject handPos, Ledge currentLedge)
    {
        _characterController.enabled = false;
        _animator.SetBool("GrabLedge", true);
        _animator.SetBool("isJumping", true);
        _animator.SetFloat("Speed", 0f);
        _onLedge = true;
        transform.position = handPos.transform.position;
        _activeLedge = currentLedge;
    }

    //Ledge Climb Mechanic
    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _animator.SetBool("GrabLedge", false);
        _characterController.enabled = true;
    }

    //Ladder Climb Mechanic
    public void LadderClimbEnter(int LadderPosition)
    {
        _ladderPosition = LadderPosition;
        _canClimbLadder = true;
    }

    //Ladder Climb Mechanic
    public void LadderClimbExit()
    {
        _canClimbLadder = false;
    }


    //Roll Mechanic
    private void HandleRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _animator.SetBool("isRolling", true);
            StartCoroutine(CalculateRoll());
        }
    }

    //Roll Mechanic
    private IEnumerator CalculateRoll()
    {
        yield return new WaitForSeconds(1);
    }
}
