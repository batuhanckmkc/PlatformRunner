using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionControllerAi : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float interactTork;
    private AiController _aiController;

    private void Start()
    {
        _aiController = GetComponent<AiController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            _aiController.RaceEnd(gameObject);
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
