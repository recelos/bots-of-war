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

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _fieldOfView = GetComponent<FieldOfView>();

        // setup for nav mesh
        _agent.updatePosition = true;
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _agent.speed = _speed;

        // random point generating
        InvokeRepeating("InvokeGetRandomPointOnNavMesh", _interval, _interval);
    }

    private void Update()
    {
        //get all items in radius
        _itemsInViewRadius = _fieldOfView.FindVisiblePickups().ToArray();

        if (_itemsInViewRadius.Length > 0)
        {
            //if there is a target in radius
            _target = _itemsInViewRadius[0].position;
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
