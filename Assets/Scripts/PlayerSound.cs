using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip _bress;
    [SerializeField] private AudioSource _bressSource;
    
    [SerializeField] private AudioClip _findingMoney;
    [SerializeField] private AudioSource _findingMoneySource;
    
    private Player _player;

    private void Start()
    {
        _bressSource.clip = _bress;
        _findingMoneySource.clip = _findingMoney;
        
        _player = FindObjectOfType<Player>();
        _player.OnPutHand += () =>
        {
            _bressSource.Play();
            _findingMoneySource.Play();
        };

        _player.OnPullOutHand += () =>
        {
            _bressSource.Stop();
            _findingMoneySource.Stop();
        };
    }
}
