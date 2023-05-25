using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] Vector3 _target; // the target is calculated dynamically
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

        // random point generating, with random interval
        InvokeRepeating("InvokeGetRandomPointOnNavMesh", Random.Range(_interval - 1, _interval + 1), Random.Range(_interval - 1, _interval + 1));
    }

    private void Update()
    {
        //get all items in radius
        _itemsInViewRadius = _fieldOfView.FindVisibleTargets().Item2.ToArray();

        if (_itemsInViewRadius.Length > 0)
        {
            //if there is a target in radius
            _target = _itemsInViewRadius[0].position;
        }

        // if player is dead, bot stops so it doesn't play the animation of dying while moving XD
        if (_playerHealth.dead)
        {
            _target = transform.position;
        }
        _agent.SetDestination(_target);

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

    private void InvokeGetRandomPointOnNavMesh()
    {
        _target = NavMeshPoint.GetRandomPointOnNavMesh();
    }
}
