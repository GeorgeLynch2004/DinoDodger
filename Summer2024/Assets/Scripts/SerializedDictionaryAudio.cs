using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class SerializedDictionaryAudio : MonoBehaviour
    {
        [SerializedDictionary("Audio Name", "Audio Clip")]
        public SerializedDictionary<string, AudioClip> ElementDescriptions;
    }
}