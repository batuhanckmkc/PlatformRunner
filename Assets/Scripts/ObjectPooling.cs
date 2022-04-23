using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling objectPooling;

    [SerializeField]
    private GameObject _opponentPrefab;
    [SerializeField]
    private Queue<GameObject> opponentPool = new Queue<GameObject>();
    [SerializeField]
    private int _poolStartSize = 10;
    private float enemyStartPosZ = 1f;
    private float enemyStartPosX = 1f;

    private Vector3 enemyStartPos;

    private void Awake()
    {
        objectPooling = this;
    }
    void Start()
    {
        enemyStartPos = transform.position;
        for (int i = 0; i < _poolStartSize; i++)
        {
            enemyStartPos.x += enemyStartPosX;
            if (i % 5 == 0)
            {
                enemyStartPos.x = -2f;
                enemyStartPos.z += enemyStartPosZ;
            }

            //if (i % 2 == 0)
            //{
            //    if (!deneme)
            //    {
            //        enemyStartPos.x += enemyStartPosX * i;
            //    }
            //    else
            //    {
            //        enemyStartPos.x += enemyStartPosX * i;
            //    }
            //}
            //if (i % 2 != 0)
            //{
            //    enemyStartPos.x -= enemyStartPosX * i;
            //}
            //if (i % 5 == 0)
            //{
            //    enemyStartPos.x = 0;
            //    enemyStartPos.z += 0.5f;
            //    deneme = true;
            //}
            GameObject opponent = Instantiate(_opponentPrefab, enemyStartPos, Quaternion.identity, transform);
            opponentPool.Enqueue(opponent);
            opponent.SetActive(true);
        }
    }

    public GameObject GetOpponent()
    {
        if (opponentPool.Count > 0)
        {
            GameObject opponent = opponentPool.Dequeue();
            opponent.SetActive(true);
            return opponent;
        }
        else
        {
            GameObject opponent = Instantiate(_opponentPrefab);
            return opponent;
        }
    }

    public void ReturnOpponent(GameObject opponent)
    {
        opponentPool.Enqueue(opponent);
        opponent.SetActive(false);
    }
}
