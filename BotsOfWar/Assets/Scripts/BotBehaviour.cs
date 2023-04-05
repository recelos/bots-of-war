using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    // These fields don't need to be private, but w/e
    [SerializeField]private float _speed = 3f;
    [SerializeField]private float _minDistanceToWall = 1f;
    private float _moveX;
    private float _moveY;
    private Vector3 _targetPosition;

    private void Start()
    {
        // Initialize the target position to a random point within the box
        _targetPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
    }

    private void Update()
    {
        // Calculate the movement direction towards the target position
        Vector3 movementDirection = (_targetPosition - transform.position).normalized;

        // Move the sprite towards the target position
        transform.position += movementDirection * _speed * Time.deltaTime;

        // If the sprite is close to a wall, evade it
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _minDistanceToWall); // Get list of all Colliders that fall within a circular area
        
        // Custom function to check for collisions with walls
        CheckForCollisions(colliders, ref movementDirection, ref _moveX, ref _moveY);

        // If the sprite has reached the target position, set a new random target position
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _targetPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
        }
    }

    private void CheckForCollisions(Collider2D[] colliders, ref Vector3 movementDirection, ref float moveX, ref float moveY)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Wall"))
            {
                // Calculate the evasion direction away from the wall
                Vector3 evasionDirection = (transform.position - collider.transform.position).normalized;

                // Randomize the evasion direction
                evasionDirection += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);

                // Set the new movement direction away from the wall
                movementDirection = evasionDirection.normalized;

                // Update the moveX and moveY values based on the new movement direction
                moveX = movementDirection.x;
                moveY = movementDirection.y;

                break;
            }
        }
    }
}
