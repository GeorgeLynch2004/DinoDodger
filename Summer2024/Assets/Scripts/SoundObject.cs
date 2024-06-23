using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(DespawnCountdown());
    }


    public void SetAudioClip(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;
    }

    private IEnumerator DespawnCountdown()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
