using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private Player _thief;
    [SerializeField] private SleepHuman _sleepHuman;
    [SerializeField] private UICollentMoney _uiCollentMoney;
    [SerializeField] private UITitle _uiTitle;

    private int _money = 100;
    private int _totalMoney = 0;
    
    void Start()
    {
        _totalMoney = 0;
     
        _thief.Initialize();
        _thief.OnPutHand += () =>
        {
            if (_uiTitle.IsShowing)
            {
                _uiTitle.Show(false);
                _sleepHuman.StartSleepSequence();
            }
        };
        _thief.OnPullOutHand += () =>
        {
            _uiCollentMoney.ApplyAcquireMoney(_money);
            _uiCollentMoney.ApplyTotalMoney(_totalMoney + _money);
            _money += _money;
        };
        
        _sleepHuman.Initialize();
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
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _sleepHuman.StopSleepSequence();
            BackTitle();
        }

        if (_thief.IsStealing && !_sleepHuman.IsSleeping)
        {
            _sleepHuman.FindSteal();
            _sleepHuman.StopSleepSequence();
            BackTitle();
        }
    }

    private void BackTitle()
    {
        _uiTitle.Show(true);
        SceneManager.LoadScene(0);
    }
}
