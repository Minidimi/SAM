using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class ROSManipulator : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 0.0f;
    [SerializeField] private float damp = 1.0f;
    [SerializeField] private string transformTopic = "";

    private float lastCmd = 0.0f;
    private float currentRotation;

    private Vector3 translateROS;

    private Quaternion rotationROS;

    // Start is called before the first frame update
    void Start()
    {
        
        ROSConnection.GetOrCreateInstance().Subscribe<TransformStampedMsg>(transformTopic, receiveTransform);
        translateROS = transform.localPosition;
        rotationROS = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = translateROS;
        transform.localRotation = rotationROS;
    }

    private void FixedUpdate()
    {
        //receiveTransform(createMessage());
    }

    void receiveTransform(TransformStampedMsg msg)
    {
        Vector3Msg translateMsg = msg.transform.translation;
        QuaternionMsg rotMsg = msg.transform.rotation;
        translateROS = new Vector3((float) translateMsg.x, (float) translateMsg.y, (float) translateMsg.z);
        rotationROS = new Quaternion((float) rotMsg.x, (float) rotMsg.y, (float) rotMsg.z, (float) rotMsg.w);
        lastCmd = Time.time;
        Debug.Log(translateMsg.x);
    }

    TransformMsg createMessage()
    {
        Vector3Msg pos = new Vector3Msg(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        currentRotation += rotSpeed;
        Vector3 currentRot = transform.localRotation.eulerAngles;
        currentRot.y += rotSpeed;
        Quaternion newRot = Quaternion.Euler(currentRot);
        QuaternionMsg rot = new QuaternionMsg(newRot.x, newRot.y, newRot.z, newRot.w);
        return new TransformMsg(pos, rot);
    }
}
        
  