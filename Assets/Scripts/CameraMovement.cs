using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float delay;

    // Update is called once per frame
    void MovementDelay()
    {
        if (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, delay);
        }
    }
}
