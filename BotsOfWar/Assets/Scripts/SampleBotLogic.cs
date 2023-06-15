using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBotLogic : MonoBehaviour
{
    AgentMovement agentMovement;
    BotShoot botShoot;
    PlayerHealth playerHealth;
    FieldOfView fieldOfView;
    private float _timeSinceLastRandomPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        agentMovement = GetComponent<AgentMovement>();
        botShoot = GetComponent<BotShoot>();
        playerHealth = GetComponent<PlayerHealth>();
        fieldOfView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBot();
        Shoot();
    }

    private void MoveBot(){
        //get all items in radius
        var _itemsInViewRadius = fieldOfView.VisiblePickUps;

        if (_itemsInViewRadius.Count > 0)
        {
            //if there is a target in radius
            if(_itemsInViewRadius[0] != null)
                agentMovement.Target = _itemsInViewRadius[0].position;
        }
        else
        {
            agentMovement.Target = NavMeshPoint.GetRandomPointOnNavMesh();

            _timeSinceLastRandomPoint+=Time.deltaTime;
            if(_timeSinceLastRandomPoint >= 1){
                agentMovement.Target = NavMeshPoint.GetRandomPointOnNavMesh();
                _timeSinceLastRandomPoint = 0;
            }

        }
    }
    //Funtion finds lowest health enemy in the field of view of our bot
    private Transform GetLowestHealthEnemy()
    {
        var enemies = fieldOfView.VisibleEnemies;
        Transform target = null;
        foreach(var enemy in enemies) {
            if(target==null || enemy.GetComponent<PlayerHealth>().GetHealth() < target.GetComponent<PlayerHealth>().GetHealth())
                target = enemy;
        }
        return target;
    }
    private void Shoot()
    {
        var direction = GetLowestHealthEnemy();
        if(direction!= null)
            botShoot.Shoot(direction.position);
    }
}
