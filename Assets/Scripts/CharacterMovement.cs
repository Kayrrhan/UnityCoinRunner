using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private Animator _characterAnimator;

    // private static string IsWalking = "isWalking";
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private float _vertical;
    private float _horizontal;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    void FixedUpdate()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _characterAnimator.SetBool(IsWalking,_vertical != 0 || _horizontal != 0);
        _currentPosition = transform.position;
        _targetPosition = _currentPosition + new Vector3(_horizontal,0,_vertical);
        transform.LookAt(_targetPosition);
        transform.position = Vector3.MoveTowards(_currentPosition,_targetPosition,_speed*Time.fixedDeltaTime);
    }
}
