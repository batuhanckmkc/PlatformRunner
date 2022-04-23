using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{


    private Transform _finishLine;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _currentPos;
    private Animator _opponentAnim;

    [SerializeField] GameObject _endGameParticle;

    void Start()
    {
        _finishLine = GameObject.FindGameObjectWithTag("FinishLine").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentPos = transform.position;
        _opponentAnim = GetComponent<Animator>();
    }

    void Update()
    {
        OpponentMove();
    }

    private void OpponentMove()
    {
        if (GameManager.Instance.isStartGame)
        {
            _navMeshAgent.SetDestination(new Vector3(_currentPos.x, _currentPos.y, _finishLine.position.z));
            _opponentAnim.SetFloat("Speed", _navMeshAgent.speed);
        }
    }

    public void RaceEnd(GameObject gameObject)
    {
        ObjectPooling.objectPooling.ReturnOpponent(gameObject);
        GameObject endGameParticle = Instantiate(_endGameParticle, transform.position, Quaternion.identity);
        Destroy(endGameParticle, 1f);
    }
}
