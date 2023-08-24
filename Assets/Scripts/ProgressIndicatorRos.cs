using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

/// <summary>
/// This class offers the behaviour to display the percentages in the sliders of the Unity scene. This class expects to receive Point 
/// messages from ROS on three different topics: One topic for the Authority, one for the Confidence and the last topic for the Teleoperation.
/// Of these messages, only the value x is used, the y- and z-value are discarded. This is chosen for performance reasons since the current
/// implementation of the Unity Robotics Hub does not seem to efficiently support simple float messages. The x-values are expected to be in
/// the range between 0 and 1.
/// </summary>
public class ProgressIndicatorRos : MonoBehaviour
{
    [SerializeField] private GameObject authorityObj;
    [SerializeField] private GameObject confidenceObj;
    [SerializeField] private GameObject teleoperationObj;
    [SerializeField] public string authorityTopic;
    [SerializeField] public string confidenceTopic;
    [SerializeField] public string teleoperationTopic;
    [SerializeField] public GameObject[] transparencyObjs;

    //This class uses MRTK progress bars to represent the sliders
    private ProgressIndicatorLoadingBar authorityBar;
    private ProgressIndicatorLoadingBar confidenceBar;
    private ProgressIndicatorLoadingBar teleoperationBar;

    // Start is called before the first frame update
    void Start()
    {
        authorityBar = authorityObj.GetComponent<ProgressIndicatorLoadingBar>();
        confidenceBar = confidenceObj.GetComponent<ProgressIndicatorLoadingBar>();
        teleoperationBar = teleoperationObj.GetComponent<ProgressIndicatorLoadingBar>();
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(authorityTopic, ReceiveAuthority);
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(confidenceTopic, ReceiveConfidence);
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(teleoperationTopic, ReceiveTeleoperation);
        //Necessary to use the MRTK progress bars
        authorityBar.OpenAsync();
        confidenceBar.OpenAsync();
        teleoperationBar.OpenAsync();
    }

    private void ReceiveAuthority(PointMsg msg)
    {
        var authorityValue = (float)msg.x;
        authorityBar.Progress = authorityValue;

        //The received authority values also affect the transparency of the objects specified in transparencyObjs
        foreach (var obj in transparencyObjs)
        {
            var mat = obj.GetComponent<MeshRenderer>().material;
            var col = mat.color;
            col.a = authorityValue;
            mat.color = col;
        }
    }

    private void ReceiveConfidence(PointMsg msg)
    {
        var confidenceValue = (float)msg.x;
        confidenceBar.Progress = confidenceValue;
    }

    private void ReceiveTeleoperation(PointMsg msg)
    {
        var teleoperationValue = (float)msg.x;
        teleoperationBar.Progress = teleoperationValue;
    }
}
