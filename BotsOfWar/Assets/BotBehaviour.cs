using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minDistanceToWall = 1f;

    private float moveX;
    private float moveY;
    private Vector3 targetPosition;

    private void Start()
    {
        // Initialize the target position to a random point within the box
        targetPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
    }

    private void Update()
    {
        // Calculate the movement direction towards the target position
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        // Move the sprite towards the target position
        transform.position += movementDirection * speed * Time.deltaTime;

        // If the sprite is close to a wall, evade it
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, minDistanceToWall);
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

                break;
            }
        }

        // Update the moveX and moveY values based on the new movement direction
        moveX = movementDirection.x;
        moveY = movementDirection.y;

        // If the sprite has reached the target position, set a new random target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
        }
    }
}
