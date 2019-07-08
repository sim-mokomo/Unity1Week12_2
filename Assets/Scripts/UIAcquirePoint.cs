using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAcquirePoint : MonoBehaviour
{
    [SerializeField] private Text _point;

    public void ApplyPoint(int point)
    {
        var prefix = point > 0 ? "+" : point < 0 ? "-" : "";
        _point.text = $"{prefix}{point}";
    }
}
