using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class ModeController : MonoBehaviour
{
    [SerializeField] public string topic;
    [SerializeField] public GameObject target;
    [SerializeField] public GameObject video;
    [SerializeField] public GameObject stats;
    [SerializeField] public GameObject[] hideable;
    [SerializeField] public Vector3[] eulerRotations;
    [SerializeField] public Vector3[] translations;
    [SerializeField] public float[] scales;
    [SerializeField] public Vector3[] videoTranslations;
    [SerializeField] public Vector3[] statsTranslations;
    [SerializeField] public bool[] show;

    private Vector3 currentRot;
    private Vector3 initTranslation;
    private Vector3 initVideo;
    private Vector3 initStats;
    private Vector3 initScale;

    // Start is called before the first frame update
    void Start()
    {
        currentRot = target.transform.eulerAngles;
        initTranslation = target.transform.localPosition;
        initScale = target.transform.localScale;
        initVideo = video.transform.position;
        initStats = stats.transform.position;
        ROSConnection.GetOrCreateInstance().Subscribe<PointMsg>(topic, receiveMode);
        //receiveMode(new PointMsg(4, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
            receiveMode(new PointMsg(1, 0, 0));
        else if (Input.GetKeyDown("2"))
            receiveMode(new PointMsg(2, 0, 0));
        else if (Input.GetKeyDown("3"))
            receiveMode(new PointMsg(3, 0, 0));
        else if (Input.GetKeyDown("4"))
            receiveMode(new PointMsg(4, 0, 0));
        else if (Input.GetKeyDown("5"))
            receiveMode(new PointMsg(5, 0, 0));
        else if (Input.GetKeyDown("6"))
            receiveMode(new PointMsg(6, 0, 0));
    }

    void receiveMode(PointMsg msg)
    {
        int mode = Mathf.RoundToInt((float) msg.x);
        var rot = eulerRotations[mode - 1];
        //var dif = rot - currentRot;
        //currentRot = rot;
        //target.transform.Rotate(dif);
        target.transform.rotation = Quaternion.EulerAngles(currentRot + rot);
        target.transform.localPosition = initTranslation + translations[mode - 1];
        target.transform.localScale = initScale * scales[mode - 1];
        video.transform.position = initVideo + videoTranslations[mode - 1];
        stats.transform.position = initStats + statsTranslations[mode - 1];

        foreach (var obj in hideable)
        {
            obj.SetActive(show[mode - 1]);
        }
    }
}
