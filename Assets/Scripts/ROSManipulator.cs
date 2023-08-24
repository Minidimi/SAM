using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;

public class ROSManipulator : MonoBehaviour
{
    [SerializeField] private string topic = "";

    private float lastCmd = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        ROSConnection.GetOrCreateInstance().Subscribe<PoseMsg>(topic, receiveTransform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    void receiveTransform(PoseMsg msg)
    {
        //PointMsg translateMsg = msg.position;
        //QuaternionMsg rotMsg = msg.orientation;
        //transform.localPosition = new Vector3(-(float) translateMsg.y, (float) translateMsg.z, (float) translateMsg.x);
        //transform.localRotation = new Quaternion((float) rotMsg.x, -(float) rotMsg.z, (float) rotMsg.y, (float) rotMsg.w);
        //var quat = new Quaternion((float)rotMsg.x, (float)rotMsg.y, (float)rotMsg.z, (float)rotMsg.w);
        //var euler = quat.eulerAngles;
        transform.localPosition = msg.position.From<FLU>();
        transform.localRotation = msg.orientation.From<FLU>();
        lastCmd = Time.time;
    }
}
        
  