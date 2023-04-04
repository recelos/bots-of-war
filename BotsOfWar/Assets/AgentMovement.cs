using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{

    private Vector3 target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        //these 3 lines always have to be here
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }
    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x,target.y,transform.position.z));
    }
}
