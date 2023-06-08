using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //1 to 10 points per stat
    private int _healthPoints = 4;
    private int _speedPoints = 5;
    private int _fireRatePoints = 5;
    private int _bulletSpeedPoints = 5;
    private int _bulletDamagePoints = 1;

    //use below to adjust settings
    private int _multiplierHealth = 5;
    private int _multiplierBulletSpeed = 70; 
    private int _multiplierFireRate = 30; 
    private float _multiplierSpeed = 0.4f;
    private int _multiplierBulletDamage = 1; 
    void Start()
    {
        Invoke("SetStats", 0.1f);
    }

    public void SetStats()
    {
        gameObject.GetComponent<PlayerHealth>().SetHealth(_healthPoints * _multiplierHealth);
        gameObject.GetComponent<BotShoot>().SetBulletStats(_bulletSpeedPoints * _multiplierBulletSpeed,
            _bulletDamagePoints * _multiplierBulletDamage, _fireRatePoints * _multiplierFireRate);
        gameObject.GetComponent<AgentMovement>().SetMoveSpeed(_speedPoints * _multiplierSpeed);
    }
}