using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] [Range(45f, 135f)] public float rotateSpeed;
    void Update()
    {
        if(gameObject.tag == "RotatingPlatformLeft")
        {
            RotateObject();
        }
        else
        {
            RotateObjectRight();
        }
    }

    public virtual void RotateObject()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    public virtual void RotateObjectRight()
    {
        transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
