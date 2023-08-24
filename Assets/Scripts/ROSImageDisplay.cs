using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Deprecated class for streaming a video from ROS. For this functionality, use ROSImageStream instead.
/// </summary>
public class ROSImageDisplay : MonoBehaviour
{
    

    private RawImage image;
    private int frameCount = 0;
    private int receivedCount = 0;
    private bool firstFrame = true;
    private bool messageProcessed = false;
    private Texture2D texture2D;

    public int FPS = 0;
    public int RPS = 0;
    private int updateRate = 1;
    private float dt = 0.0f;
    
    [SerializeField] private string topic;
    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<ImageMsg>(topic, receiveImage);
    }

    // Update is called once per frame
    void Update()
    {
        if (messageProcessed)
        {
            image.texture = texture2D;
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
            Debug.Log("FPS " + FPS);
            Debug.Log("RPS " + RPS);
        }
    }

    private void receiveImage(ImageMsg msg)
    {
        receivedCount++;

        if (!messageProcessed)
        {
            StartCoroutine(ProcessImage(msg));
        }
    }

    private IEnumerator ProcessImage(ImageMsg msg)
    {
        byte[] data = msg.data;
        int height = checked((int) msg.height);
        int width = checked((int) msg.width);

        if (msg.encoding.Equals("bgra8"))
        {
            if (frameCount == 0)
            {
                texture2D = new Texture2D(width, height, TextureFormat.BGRA32, false);
            }

            texture2D.LoadRawTextureData(data);
            texture2D.Apply();
        }
        else
        {
            Debug.Log(msg.encoding);
            throw new System.Exception("Only bgra8 encoding is supported!");
        }

        image.SetNativeSize();
        messageProcessed = true;
        
        
        yield return null;
    }
}
