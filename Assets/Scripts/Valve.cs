using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector;

/// <summary>
/// This class receives the rotation of a valve around y. For performance reasons i expects a Point message from ROS and only uses
/// the x-value to determine the angle.
/// </summary>
public class Valve : MonoBehaviour
{
    [SerializeField] private string topic;

    private float currentRot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(topic, receiveJointState);
    }

    void receiveJointState(PointMsg msg)
    {
        var rot = (float)msg.x - currentRot;

        transform.Rotate(0, (float)rot, 0);

        currentRot = (float)msg.x;
    }
}
