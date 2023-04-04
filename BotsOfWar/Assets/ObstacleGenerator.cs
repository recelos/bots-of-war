using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _density;
    private Vector3 [] _spawnPoints =
        {new Vector3(-6.55f, -0.98f, 0),
        new Vector3(-4.68f, 0.96f, 0),
        new Vector3(-8.8f, 0.29f, 0),
        new Vector3(-4.03f, -2.97f, 0),
        new Vector3(-9.54f, -1.79f, 0),
        new Vector3(7.48f, -3.27f, 0),
        new Vector3(8.78f, -0.82f, 0),
        new Vector3(8.78f, 2.71f, 0),
        new Vector3(-9.64f, 2.57f, 0),
        new Vector3(5.83f, 1.03f, 0),
        new Vector3(5.5f, 3.18f, 0),
        new Vector3(5.53f, -1.62f, 0),
        new Vector3(2.64f, 0.39f, 0),
        new Vector3(0.83f, 3.31f, 0),
        new Vector3(-1.22f, 0.19f, 0),
        new Vector3(-2.29f, 3.48f, 0),
        new Vector3(-6.05f, 3.55f, 0),
        new Vector3(-7.53f, -3.44f, 0),
        new Vector3(-0.57f, -3.37f, 0),
        new Vector3(2.41f, -2.53f, 0)};

    private void Start()
    {
        ToggleObstacles();
    }

    private void ToggleObstacles()
    {
        for(int i = 0; i < _spawnPoints.Length; i++)
        {
            if(Random.value < _density)
            {
                GameObject child = Instantiate(_prefab, _spawnPoints[i], Quaternion.identity);
                child.transform.SetParent(transform);
            }
        }
    }
}
