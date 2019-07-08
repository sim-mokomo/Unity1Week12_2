using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private Player _thief;
    [SerializeField] private UICollentMoney _uiCollentMoney;

    private int _money = 100;
    private int _totalMoney = 0;
    
    void Start()
    {
        _totalMoney = 0;
     
        _thief.Initialize();
        _thief.OnPullOutHand += () =>
        {
            _uiCollentMoney.ApplyAcquireMoney(_money);
            _uiCollentMoney.ApplyTotalMoney(_totalMoney + _money);
            _money += _money;
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thief.PutHand();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _thief.PullOutHand();
        }
    }
}
