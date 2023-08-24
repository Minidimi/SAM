using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    [SerializeField] public string topic;
    [SerializeField] public GameObject movingCrawler;

    // Start is called before the first frame update
    void Start()
    {
        movingCrawler.SetActive(false);
        ROSConnection.GetOrCreateInstance().Subscribe<PoseMsg>(topic, receiveTransform);
    }

    void receiveTransform(PoseMsg msg)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            movingCrawler.SetActive(true);
        }
        
    }
}
