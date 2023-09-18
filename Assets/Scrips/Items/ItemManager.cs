using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stub;
    [SerializeField]
    private List<Item> items = new List<Item>();

    public static ItemManager Instance;

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

    public GameObject Get(string itemName)
    {
        var item = items.Find(item => item.name == itemName);
        if (item == null)
        {
            Debug.LogWarning("Item with name \"" + itemName + "\" not found");
            return stub;
        }
        return item.prefab;
    }
}
