using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using UnityEngine.UI;
using System.IO;

public class ROSImageDisplay : MonoBehaviour
{
    [SerializeField] private string topic;

    private RawImage image;
    private int frameCount = 0;

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
        
    }

    private void receiveImage(ImageMsg msg)
    {
        Debug.Log("Received message");
        byte[] data = msg.data;
        int height = checked((int)msg.height);
        int width = checked((int)msg.width);
        int step = checked((int)msg.step);

        Texture2D texture;

        if (msg.encoding.Equals("rgb8"))
        {
            texture = applyToTextureRGB8(data, width, height);
        }
        else if (msg.encoding.Equals("bgra8"))
        {
            texture = applyToTextureBGRA8(data, width, height);
        }
        else
        {
            Debug.Log(msg.encoding);
            throw new System.Exception("Only rgb8 and bgra8 encoding is supported!");
        }

        image.texture = texture;
        image.SetNativeSize();
    }

    private Texture2D applyToTextureRGB8(byte[] data, int width, int height)
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

    private Texture2D applyToTextureBGRA8(byte[] data, int width, int height)
    {
        Texture2D target = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int position = row * 4 * width + 4 * col;
                byte b = data[position];
                byte g = data[position + 1];
                byte r = data[position + 2];
                byte a = data[position + 3];

                if (b != 0 || r != 0 || g != 0 || a != 0)
                    Debug.Log("Received non-zero data of length" + data.Length);

                Color color = new Color(r / 128.0f, g / 128.0f, b / 128.0f, a / 128.0f);
                pixels[row * width + col] = color;
            }
        }

        target.SetPixels(pixels);

        return target;
    }
}
