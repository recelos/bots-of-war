using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage {get; set; }
    void Start()
    {
        transform.SetParent(GameObject.Find("BulletContainer").transform);
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // TODO: deal damage
        Destroy(gameObject);

        // (1 bullet = 1 damage point). If the bullet is destroyed then the player can receive damage from other bullets
        if (collision2D.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision2D.gameObject.GetComponent<PlayerHealth>();
            playerHealth.SetTakingDamage(false);
        }
    }

}
