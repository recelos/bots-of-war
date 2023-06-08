using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentMovement : MonoBehaviour
{
    public Vector3 Target {get;set;} // user sets where to move bot
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _interval; // interval between new point generating

    private NavMeshAgent _agent;
    private Vector3 _positionLastFrame;
    private Vector3 _positionCurrentFrame;

    private FieldOfView _fieldOfView;
    private Transform[] _itemsInViewRadius;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _fieldOfView = GetComponent<FieldOfView>();
        _playerHealth = GetComponent<PlayerHealth>();

        // setup for nav mesh
        _agent.updatePosition = true;
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _agent.speed = _speed;

    }

    private void Update()
    {
        

        // if player is dead, bot stops so it doesn't play the animation of dying while moving XD
        if (_playerHealth.dead)
        {
            Target = transform.position;
        }
        _agent.SetDestination(Target);

        // Split the character based on the walk direction
        _positionLastFrame = _positionCurrentFrame;
        _positionCurrentFrame = transform.position;
        var playerDiretion = (_positionCurrentFrame - _positionLastFrame).normalized;

        if (playerDiretion.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (playerDiretion.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;

        // animations
        _animator.SetFloat("Horizontal", playerDiretion.x);
        _animator.SetFloat("Vertical", playerDiretion.y);
        _animator.SetFloat("Magnitude", playerDiretion.magnitude);
    }

    
}
