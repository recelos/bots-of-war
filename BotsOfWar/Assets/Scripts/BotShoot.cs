using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRatePerMinute;
    [SerializeField] private int _bulletSpeed;
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

    public float ShootAI(Vector2 enemyTarget)
    {
        GameObject bullet = Instantiate(_bullet, this.transform.position, this.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((enemyTarget).normalized * _bulletSpeed);
        //set bullet a damage
        bullet.GetComponent<Bullet>().Damage = _damage;
        bullet.GetComponent<Bullet>().Source = this.gameObject;

        return _fireRatePerMinute;
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
        bullet.GetComponent<Rigidbody2D>().AddForce(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.position).normalized * _bulletSpeed);
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
            _damage += 3;
        }
    }
}
