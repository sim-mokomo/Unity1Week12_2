using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UITitle : MonoBehaviour
{
    [SerializeField] private Text _title;
    [SerializeField] private Text _start;
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;

    public bool IsShowing { get; private set; } = true;

    public void Show(bool show)
    {
        IsShowing = show;
        if (show)
        {
            DOTween.ToAlpha(
                () => _title.color,
                (color) =>
                {
                    _title.color = color;
                    _start.color = color;
                },
                1f,
                _fadeInDuration);
        }
        else
        {
            DOTween.ToAlpha(
                () => _title.color,
                (color) =>
                {
                    _title.color = color;
                    _start.color = color;
                },
                0f,
                _fadeOutDuration);
        }
    }
}
