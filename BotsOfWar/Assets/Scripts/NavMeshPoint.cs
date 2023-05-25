using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Extensions;
using NavMeshPlus.Components;

public class NavMeshPoint : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();

        if (_navMeshSurface != null)
        {
            _navMeshSurface.BuildNavMesh();
        }
    }

    public static Vector2 GetRandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int randomIndex = Random.Range(0, navMeshData.vertices.Length);
        Vector2 randomPoint = navMeshData.vertices[randomIndex];

        return randomPoint;
    }
}