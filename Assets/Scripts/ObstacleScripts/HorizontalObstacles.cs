using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HorizontalObstacles : RotatingPlatform
{
    private float _minX = -1.75f;
    private float _maxX =  1.75f;
    
    private void Start()
    {
        StartCoroutine(nameof(HorizontalPace));
    }

    IEnumerator HorizontalPace()
    {

        if (Vector3.Distance(transform.position, new Vector3(_maxX, transform.position.y, transform.position.z)) > 0.75f)
        {
            transform.DOMoveX(_maxX, 3f);
        }
        if(Vector3.Distance(transform.position, new Vector3(_minX, transform.position.y, transform.position.z)) > 0.75f)
        {
            transform.DOMoveX(_minX, 3f);
            
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(HorizontalPace));
    }

    private void Update()
    {
        RotateObject();
    }

    public override void RotateObject()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
