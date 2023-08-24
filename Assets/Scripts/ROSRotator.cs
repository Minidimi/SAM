using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ROSRotator : MonoBehaviour
{
    
    [SerializeField] private GameObject[] objects;
    [SerializeField] private string topic;
    [SerializeField] private bool useRadians = true;

    private float[] currentRots;

    // Start is called before the first frame update
    void Start()
    {
        currentRots = new float[objects.Length];
        
        ROSConnection.GetOrCreateInstance().Subscribe<PoseMsg>(topic, receiveJointState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    void receiveJointState(PoseMsg msg)
    {
        var rots = new float[7];
        rots[0] = (float)msg.position.x;
        rots[1] = (float)msg.position.y;
        rots[2] = (float)msg.position.z;
        rots[3] = (float)msg.orientation.x;
        rots[4] = (float)msg.orientation.y;
        rots[5] = (float)msg.orientation.z;
        rots[6] = (float)msg.orientation.w;

        for (int i = 0; i < rots.Length; i++)
        {
            var obj = objects[i];
            var rot = rots[i] - currentRots[i];
            if (useRadians)
            {
                obj.transform.Rotate(0, (float) -rot * 57.296f, 0);
            } else
            {
                obj.transform.Rotate(0, (float)rot, 0);
            }
            
        }

        currentRots = rots;
    }
}