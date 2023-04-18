using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _density;

    private readonly Vector3 [] _spawnPoints =     // Defined in code coordinates
        {
            new Vector3(-1.67f, 1.79f, 0),
            new Vector3(-2.14f, -2.88f, 0),
            new Vector3(4.22f, 4.28f, 0),
            new Vector3(4.25f, 0.45f, 0),
            new Vector3(4.3f, -4.36f, 0),
            new Vector3(-4.22f, 4.25f, 0),
            new Vector3(-1.18f, 4.2f, 0),
            new Vector3(1.36f, 2.33f, 0),
            new Vector3(3.56f, 1.71f, 0),
            new Vector3(-0.56f, -0.99f, 0),
            new Vector3(1.31f, -0.51f, 0),
            new Vector3(-4.36f, 0.45f, 0),
            new Vector3(-4.33f, -0.61f, 0),
            new Vector3(1.2f, -2.54f, 0),
            new Vector3(-1.07f, -4.38f, 0),
            new Vector3(3.74f, -0.91f, 0),
            new Vector3(-2.89f, 3.98f, 0),
            new Vector3(-4.38f, -3.26f, 0),
            new Vector3(-3.8f, -4.33f, 0),
            new Vector3(-0.99f, 0.51f, 0)
        };

    private void Start()
    {
        ToggleObstacles();
    }

    private void ToggleObstacles()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            if (Random.value < _density) // define probability of object spawn
            {
                // Create object on the scene, when game starts
                var child = Instantiate(_prefab, spawnPoint, Quaternion.identity);

                // Assign this object to the parent which keeps this script
                child.transform.SetParent(transform);
            }
        }
    }
}
