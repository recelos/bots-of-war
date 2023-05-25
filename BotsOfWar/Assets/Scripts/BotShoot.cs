using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRatePerMinute = 500;
    [SerializeField] private int _bulletSpeed = 400;
    [SerializeField] private int _damage;
    private float _timeSinceLastShot;

    void Update()
    {
        //how much time has passed since last shot
        _timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0) && CanShoot())
        {
            Shoot();
            _timeSinceLastShot = 0;
        }

    }

    private void Shoot()
    {
        //create bullet at shootPoint position with shootPoint rotation
        GameObject bullet = Instantiate(_bullet, this.transform.position, this.transform.rotation);
        GiveSpeedToBullet(bullet);
        //set bullet a damage
        bullet.GetComponent<Bullet>().Damage = _damage;
        bullet.GetComponent<Bullet>().Source = this.gameObject;
    }

    private void GiveSpeedToBullet(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().AddForce(((Vector2)Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.position).normalized * _bulletSpeed);
    }
    //checks if the bot is able to shoot
    private bool CanShoot()
    {
        return _timeSinceLastShot > 1f / (_fireRatePerMinute / 60f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            _damage += 100;
        }
    }
}
