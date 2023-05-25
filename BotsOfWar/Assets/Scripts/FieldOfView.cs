using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float _radius = 5f;
    [SerializeField] float _angle = 360f;
    [SerializeField] LayerMask _obstacleMask; // Layer mask for obstacles, e.g. walls, bot doesn't see through them

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    // Find targets every x seconds, so it doesn't have to be done every frame
    public IEnumerator<WaitForSeconds> FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
            FindVisiblePickups();
        }
    }

    // Field of view in 2D space, only used for drawing in editor
    public Vector2 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Get all targets in radius
    public List<Transform> FindVisibleTargets()
    {
        var visibleTargets = new List<Transform>();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            var target = targetCollider.transform;

            // Skip if the target is the same as the object this script is attached to
            // or if the target is not another bot
            if (target == transform || target.tag != "Player")
                continue;

            Vector2 dirToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToTarget) < _angle / 2)
            {
                var distanceToTarget = Vector2.Distance(transform.position, target.position);

                // Raycast to check for obstacles between the player and the target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, _obstacleMask);

                if (hit.collider != null)
                    continue;

                visibleTargets.Add(target);
                Debug.Log("Target found: " + target.name);
            }
        }
        return visibleTargets;
    }

    // Get all pickups in radius
    public List<Transform> FindVisiblePickups()
    {
        var visiblePickups = new List<Transform>();
        Collider2D[] pickupsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D pickupCollider in pickupsInViewRadius)
        {
            var pickup = pickupCollider.transform;

            // Skip if the pickup is the same as the object this script is attached to
            // or if the pickup is not a pickup
            if (pickup == transform || pickup.tag != "PickUp")
                continue;

            Vector2 dirToPickup = (pickup.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToPickup) < _angle / 2)
            {
                var distanceToPickup = Vector2.Distance(transform.position, pickup.position);

                // Raycast to check for obstacles between the player and the pickup
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPickup, distanceToPickup, _obstacleMask);

                if (hit.collider != null)
                    continue;

                visiblePickups.Add(pickup);
                Debug.Log("Pickup found: " + pickup.name);
            }
        }
        return visiblePickups;
    }


    // Draw field of view in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Vector2 fovLine1 = DirectionFromAngle(-_angle / 2, false);
        Vector2 fovLine2 = DirectionFromAngle(_angle / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + fovLine1 * _radius);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + fovLine2 * _radius);
    }
}
