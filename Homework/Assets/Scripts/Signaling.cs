using System.Collections;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _volumeSpeed = 0.4f;
    private Coroutine _changeVolumeJob;

    public void IncreaseVolume()
    {
        float maxVolume = 1f;

        StopVolumeCoroutine();

        _audioSource.Play();
        _changeVolumeJob = StartCoroutine(ChangeSoundVolume(maxVolume));
    }

    public void DecreaseVolume()
    {
        float minVolume = 0;

        StopVolumeCoroutine();

        _changeVolumeJob = StartCoroutine(ChangeSoundVolume(minVolume));
    }

    private void Start()
    {
        _audioSource.volume = 0;
    }

    private IEnumerator ChangeSoundVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeSpeed * Time.deltaTime);
            yield return null;
        }

        if (targetVolume == 0)
        {
            _audioSource.Stop();
        }
    }

    private void StopVolumeCoroutine()
    {
        if (_changeVolumeJob != null)
        {
            StopCoroutine(_changeVolumeJob);
        }
    }
}
