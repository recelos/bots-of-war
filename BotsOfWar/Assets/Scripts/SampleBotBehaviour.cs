using UnityEngine;

public class SampleBotBehaviour : IBehaviour
{
    private const float Speed = 3f;
    private Vector3 _targetPosition;

    public SampleBotBehaviour(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public Vector3 GetNextPosition(Vector3 currentPosition)
    {
        var movementDirection = (_targetPosition - currentPosition).normalized;
        
        var nextPosition = currentPosition + movementDirection * (Speed * Time.deltaTime);
        return nextPosition;
    }

    public void ChangeTargetPosition(Vector3 newTargetPosition)
    {
        _targetPosition = newTargetPosition;
    }
}
