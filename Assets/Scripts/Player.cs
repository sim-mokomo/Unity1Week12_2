﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _putHandDistTP;
    [SerializeField] private float _putHandDuration;

    private Vector3 _initPos;
    private Vector3 _initRot;

    public event Action OnPutHand;
    public event Action OnPullOutHand;

    public void Initialize()
    {
        _initPos = transform.position;
        _initRot = transform.eulerAngles;

        OnPutHand += () =>
        {
            transform.DOMove(_putHandDistTP.transform.position, _putHandDuration);
            transform.DORotate(_putHandDistTP.transform.rotation.eulerAngles, _putHandDuration);
        };

        OnPullOutHand += () =>
        {
            transform.DOMove(_initPos, _putHandDuration);
            transform.DORotate(_initRot, _putHandDuration);
        };
    }

    public void PutHand()
    {
        OnPutHand?.Invoke();
    }

    public void PullOutHand()
    {
        OnPullOutHand?.Invoke();
    }
}