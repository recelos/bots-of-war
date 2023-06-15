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
    public (List<Transform>, List<Transform>) FindVisibleTargets()
    {
        var visiblePickups = new List<Transform>();
        var visibleTargets = new List<Transform>();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            var target = targetCollider.transform;

            // Skip if the target is the same as the object this script is attached to
            // or if the target is not another bot
            if (target == transform)
                continue;

            Vector2 dirToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToTarget) < _angle / 2)
            {
                var distanceToTarget = Vector2.Distance(transform.position, target.position);

                // Raycast to check for obstacles between the player and the target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, _obstacleMask);

                if (hit.collider != null)
                    continue;

                if (target.tag == "PickUp")
                    visiblePickups.Add(target);
                
                if (target.tag == "Player" && !target.GetComponent<PlayerHealth>().dead)
                    visibleTargets.Add(target);
            }
        }
        return (visibleTargets, visiblePickups);
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
