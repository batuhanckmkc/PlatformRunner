using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.rotation.x, 180 * Time.deltaTime, transform.rotation.z);
    }
}
