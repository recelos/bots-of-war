using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _pickUp1;
    [SerializeField] private GameObject _pickUp2;
    [SerializeField] private GameObject _pickUp3;
    [SerializeField] private GameObject _pickUp4;
    [SerializeField] private GameObject _pickUp5;
    [SerializeField] private float _density;
    private readonly Vector3 [] _spawnPoints =     
    {
        
    };

    private void Start()
    {
        ToggleObstacles();
    }

    private void ToggleObstacles()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            // Create object on the scene, when game starts
            var child = Instantiate(_prefab, spawnPoint, Quaternion.identity);

            // Assign this object to the parent which keeps this script
            child.transform.SetParent(transform);
        }
    }
}
