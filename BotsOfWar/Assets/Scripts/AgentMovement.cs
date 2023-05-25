using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] Vector3 _target; // where the bot is going
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _interval; // interval between new point generating

    private NavMeshAgent _agent;
    private Vector3 _positionLastFrame;
    private Vector3 _positionCurrentFrame;

    // ATTACKING
    private bool _isAttacking = false; // current attack state
    private bool _isAttackingBefore = false; // last attack state
    private Transform _enemyTarget;
    private Vector2 _dirToEnemy;
    private BotShoot _botShoot;
    //

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _botShoot = GetComponent<BotShoot>();

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
        ActionDecide();
    }

    private void Walk()
    {
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

    // Some example of state machine
    private void ActionDecide()
    {
        if (_isAttacking)
        {
            if (!_isAttackingBefore)
            {
                StartCoroutine(Attack());
            }
        } // if the player hasn't already started attacking
        else if (!_isAttacking) // the player is not attacking
        {
            _agent.SetDestination(_target);
            Walk();
        }
    }

    public void CanAttack(bool isAttacking, Transform enemyTarget, Vector2 dirToTarget)
    {
        //_isAttackingBefore = isAttacking;
        _isAttacking = isAttacking;
        _enemyTarget = enemyTarget;
        _dirToEnemy = dirToTarget;
    }

    // Bot behaviour during attacking (player is moving during attack right now)
    private IEnumerator Attack()
    {
        _isAttackingBefore = true;
        while (_isAttacking)
        {
            var fireRatePerMinute = _botShoot.ShootAI(_dirToEnemy);
            yield return new WaitForSeconds(60/fireRatePerMinute);
            _agent.SetDestination(_target);
            Walk();
        }
        _isAttackingBefore = false;
    }

    private void InvokeGetRandomPointOnNavMesh()
    {
        _target = NavMeshPoint.GetRandomPointOnNavMesh();
    }
}
