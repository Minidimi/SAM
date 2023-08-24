using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.Geometry;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class Gripper : MonoBehaviour
{

    [SerializeField] private string topic;
    [SerializeField] private double minRotation = 0;

    [SerializeField] private double maxRotation = 41;

    [SerializeField] private double minRotation_received = 48;
        
    [SerializeField] private double maxRotation_received = 230;

    private double _currentRot = 0;
    
    [SerializeField] private GameObject rightInnerKnuckle;
    [SerializeField] private GameObject rightInnerFinger;
    [SerializeField] private GameObject rightOuterKnuckle;
    [SerializeField] private GameObject leftInnerKnuckle;
    [SerializeField] private GameObject leftInnerFinger;
    [SerializeField] private GameObject leftOuterKnuckle;
    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(topic, receiveCommand);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            Control(230.0d);
        }

        if (Input.GetKeyDown("c"))
        {
            Control(48.0d);
        }

        if (Input.GetKeyDown("m"))
        {
            Control(100.0d);
        }
    }

    public void Rotate(double value)
    {
        float f_val = (float)value;
        rightInnerFinger.transform.Rotate( f_val, 0, 0);
        rightInnerKnuckle.transform.Rotate( -f_val, 0, 0);
        rightOuterKnuckle.transform.Rotate( -f_val, 0, 0);
                
        leftInnerFinger.transform.Rotate(f_val, 0, 0);
        leftInnerKnuckle.transform.Rotate( -f_val, 0, 0);
        leftOuterKnuckle.transform.Rotate( -f_val, 0, 0);

        _currentRot += value;
    }

    public void Control(double value)
    {
        value =  (value - minRotation_received) / (maxRotation_received - minRotation_received);
        value = (value * (maxRotation - minRotation)) + minRotation;
        if (value > maxRotation)
        {
            value = maxRotation;
        }
        else if (value < minRotation)
        {
            value = minRotation;
        }

        Rotate(value - _currentRot);
    }

    public void receiveCommand(PointMsg msg)
    {
        double val = msg.x;
        Control(val);
    }
}
