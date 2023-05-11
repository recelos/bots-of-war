using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _bulletSpeed = 400;
    [SerializeField] private int _damage;
    private BasicMovement _basicMovement;
    private float _timeSinceLastShot;
    
    void Start()
    {
        //fireRate (500 hunderd bullets per minute)
        _fireRate = 500;
        _basicMovement = GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //how much time has passed since last shot
        _timeSinceLastShot += Time.deltaTime;

        if(Input.GetMouseButton(0) && CanShoot()){
            Shoot();
            _timeSinceLastShot = 0;
        }
        
    }

    private void Shoot(){
        //create bullet at shootPoint position with shootPoint rotation
        GameObject bullet = Instantiate(_bullet, this.transform.position, this.transform.rotation);
        //give bullet a speed
        bullet.GetComponent<Rigidbody2D>().AddForce( ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) this.transform.position).normalized *_bulletSpeed);
        //set bullet a damage
        bullet.GetComponent<Bullet>().Damage = _damage;
        bullet.GetComponent<Bullet>().Source = this.gameObject;
    }
    //checks if the bot is able to shoot
    private bool CanShoot(){
        return _timeSinceLastShot > 1f / (_fireRate / 60f);
    }
}
