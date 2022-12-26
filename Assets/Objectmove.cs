using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectmove : MonoBehaviour
{
    #region variables
    public Transform startPoint, endPoint;
    public float speed;
    public Transform target;
    public float distance;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        target = endPoint;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.25f)
        {
            if (target == endPoint)
            {
                target = startPoint;
            }
            else
            {
                target = endPoint;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
