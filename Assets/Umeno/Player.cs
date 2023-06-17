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
        float distance = transform.position.x + endValue;
        Debug.Log($"���ۂɐi����{distance}");
        if (_playerNo == PlayerNo.Player1)
        {
            Debug.Log($"Player1��{endValue}�i��");
            transform.DOMoveX(distance, _moveSpeed);
        }
        if (_playerNo == PlayerNo.Player2)
        {
            Debug.Log($"Player2��{endValue}�i��");
            transform.DOMoveX(distance, _moveSpeed);
        }
    }
    enum PlayerNo
    {
        Player1,
        Player2,
    }
}
