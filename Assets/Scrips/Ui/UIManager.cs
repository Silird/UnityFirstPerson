using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Image _sight;

    public static UIManager Instance;

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
        _sight = GameObject.Find("Sight").GetComponent<Image>();
        if (_sight == null)
        {
            Debug.Log("Sight not found");
        }
    }

    public void SetSightSize(float size)
    {
        _sight.rectTransform.sizeDelta = new Vector2(size, size);
    }
}
