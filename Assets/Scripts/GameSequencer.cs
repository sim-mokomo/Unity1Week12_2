using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using naichilab;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private Player _thief;
    [SerializeField] private SleepHuman _sleepHuman;
    [SerializeField] private UICollentMoney _uiCollentMoney;
    [SerializeField] private UITitle _uiTitle;
    [SerializeField] private SpriteRenderer _forground;
    [SerializeField] private float _fadeInForground;
    [SerializeField] private GameObject _tweetCanvas;

    private int _money = 100;
    private int _totalMoney = 0;

    public bool IsGameEnd { get; private set; } = false;
    public bool IsEndInitialize { get; private set; } = false;
    public event Action OnStartGame;
    public event Action OnEndGame;
    public event Action OnAcquireMoney;
    
    void Start()
    {
        IsGameEnd = false;
        IsEndInitialize = false;
        _totalMoney = 0;
        _tweetCanvas.gameObject.SetActive(false);
        _uiCollentMoney.Show(false);
        
        OnEndGame += () =>
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (_totalMoney);
        };
     
        _thief.Initialize();
        _thief.OnPutHand += () =>
        {
            if (_uiTitle.IsShowing)
            {
                _uiCollentMoney.Show(true);
                _uiTitle.Show(false);
                _sleepHuman.StartSleepSequence();
                OnStartGame?.Invoke();
            }
        };

        _thief.OnFinding += () => { _money += UnityEngine.Random.Range(100,200); };
        
        _thief.OnPullOutHand += () =>
        {
            _uiCollentMoney.ApplyAcquireMoney(_money);
            _uiCollentMoney.ApplyTotalMoney(_totalMoney + _money);
            _totalMoney += _money;
            _money = 0;
            OnAcquireMoney?.Invoke();
        };
        
        _sleepHuman.Initialize();

        DOTween.ToAlpha(
            () => _forground.color,
            (color) => _forground.color = color,
            0f,
            _fadeInForground).onComplete = () => IsEndInitialize = true;
    }

    void Update()
    {
        if (!IsEndInitialize)
            return;
        if (IsGameEnd)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thief.PutHand();
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            _thief.Finding();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _thief.PullOutHand();
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
        IsGameEnd = true;
        DOTween.ToAlpha(
            () => _forground.color,
            (color) => _forground.color = color,
            1f,
            _fadeInForground).onComplete = () =>
        {
            _tweetCanvas.gameObject.SetActive(true);
            _thief.Kill();
            OnEndGame?.Invoke();
        };
    }

    private IEnumerator Wait(float delay,Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void OnClickTweet()
    {
        naichilab.UnityRoomTweet.Tweet("thief",$"{_totalMoney}円盗んだ。ただし死んだ。@sim_mokomo",new string[]{"unityroom","unity1week"});
        SceneManager.LoadScene(0);
    }
}
