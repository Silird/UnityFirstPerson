using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private List<Sound> sounds = new List<Sound>();

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(string soundName, GameObject soundSource = null)
    {
        var s = sounds.Find(sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound with name \"" + soundName + "\" not found");
            return;
        }
        
        if (soundSource == null)
        {
            soundSource = gameObject;
        }

        var source = soundSource.AddComponent<AudioSource>();
        source.clip = s.clip; 
        source.volume = s.volume;
        source.loop = s.loop;
        source.volume = s.volume;
        source.spatialBlend = s.thirdD;
        source.Play();
    }
}
