using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ProgressIndicatorRos : MonoBehaviour
{
    [SerializeField] private GameObject authorityObj;
    [SerializeField] private GameObject confidenceObj;
    [SerializeField] private GameObject teleoperationObj;
    [SerializeField] public string authorityTopic;
    [SerializeField] public string confidenceTopic;
    [SerializeField] public string teleoperationTopic;
    [SerializeField] public GameObject[] transparencyObjs;

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
        authorityBar.OpenAsync();
        confidenceBar.OpenAsync();
        teleoperationBar.OpenAsync();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    private void ReceiveAuthority(PointMsg msg)
    {
        var authorityValue = (float)msg.x;
        authorityBar.Progress = authorityValue;

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
