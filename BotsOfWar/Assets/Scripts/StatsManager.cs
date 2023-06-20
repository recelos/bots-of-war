using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //1 to 10 points per stat
    private const int DefaultHealthPoints = 4;
    private const int DefaultSpeedPoints = 5;
    private const int DefaultFireRate = 5;
    private const int DefaultBulletSpeed = 5;
    private const int DefaultBulletDamage = 1;

    //use below to adjust settings
    private const int HealthMultiplier = 5;
    private const int BulletSpeedMultiplier = 70; 
    private const int FireRateMultiplier = 30; 
    private const float SpeedMultiplier= 0.4f;
    private const int BulletDamageMultiplier = 1; 
    void Start()
    {
        Invoke("SetStats", 0.1f);
    }

    public void SetStats()
    {
        var name = this.name;
         var textAsset = (TextAsset)Resources.Load(name, typeof(TextAsset));
        var text = textAsset.text;
        var botStats = JsonUtility.FromJson<BotStats>(text);
        
        var health = Validate(botStats.Health) ? botStats.Health : DefaultHealthPoints; 
        var speed = Validate(botStats.Speed) ? botStats.Speed : DefaultSpeedPoints; 
        var fireRate = Validate(botStats.FireRate) ? botStats.FireRate : DefaultFireRate; 
        var bulletSpeed = Validate(botStats.BulletSpeed) ? botStats.BulletSpeed : DefaultBulletSpeed; 
        var bulletDamage = Validate(botStats.BulletDamage) ? botStats.BulletDamage : DefaultBulletDamage;

        ValidateStatsLimit(ref health, ref speed, ref fireRate, ref bulletSpeed, ref bulletDamage);

        gameObject.GetComponent<PlayerHealth>().SetHealth(health * HealthMultiplier);
        gameObject.GetComponent<BotShoot>().SetBulletStats(bulletSpeed * BulletSpeedMultiplier,
            bulletDamage * BulletDamageMultiplier, fireRate * FireRateMultiplier);
        gameObject.GetComponent<AgentMovement>().SetMoveSpeed(speed * SpeedMultiplier);
    }

    private void ValidateStatsLimit(ref int health, ref int speed, ref int fireRate, ref int bulletSpeed, ref int bulletDamage)
    {
        var sumStats = health + speed + fireRate + bulletSpeed + bulletDamage;
        if (sumStats > 25)
        {
            health = 5;
            speed = 5;
            fireRate = 5;
            bulletSpeed = 5;
            bulletDamage = 5;
        }
    }

    private bool Validate(int stat)
    {
        return !(stat < 1 || stat > 10);
    }
}