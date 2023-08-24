using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;
using UnityEngine;
/// <summary>
/// Deprecated class for controlling sliders.
/// </summary>
public class BarController : MonoBehaviour
{

    [SerializeField] private GameObject authorityObj;
    [SerializeField] private GameObject confidenceObj;
    [SerializeField] public string topic;

    private ConfidenceBar authorityBar;
    private ConfidenceBar confidenceBar;

    // Start is called before the first frame update
    void Start()
    {
        authorityBar = authorityObj.GetComponent<ConfidenceBar>();
        confidenceBar = confidenceObj.GetComponent<ConfidenceBar>();
        ROSConnection.GetOrCreateInstance().Subscribe<Float32MultiArrayMsg>(topic, ReceiveValues);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReceiveValues(Float32MultiArrayMsg msg)
    {
        var authorityValue = (float) msg.data[0];
        var confidenceValue = (float) msg.data[1];

        authorityBar.SetValue(authorityValue);
        confidenceBar.SetValue(confidenceValue);
    }
}
