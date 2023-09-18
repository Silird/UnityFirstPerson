using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem stub;
    [SerializeField]
    private List<Effect> effects = new();

    public static EffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (stub == null)
        {
            Debug.LogWarning("Stub not found!");
        }
    }

    public ParticleSystem Get(string effectName)
    {
        var effect = effects.Find(effect => effect.name == effectName);
        if (effect == null)
        {
            Debug.LogWarning("Effect with name \"" + effectName + "\" not found");
            return stub;
        }
        return effect.effect;
    }
}
