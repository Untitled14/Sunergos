using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static AudioController Instance;

    public SoundDictionary[] SoundContainer;
    public Dictionary<string, GameObject> Sounds;
    private Dictionary<string, GameObject> _activeSounds;
    // Use this for initialization
    void Start () {
        Instance = this;
        Sounds = new Dictionary<string, GameObject>();
        _activeSounds = new Dictionary<string, GameObject>();
        foreach (var sound in SoundContainer)
        {
            if(sound.Audio.GetComponent<AudioData>() != null)
                sound.Audio.GetComponent<AudioData>().Pitch = sound.Audio.GetComponent<AudioSource>().pitch;
            
            if (sound.Name != "" && sound.Audio != null && !Sounds.ContainsKey(sound.Name))
                Sounds.Add(sound.Name, sound.Audio);
        }
	}
    public void PlaySound(string key = "", float pitchDiff = 0, float volume = 1)
    {
        float diff = Random.Range(-pitchDiff, pitchDiff);
        if(Sounds.ContainsKey(key))
        {
            if (!_activeSounds.ContainsKey(key))
            {
                GameObject sound = Instantiate(Sounds[key]) as GameObject;
                sound.transform.parent = transform;
                _activeSounds.Add(key,sound);
            }
            else
            {
                if(_activeSounds[key].GetComponent<AudioData>() != null)
                    _activeSounds[key].GetComponent<AudioSource>().pitch = _activeSounds[key].GetComponent<AudioData>().Pitch + diff;
                _activeSounds[key].GetComponent<AudioSource>().volume = volume;
                _activeSounds[key].GetComponent<AudioSource>().Play();
            }
        }
    }

    [System.Serializable]
    public class SoundDictionary
    {
        public string Name = "sound";
        public GameObject Audio;
    }
}
