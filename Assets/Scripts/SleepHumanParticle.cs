using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepHumanParticle : MonoBehaviour
{
    private SleepHuman _sleepHuman;
    [SerializeField] private ParticleSystem _sleepVfx;

    private void Start()
    {
        _sleepHuman = GetComponent<SleepHuman>();
        _sleepHuman.OnWokeUp += () => { _sleepVfx.Stop(); };
        _sleepHuman.OnSleep += () => {_sleepVfx.Play(); };
    }    
}
