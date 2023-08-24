using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using RosMessageTypes.Std;
using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;


public class ConfidenceBar : MonoBehaviour
{
    [SerializeField] private string topic;
    [SerializeField] private float maxScale;
    [SerializeField] private float overlap;
    [SerializeField] private GameObject right;
    
    private void Awake()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Float32Msg>(topic, receiveValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void receiveValue(Float32Msg msg)
    {
        SetValue(msg.data);
    }

    public void SetValue(float newConfidence)
    {
        Vector3 oldScale = transform.localScale;
        Vector3 newScale = new Vector3(oldScale.x, newConfidence * maxScale + overlap, oldScale.z);
        transform.localScale = newScale;

        Vector3 oldPosition = right.transform.position;
        Vector3 newPosition = new Vector3(oldPosition.x, newConfidence * maxScale, oldPosition.z);
        right.transform.position = newPosition;
    }
}
