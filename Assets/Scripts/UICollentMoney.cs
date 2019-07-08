using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UICollentMoney : MonoBehaviour
{
    [SerializeField] private Text _totalMoney;
    [SerializeField] private GameObject _acquireMoneyRoot;
    [SerializeField] private GameObject _acquireMoneyPrefab;
    [SerializeField] private float _acquireMoneyMoveOffset;
    [SerializeField] private float _acquireMoneyMoveDuration;

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
}
