using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _centralPoint;
    [SerializeField] private Transform _shootPoint;
    private BasicMovement _basicMovement;
    private float _fireRate;
    private float _timeSinceLastShot;
    private const int _bulletSpeed = 400;
    private int _damage;
    void Start()
    {
        //fireRate (500 hunderd bullets per minute)
        _fireRate = 500;
        _basicMovement =  GetComponent<BasicMovement>();
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
        //give bullet's speed
        bullet.GetComponent<Rigidbody2D>().AddForce( ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) this.transform.position).normalized *_bulletSpeed);
        Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized);
        //set bullet's damage
        bullet.GetComponent<Bullet>().Damage = _damage;
        bullet.GetComponent<Bullet>().Source = this.gameObject;
    }
    //checks if bot is able to shoot
    private bool CanShoot(){
        return _timeSinceLastShot > 1f / (_fireRate / 60f);
    }
}
