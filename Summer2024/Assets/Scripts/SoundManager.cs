using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;


public class SoundManager : MonoBehaviour
{
    private SerializedDictionaryAudio audioDictionary;
    [SerializeField] private GameObject soundObjectPrefab;
    [SerializeField] private AudioSource gameMusic;

    private void Start() {
        audioDictionary = GetComponent<SerializedDictionaryAudio>();
        DontDestroyOnLoad(gameObject);
        gameMusic.Play();
    }

    public void PlaySound(string soundName)
    {
        // get the corresponding audio clip.
        audioDictionary.ElementDescriptions.TryGetValue(soundName, out AudioData audioData);
        AudioClip soundToPlay = audioData.Clip;
        float volume = audioData.Volume;

        if (soundToPlay != null)
        {
            // create a new sound game object that plays the sound and then kills itself.
            GameObject newSoundObject = Instantiate(soundObjectPrefab, Vector2.zero, Quaternion.identity);
            AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();
            audioSource.volume = volume;
            newSoundObject.GetComponent<SoundObject>().SetAudioClip(soundToPlay, audioSource);
        }
    }
}
