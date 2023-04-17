using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireRate; //shots per minute
    private float _timeSinceLastShot;
 
    // Update is called once per frame
    void Update()
    {
        FaceMouse();
        if(CanShoot()){
            Shoot();
        }
        _timeSinceLastShot += Time.deltaTime;
        
    }
    private void Shoot(){
        if(Input.GetMouseButton(0)){
            GameObject bullet = Instantiate(_bullet,_shootPoint.position, _shootPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * _bulletSpeed);
            _timeSinceLastShot = 0;
        }
    }
    private bool CanShoot(){
        return _timeSinceLastShot > 1f / (_fireRate / 60f);
    }

    private void FaceMouse(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2) _player.position;
        _player.transform.up = direction;
    }
}
