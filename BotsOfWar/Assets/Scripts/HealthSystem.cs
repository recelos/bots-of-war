// The script tracks whether players on the map have come into contact with a bullet. If so, they lose health

using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    private List<GameObject> players;

    private void Start()
    {
        // Get all objects with the tag "Player" and add them to the "players" list
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
            players.Add(playerObject);
    }

    private void Update()
    {
        foreach (var player in players)
        {
            // Check if the player has interacted with another object
            var collider = Physics2D.OverlapBox(player.GetComponent<BoxCollider2D>().transform.position,
                player.GetComponent<BoxCollider2D>().size, 0f);

            if(collider.CompareTag("Bullet"))
            {
                var playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth == null || collider == null)
                {
                    continue;
                }
                collider.enabled = false;
                playerHealth.TakeDamage(1);
            }
        }
    }
}