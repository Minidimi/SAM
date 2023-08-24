using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoordinateFrame : MonoBehaviour
{
    float rot = 0.0f;

    [SerializeField] float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rot += speed;
        var rotation = Quaternion.AngleAxis(rot, new Vector3(0, 0, 1));
        rotation = new Quaternion(rotation.x, rotation.z, rotation.y, rotation.w);
        transform.rotation = rotation;
    }
}
