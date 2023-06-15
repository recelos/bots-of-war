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
    private FieldOfView _fieldOfView;
    private Transform[] _targetsInViewRadius;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        //how much time has passed since last shot
        _timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot(Vector2 direction)
    {
        if(!CanShoot() || _fieldOfView.VisibleEnemies.Count == 0 || direction == null)
            return;
        //create bullet at shootPoint position with shootPoint rotation
        GameObject bullet = Instantiate(_bullet, this.transform.position, this.transform.rotation);
      
        //set bullet speed
        bullet.GetComponent<Rigidbody2D>().AddForce((direction - (Vector2) transform.position).normalized * _bulletSpeed);

        //set bullet a damage
        bullet.GetComponent<Bullet>().Damage = _damage;
        bullet.GetComponent<Bullet>().Source = this.gameObject;
        _timeSinceLastShot = 0;
        //destroy bullet after 5 seconds, in case it bugs out
        Destroy(bullet, 5f);
    }

    // Calculate vector between bot and target
    private Vector2 GetDirectionToTarget(Transform target)
    {
        return (target.position - transform.position).normalized;
    }
    
    private bool CanShoot()
    {
        return _timeSinceLastShot > 1f / (_fireRatePerMinute / 60f) && !_playerHealth.dead;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            var probability = Random.Range(0, 100);

            // either increase damage or fire rate
            if (probability < 50)
            {
                _damage += 15;
            }
            else
            {
                _fireRatePerMinute += 50;
            }
        }
    }
    public void SetBulletStats(int bulletSpeed, int bulletDamage, float fireRatePerMinute)
    {
        _bulletSpeed = bulletSpeed;
        _damage = bulletDamage;
        _fireRatePerMinute = fireRatePerMinute;
    }
}
