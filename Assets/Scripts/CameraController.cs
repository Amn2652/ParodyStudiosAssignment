using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{

    public CinemachineFreeLook vcam;
    public float rotationY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

  
    void Update()
    {
        var state = vcam.State;

        var rotation = state.FinalOrientation;

        var euler = rotation.eulerAngles;

        rotationY = euler.y;

        var RoundedRotation = Mathf.RoundToInt(rotationY);
    }

    public Quaternion FlatRotation() => Quaternion.Euler(0,rotationY,0);
}
