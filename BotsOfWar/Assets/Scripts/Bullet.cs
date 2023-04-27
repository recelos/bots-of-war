using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private int _damage;
    void Start(){
        transform.SetParent(GameObject.Find("BulletContainer").transform);
    }
    private void OnCollisionEnter2D(Collision2D collision2D){
        //zadawanie obrazen tutaj
        Destroy(gameObject);
    }

    public void setDamage(int damage){
        this._damage = damage;
    }
}
