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
        ChangeShootPointPosition();
        //create bullet at shootPoint position with shootPoint rotation
        GameObject bullet = Instantiate(_bullet,_shootPoint.position, _shootPoint.rotation);
        //give bullet's speed
        if(_basicMovement.isFacingRight())
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * _bulletSpeed);
        else
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * (-_bulletSpeed));
        //set bullet's damage
        bullet.GetComponent<Bullet>().Damage = _damage;
      
    }

    private void ChangeShootPointPosition(){
        //read mouse position
        var _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //calculate direction from bot to mouse position
        var _direction = _mousePos - _centralPoint.position;
        //rotate central point
        if(_basicMovement.isFacingRight()){
            _centralPoint.transform.right = _direction;
        }else{
             _centralPoint.transform.right = -_direction;
        }
    }
    //checks if bot is able to shoot
    private bool CanShoot(){
        return _timeSinceLastShot > 1f / (_fireRate / 60f);
    }
}
