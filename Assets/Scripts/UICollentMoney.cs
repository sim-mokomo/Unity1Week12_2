using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UICollentMoney : MonoBehaviour
{
    [SerializeField] private Image _moneyIcon;
    [SerializeField] private Text _totalMoney;
    [SerializeField] private GameObject _acquireMoneyRoot;
    [SerializeField] private GameObject _acquireMoneyPrefab;
    [SerializeField] private float _acquireMoneyMoveOffset;
    [SerializeField] private float _acquireMoneyMoveDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    public void ApplyTotalMoney(int totalMoney)
    {
        _totalMoney.text = $"{totalMoney}";
    }

    public void ApplyAcquireMoney(int acquireMoney)
    {
        var acquire = Instantiate(_acquireMoneyPrefab, _acquireMoneyRoot.transform.position, Quaternion.identity,
            _acquireMoneyRoot.transform).GetComponent<UIAcquirePoint>();
        acquire.ApplyPoint(acquireMoney);
        acquire.transform.DOMoveY(_acquireMoneyMoveOffset, _acquireMoneyMoveDuration).onComplete =
            () => { Destroy(acquire.gameObject); };
    }

    public void Show(bool show)
    {
        if (show)
        {
            gameObject.SetActive(true);
            DOTween.ToAlpha(
                () => _totalMoney.color,
                (color) =>
                {
                    _totalMoney.color = color;
                    _moneyIcon.color = color;
                },
                1f,
                _fadeInDuration);
        }
        else
        {
            gameObject.SetActive(false);
//            DOTween.ToAlpha(
//                () => _totalMoney.color,
//                (color) =>
//                {
//                    _totalMoney.color = color;
//                    _moneyIcon.color = color;
//                },
//                0f,
//                _fadeOutDuration);
        }
    }
}
