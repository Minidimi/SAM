using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a helper class to rotate an object around y by a fixed amount.
/// </summary>
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
