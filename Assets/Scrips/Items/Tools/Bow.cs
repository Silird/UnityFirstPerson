using UnityEngine;

public class Bow : Tool
{
    [SerializeField]
    public float sightMin = 20;
    [SerializeField]
    public float sightMax = 100;
    [SerializeField]
    public float aimSpeed = 50; // 20;
}
