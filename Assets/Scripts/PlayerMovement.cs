using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float runSpeed;
    [SerializeField] [Range(0f, 0.5f)] float _speedX;
    [SerializeField] CollisionControllerPlayer collisionControllerPlayer;

    private float _boundaryX = 1.5f;
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;

    private Animator _playerAnim;
  
    void Start()
    {
        _playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.isStartGame && !collisionControllerPlayer.isRaceFinished)
        {
            InputSystem();
            MoveSystem();
        }
        else if (collisionControllerPlayer.isRaceFinished)
        {
            _playerAnim.SetFloat("Speed", 0);
        }

        if (GameManager.Instance._isLevelScreenActivate)
        {
            GameManager.Instance.EndGameDance(transform.gameObject ,_playerAnim);
        }
    }

    private void InputSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = _lastFrameFingerPositionX - Input.mousePosition.x;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
        }
        float xBondrey = Mathf.Clamp(transform.position.x, -_boundaryX, _boundaryX);
        transform.position = new Vector3(xBondrey, transform.position.y, transform.position.z);
    }

    private void MoveSystem()
    {
        float swerve = Time.fixedDeltaTime * _moveFactorX * -_speedX;
        transform.Translate(swerve, 0, runSpeed * Time.deltaTime);

        _playerAnim.SetFloat("Speed", runSpeed);
    }
}
