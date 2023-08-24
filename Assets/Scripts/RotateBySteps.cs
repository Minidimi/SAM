using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBySteps : MonoBehaviour
{
    [SerializeField] public float stepSize;
    
    public void rotateRight()
    {
        transform.Rotate(0, -stepSize, 0);
    }

    public void rotateLeft()
    {
        transform.Rotate(0, stepSize, 0);
    }
}
