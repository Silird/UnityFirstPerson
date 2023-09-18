using UnityEngine;
using System;

public static class Util
{
    public static readonly System.Random Rand = new();
    public static T GetChildComponentByName<T>(MonoBehaviour source, string name) where T : Component
    {
        foreach (var component in source.GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }

    /*
     * Return gauss random from 0 to 1
     * Near 0 more frequently
    */
    public static float GaussRandom()
    {

        const float mean = 0; // Мат ожидание
        const float stdDev = 0.7f; // Среднеквадратичное отклонение

        float result;
        do
        {
            var u1 = 1.0 - Rand.NextDouble(); //uniform(0,1] random doubles
            var u2 = 1.0 - Rand.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            var randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            result = (float)randNormal;
        } while ((result < 0) || (result >= 1));

        return result;
    }

    public static Vector3 TransformRelative(Transform parent, Vector3 transitionVector) 
    {
        var scale = Vector3.one;
        var coordinateSystem = Matrix4x4.TRS(parent.position, parent.rotation, scale);

        return coordinateSystem.MultiplyPoint(transitionVector);
    }
}
