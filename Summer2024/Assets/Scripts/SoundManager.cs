using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;


public class SoundManager : MonoBehaviour
{
    private SerializedDictionaryAudio audioDictionary;
    [SerializeField] private GameObject soundObjectPrefab;

    private void Start() {
        audioDictionary = GetComponent<SerializedDictionaryAudio>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName)
    {
        // get the corresponding audio clip.
        audioDictionary.ElementDescriptions.TryGetValue(soundName, out AudioClip soundToPlay);

        if (soundToPlay != null)
        {
            Debug.Log(soundToPlay);
            // create a new sound game object that plays the sound and then kills itself.
            GameObject newSoundObject = Instantiate(soundObjectPrefab, Vector2.zero, Quaternion.identity);
            AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();
            newSoundObject.GetComponent<SoundObject>().SetAudioClip(soundToPlay, audioSource);
        }
    }
}
