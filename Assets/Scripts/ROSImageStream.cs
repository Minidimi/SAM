using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using UnityEngine.UI;
using System.IO;
using System;

/// <summary>
/// This class can be used to stream a video from ROS to Unity. This class expects Image messages from ROS and displays them as a mesh on an object.
/// Here, this is specifically used on a plane to stream the video.
/// </summary>
public class ROSImageStream : MonoBehaviour
{
    [SerializeField] public MeshRenderer meshRenderer;
    [SerializeField] public string topic;
    
    private bool firstFrame = true;
    private bool messageProcessed = false;
    private Texture2D texture2D;

    private int receivedCount = 0;
    private int frameCount = 0;
    public int FPS = 0;
    public int RPS = 0;
    private int updateRate = 1;
    private float dt = 0.0f;

    
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.material = new Material(Shader.Find("Standard"));
        ROSConnection.GetOrCreateInstance().Subscribe<ImageMsg>(topic, ReceiveImage);
    }

    // Update is called once per frame
    void Update()
    {
        if (messageProcessed)
        {
            meshRenderer.material.mainTexture = texture2D;
            frameCount++;
            messageProcessed = false;
        }

        UpdateFPS();
    }

    private void UpdateFPS()
    {
        dt += Time.deltaTime;
        if (dt > 1.0f / updateRate)
        {
            FPS = Mathf.RoundToInt(frameCount / dt);
            RPS = Mathf.RoundToInt(receivedCount / dt);
            dt -= 1.0f / updateRate;
            frameCount = 0;
            receivedCount = 0;
        }
    }

    private void ReceiveImage(ImageMsg msg)
    {
        receivedCount++;


        StartCoroutine(ApplyImage(msg));        
    }

    private IEnumerator ApplyImage(ImageMsg msg)
    {
        byte[] data = msg.data;
        int height = checked((int)msg.height);
        int width = checked((int)msg.width);

        

        if (msg.encoding.Equals("rgb8") || msg.encoding.Equals("8UC3"))
        {
            if (firstFrame)
            {   
                float ratio = (1.0f * width) / height;

                var newScaleX = transform.localScale.z * ratio;
                var newScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
                transform.localScale = newScale;
                texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);
                firstFrame = false;
            }

            texture2D.LoadRawTextureData(data);
            texture2D.Apply();
        }
        else
        {
            Debug.Log(msg.encoding);
            throw new System.Exception("Only rgb8 encoding is supported!");
        }

        messageProcessed = true;

        yield return null;
    }
}

