using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CollisionControllerPlayer : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float interactTork;
    [SerializeField] GameObject paintWall;
    
    public bool isRaceFinished;


    private void Start()
    {
        isRaceFinished = false;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            Debug.Log("Bring the PaintWall!");
            isRaceFinished = true;
            GameManager.Instance.PaintAction(paintWall, gameObject, other.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StaticObstacle" || collision.gameObject.tag == "MovingObstacle")
        {
            GameManager.Instance.JumpToBase(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatformLeft")
        {
            GameManager.Instance.RotatingPlatformLeft(gameObject, interactTork);
        }
        if (collision.gameObject.tag == "RotatingPlatformRight")
        {
            GameManager.Instance.RotatingPlatformRight(gameObject, interactTork);
        }
    }
}
