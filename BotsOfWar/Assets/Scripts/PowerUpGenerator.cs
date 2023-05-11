using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _parentOfCoords;
    private Vector3 [] _spawnPoints;
    private void Start()
    {
        ReadSpawnPoints();
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

    private void ReadSpawnPoints()
    {
        // Get all children of the parent object
        var children = _parentOfCoords.GetComponentsInChildren<Transform>();

        // Create array of Vector3
        _spawnPoints = new Vector3[children.Length - 1];

        // Fill array with Vector3 coordinates
        for (int i = 1; i < children.Length; i++)
        {
            _spawnPoints[i - 1] = children[i].position;
        }
    }
}
