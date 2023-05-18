using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float angle = 360f;
    [SerializeField] LayerMask obstacleMask; // Layer mask for obstacles, e.g. walls, bot doesn't see through them

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void Update()
    {
        FindVisibleTargets();
    }

    // Field of view in 2D space
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
        List<Transform> visibleTargets = new List<Transform>();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            // Skip if the target is the same as the object this script is attached to
            if (target == transform)
                continue;

            Vector2 dirToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                // Raycast to check for obstacles between the player and the target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask);

                if (hit.collider == null)
                {
                    visibleTargets.Add(target);
                    Debug.Log("Target found: " + target.name);
                }
            }
        }
        return visibleTargets;
    }


    // Draw field of view in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Vector2 fovLine1 = DirectionFromAngle(-angle / 2, false);
        Vector2 fovLine2 = DirectionFromAngle(angle / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + fovLine1 * radius);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + fovLine2 * radius);
    }
}
