using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }
    public GameObject Source { get; set; }
    void Start()
    {
        transform.SetParent(GameObject.Find("BulletContainer").transform);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.Equals(Source) ||
            collider2D.gameObject.CompareTag("Projectile") ||
            collider2D.gameObject.CompareTag("PickUp"))
            return;

        // If the bullet is destroyed then the player can receive damage from other bullets
        if (collider2D.gameObject.CompareTag("Player"))
        {
            var playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Damage);
            }
        }
        Destroy(gameObject);
    }
}
