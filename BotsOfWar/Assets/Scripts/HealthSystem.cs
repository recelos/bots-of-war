// THIS SCRIPT CAN BE DELETED (IT IS NOT USED)

// The script tracks whether players on the map have come into contact with a bullet. If so, they lose health

//using System.Collections.Generic;
//using UnityEngine;

//public class HealthSystem : MonoBehaviour
//{
//    private List<GameObject> _players;

//    private void Start()
//    {
//        // Get all objects with the tag "Player" and add them to the "players" list
//        var playerObjects = GameObject.FindGameObjectsWithTag("Player");

//        _players = new List<GameObject>();
//        _players.AddRange(playerObjects);
//    }

//    private void Update()
//    {
//        // int i = 0; // For testing only!

//        foreach (var player in _players)
//        {
//            // Check if the player has interacted with another object
//            var collider = Physics2D.OverlapCircle(player.GetComponent<CircleCollider2D>().transform.position,
//                player.GetComponent<CircleCollider2D>().radius);

//            if (collider != null)
//            {
//                // If the collision is with the bullet
//                if (collider.CompareTag("Projectile"))
//                {
//                    // For testing only!
//                    // Debug.Log("PLAYER " + i.ToString() + " HIT");

//                    var playerHealth = player.GetComponent<PlayerHealth>();
//                    if (playerHealth == null)
//                    {
//                        continue;
//                    }
//                    playerHealth.TakeDamage(1);
//                }
//            }
//            // i++; // For testing only!
//        }
//    }
//}