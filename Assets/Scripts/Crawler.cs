using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using UnityEngine;

/// <summary>
/// This class controls the crawler robot. The scene uses two crawler models, one if it is connected to the cage and another if it is moving.
/// Here, the moving crawler is activated when a position is first received.
/// </summary>
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
