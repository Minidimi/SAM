using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

/// <summary>
/// Class to disable the MRTK Orbital function of an object while it is manually manipulated. This allows the user to smoothly position an object
/// even if it has an Orbital component. This is currently not in use since no objects use the Orbital functionality.
/// </summary>
[RequireComponent(typeof(Orbital))]
public class MoveOrbital : MonoBehaviour
{
    [SerializeField] public Vector3 minValues;

    [SerializeField] public Vector3 maxValues;

    private Orbital _orbital;
    private ObjectManipulator _manipulator;
// Start is called before the first frame update
    void Start()
    {
        _orbital = GetComponent<Orbital>();
        _manipulator = GetComponent<ObjectManipulator>();
    }

    public void StartInteraction()
    {
        _orbital.enabled = false;
    }

    public void StopInteraction()
    {
        var offset = Camera.main.transform.worldToLocalMatrix.MultiplyPoint(transform.position);
        offset.x = Mathf.Clamp(offset.x, minValues.x, maxValues.x);
        offset.y = Mathf.Clamp(offset.y, minValues.y, maxValues.y);
        offset.z = Mathf.Clamp(offset.z, minValues.z, maxValues.z);

        _orbital.LocalOffset = offset;
        _orbital.enabled = true;
    }

    public void SwitchGrabbable()
    {
        _manipulator.enabled = !_manipulator.enabled;
    }
}
