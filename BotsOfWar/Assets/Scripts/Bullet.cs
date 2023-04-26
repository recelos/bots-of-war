using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
  
    private void OnCollisionEnter2D(Collision2D collision2D){
        //zadawanie obrazen tutaj
        Destroy(gameObject);
    }

    public void setDamage(int damage){
        this.damage = damage;
    }
}
