using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    [Serializable]
    public class AudioData
    {
        public AudioClip Clip;
        public float Volume;

        public AudioData(AudioClip clip, float volume)
        {
            Clip = clip;
            Volume = volume;
        }
    }

    public class SerializedDictionaryAudio : MonoBehaviour
    {
        [SerializedDictionary("Audio Name", "Audio Clip & Volume")]
        public SerializedDictionary<string, AudioData> ElementDescriptions;
    }
}