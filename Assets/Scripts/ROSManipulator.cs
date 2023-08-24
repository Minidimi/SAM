using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;

/// <summary>
/// This class receives a Pose from ROS and transforms a GameObject accordingly.
/// </summary>
public class ROSManipulator : MonoBehaviour
{
    [SerializeField] private string topic = "";

    private float lastCmd = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        ROSConnection.GetOrCreateInstance().Subscribe<PoseMsg>(topic, receiveTransform);

    }

    void receiveTransform(PoseMsg msg)
    {
        //Convert the values to the Unity coordinate system since ROS uses a different one
        transform.localPosition = msg.position.From<FLU>();
        transform.localRotation = msg.orientation.From<FLU>();
        lastCmd = Time.time;
    }
}
        
  