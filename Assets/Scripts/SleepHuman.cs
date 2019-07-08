using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepHuman : MonoBehaviour
{
    [SerializeField] private float _sleepTimeMin;
    [SerializeField] private float _sleepTimeMax;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _sleepColor;
    [SerializeField] private Color _findStealColor;

    private Vector3 _sleepRot;   
    private Coroutine _sleepHandler;
    private SpriteRenderer _renderer;
    
    public bool IsSleeping { get; private set; }

    public SpriteRenderer Renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<SpriteRenderer>();
            return _renderer;
        }
    }

    public void Initialize()
    {
        _sleepRot = transform.eulerAngles;
        IsSleeping = true;
    }

    public void FindSteal()
    {
        Renderer.color = _findStealColor;
    }

    public void StartSleepSequence()
    {
        _sleepHandler = StartCoroutine(SleepSequence());
    }

    public void StopSleepSequence()
    {
        StopCoroutine(_sleepHandler);
    }
    
    private IEnumerator SleepSequence()
    {
        while (true)
        {
            yield return GetSleepTime();
            IsSleeping = false;
            Renderer.color = _baseColor;
            transform.rotation = Quaternion.identity;
            yield return new WaitForSeconds(0.5f);
            IsSleeping = true;
            Renderer.color = _sleepColor;
            transform.rotation = Quaternion.Euler(_sleepRot);
        }   
    }

    private WaitForSeconds GetSleepTime()
    {
        return new WaitForSeconds(Random.Range(_sleepTimeMin,_sleepTimeMax));
    }
}
