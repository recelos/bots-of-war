using UnityEngine;

public interface IBehaviour
{
    Vector3 GetNextPosition(Vector3 currentPosition);
    
    void ChangeTargetPosition(Vector3 newTargetPosition);
}