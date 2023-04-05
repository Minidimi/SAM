using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class ROSImageDisplay : MonoBehaviour
{
    [SerializeField] private string topic;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Material>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<ImageMsg>(topic, receiveImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void receiveImage(ImageMsg msg)
    {
        byte[] data = msg.data;
        int height = checked((int)msg.height);
        int width = checked((int)msg.width);
        int step = checked((int)msg.step);

        if (!msg.encoding.Equals("rgb8"))
            throw new System.Exception("Only rgb8 encoding is supported!");

        Texture2D texture = applyToTexture(data, width, height);

        material.SetTexture("image", texture);
    }

    private Texture2D applyToTexture(byte[] data, int width, int height)
    {
        Texture2D target = new Texture2D(width, height);

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int position = row * 3 * width + 3 * col;
                byte r = data[position];
                byte g = data[position + 1];
                byte b = data[position + 2];

                Color color = new Color(r / 128.0f, g / 128.0f, b / 128.0f);
                target.SetPixel(col, row, color);
            }
        }

        return target;
    }
}
