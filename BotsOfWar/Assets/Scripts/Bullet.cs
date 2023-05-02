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
    }
}
