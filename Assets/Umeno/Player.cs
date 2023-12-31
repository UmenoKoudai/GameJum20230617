using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    [SerializeField] PlayerNo _playerNo;
    public void PlayerMove(float endValue)
    {
        float distance = transform.position.x + endValue < GameManager.instance.Distance ? transform.position.x + endValue : GameManager.instance.Distance;
        Debug.Log($"実際に進距離{distance}");
        if (_playerNo == PlayerNo.Player1)
        {
            Debug.Log($"Player1が{endValue}進んだ");
            transform.DOMoveX(distance, _moveSpeed).OnComplete(() => GameManager.instance.MoveDistance((int)endValue, 0));
        }
        if (_playerNo == PlayerNo.Player2)
        {
            Debug.Log($"Player2が{endValue}進んだ");
            transform.DOMoveX(distance, _moveSpeed).OnComplete(() => GameManager.instance.MoveDistance(0, (int)endValue));
        }
    }
    enum PlayerNo
    {
        Player1,
        Player2,
    }
}
