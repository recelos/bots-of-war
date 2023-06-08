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
        //get all targets in radius
        _targetsInViewRadius = _fieldOfView.FindVisibleTargets().Item1.ToArray();
        //how much time has passed since last shot
        _timeSinceLastShot += Time.deltaTime;
        
        //if there is a target in radius and bot can shoot
        if (_targetsInViewRadius.Length > 0 && CanShoot() && !_playerHealth.dead)
        {
            //shoot, but with a delay, later can be implemented based on bot's skill or sth
            Invoke("Shoot", Random.Range(0.1f, 0.5f));
            //reset time since last shot
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

        //destroy bullet after 5 seconds, in case it bugs out
        Destroy(bullet, 5f);
    }

    private void GiveSpeedToBullet(GameObject bullet)
    {
        var direction = Vector2.zero;
        
        if (_targetsInViewRadius.Length > 0)
            direction = GetDirectionToTarget(_targetsInViewRadius[0]);

        //set bullet speed
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * _bulletSpeed);
    }

    // Calculate vector between bot and target
    private Vector2 GetDirectionToTarget(Transform target)
    {
        return (target.position - transform.position).normalized;
    }
    
    private bool CanShoot()
    {
        return _timeSinceLastShot > 1f / (_fireRatePerMinute / 60f);
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
}
