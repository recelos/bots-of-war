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
        var textAsset = (TextAsset)Resources.Load("stats", typeof(TextAsset));
        var text = textAsset.text;
        var botStats = JsonUtility.FromJson<BotStats>(text);
        
        Debug.Log(botStats.Speed);
        
        gameObject.GetComponent<PlayerHealth>().SetHealth(botStats.Health * HealthMultiplier);
        gameObject.GetComponent<BotShoot>().SetBulletStats(botStats.BulletSpeed * BulletSpeedMultiplier,
            botStats.BulletDamage * BulletDamageMultiplier, botStats.FireRate * FireRateMultiplier);
        gameObject.GetComponent<AgentMovement>().SetMoveSpeed(botStats.Speed * SpeedMultiplier);
    }

    private bool ValidateStats(int stat)
    {
        return stat < 1 || stat > 10;
    }
}