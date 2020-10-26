using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera System is Simple just follow the Player around

    [SerializeField] GameObject Target;
    [SerializeField] float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 newPos = Target.transform.position;
            newPos.z = -10;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
        }
    }
}
