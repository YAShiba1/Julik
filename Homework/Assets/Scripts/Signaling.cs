using System.Collections;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _targetVolume;
    private float _volumeSpeed = 0.2f;
    private Coroutine _changeVolumeJob;

    public void IncreaseVolume()
    {
        if (_changeVolumeJob != null)
        {
            StopCoroutine(_changeVolumeJob);
        }

        _targetVolume = 1;
        _audioSource.Play();
        _changeVolumeJob = StartCoroutine(ChangeSoundVolume());
    }

    public void DecreaseVolume()
    {
        _targetVolume = 0;
    }

    private void Start()
    {
        _audioSource.volume = 0;
    }

    private IEnumerator ChangeSoundVolume()
    {
        while (_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeSpeed * Time.deltaTime);
            yield return null;
        }

        if (_targetVolume == 0)
        {
            _audioSource.Stop();
        }
    }
}
