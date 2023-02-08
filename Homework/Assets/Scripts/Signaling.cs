using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _targetVolume;
    private float _volumeSpeed = 0.1f;
    private bool _isPlayerInsideHouse;

    private void Start()
    {
        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _isPlayerInsideHouse = true;
            _audioSource.Play();
            StartCoroutine(ChangeSoundVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerInsideHouse = false;
            StartCoroutine(ChangeSoundVolume());
        }
    }

    private IEnumerator ChangeSoundVolume()
    {
        if (_isPlayerInsideHouse)
        {
            _targetVolume = 1f;
        }
        else
        {
            _targetVolume = 0f;
        }

        while (_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeSpeed * Time.deltaTime);
            yield return null;
        }

        if(_targetVolume == 0)
        {
            _audioSource.Stop();
        }
    }
}
