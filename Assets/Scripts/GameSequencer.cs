﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSequencer : MonoBehaviour
{
    [SerializeField] private Player _thief;
    [SerializeField] private SleepHuman _sleepHuman;
    [SerializeField] private UICollentMoney _uiCollentMoney;
    [SerializeField] private UITitle _uiTitle;
    [SerializeField] private SpriteRenderer _forground;
    [SerializeField] private float _endWaitTime;
    [SerializeField] private float _fadeInForground;

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
        _uiCollentMoney.gameObject.SetActive(false);
     
        _thief.Initialize();
        _thief.OnPutHand += () =>
        {
            if (_uiTitle.IsShowing)
            {
                _uiCollentMoney.gameObject.SetActive(true);
                _uiTitle.Show(false);
                _sleepHuman.StartSleepSequence();
                OnStartGame?.Invoke();
            }
        };
        _thief.OnPullOutHand += () =>
        {
            _uiCollentMoney.ApplyAcquireMoney(_money);
            _uiCollentMoney.ApplyTotalMoney(_totalMoney + _money);
            _money += _money;
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
            return;
        
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
        IsGameEnd = true;
        DOTween.ToAlpha(
            () => _forground.color,
            (color) => _forground.color = color,
            1f,
            _fadeInForground).onComplete = () =>
        {
            OnEndGame?.Invoke();
            StartCoroutine(Wait(_endWaitTime, () => { SceneManager.LoadScene(0); }));
        };
    }

    private IEnumerator Wait(float delay,Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
