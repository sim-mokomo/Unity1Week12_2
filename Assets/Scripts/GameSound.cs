using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _bgm;
    [SerializeField] private AudioSource _seSource;
    [SerializeField] private AudioClip _acquireMoney;
    private GameSequencer _gameSequencer;

    private void Start()
    {
        _bgmSource.clip = _bgm;
        _bgmSource.loop = true;
        _bgmSource.Play();
        _gameSequencer = GetComponent<GameSequencer>();
        _gameSequencer.OnAcquireMoney += () => { _seSource.PlayOneShot(_acquireMoney); };
    }
}
