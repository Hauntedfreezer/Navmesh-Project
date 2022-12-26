using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMove : MonoBehaviour
{
    #region variables
    public Transform[] waypoints;
    public int index;
    public NavMeshAgent agent;
    public Transform target;
    public AIStates state;
    public float distanceToTarget;
    public GameObject coin;
    public GameObject key;
    public GameObject lockedDoor;
    public GameObject[] collectingAreas;
    #endregion

    private void Update()
    {
        if (state == AIStates.Patrol)
        {
            Patrol();
        }
        if (state == AIStates.Finish)
        {
            Finish();
        }
        if (state == AIStates.Collecting)
        {
            Collecting();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
       
        {
            state = AIStates.Finish;
        }
        if (other.tag == "Mud")
       
        {
            agent.speed = 0.75f;
        }
        if (other.tag == "triggercollecting")
        {
            state = AIStates.Collecting;
            Debug.Log("Changedstates");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mud")
        {
            agent.speed = 3.5f;
        }
        if (other.tag == "triggercollecting")
        {
            state = AIStates.Patrol;
            Debug.Log("Changedstates");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            Debug.Log("Coin has been collectied");
        }
       
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            lockedDoor.transform.position = new Vector3(lockedDoor.transform.position.x, transform.position.y + 5, transform.position.z);
            Debug.Log("Key has been collectied");
        }
    }
    private void Start()
    {
        //agent grabbing navemshagent
        agent = GetComponent<NavMeshAgent>();
        //index is how many index and the target
        target = waypoints[index];
    }
   void Patrol()
    {
        //looping start to finish
        distanceToTarget = Vector3.Distance(agent.transform.position,target.transform.position);
        if (distanceToTarget <= 1)
        {
            index++;
            if (index > waypoints.Length-1)
            {
                index = 0;

            }
            target = waypoints[index];
        }
        agent.SetDestination(target.position);
    }

   void Collecting()
    {
        distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
        agent.SetDestination(target.transform.position);

    }
 
   void  Finish()
    {
        agent.enabled = false;
    }
}

#region states
public enum AIStates
{
    Patrol,
    Collecting,
    Finish,
}
#endregion
