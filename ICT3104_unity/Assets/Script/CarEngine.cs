using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxBrakeTorque = 1000f;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isSlowDown = false;
    public bool isBraking = false;

    //Store all the nodes of the path
    private List<Transform> nodes;
    //Keep track of our current node
    private int currentNode = 0;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        
        //GetComponentsInChildren find all the child objects
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();

        //Making sure our list is empty at the beginning, so we set this to a new list
        nodes = new List<Transform>();

        //Looping through the array
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            //If the transform is not our own transform, 
            if (pathTransforms[i] != path.transform)
            {
                //we're going to add it to the node array which node array only contains our child nodes
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        //SlowDown();
        Braking();
        CheckWaypointDistance();
    }

    //Calculation for the wheel turning the right direction depending on where the next waypoint.
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        //Magnitude is the length of the vector 
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    //Calculation for autonomous driving towards the waypoint
    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !isBraking)
        {
            //motorTorque is for the engine of the car wheels
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }
        
    }

    // Apply brakes if the car is over a certain speed
    private void SlowDown()
    {
        if (isSlowDown)
        {
            if (currentSpeed > 20)
            {
                isBraking = true;
            }
            else
            {
                isBraking = false;
            }
        }
        else
        {
            isBraking = false;
        }
    }

    private void Braking()
    {
        if (isBraking)
        {
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        } 
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    //Calculate the distance towards the node and if is very close to the node it will go to the next one.
    //Therefore increase the current node. 
    private void CheckWaypointDistance()
    {
        //if the distance to the way point is smaller than a number, we will go to set the current node to the next node
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.9f)
        {
            //if it is the last node, will set the current node to zero
            if (currentNode == nodes.Count - 1)
            {
                //currentNode = 0;
            }
            //if it is not the last one, will increase the current node
            else
            {
                currentNode++;
            }
        }
    }
}
