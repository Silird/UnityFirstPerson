using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class Tool : MonoBehaviour
{
    public ToolType type;
    public Vector3 gripPosition;
    public Vector3 gripRotation;
    public bool mainTool = true;

    public void TakeTool(GameObject model)
    {
        var grip = GetGrip(model);
        if (grip != null)
        {
            var transform1 = transform;
            transform1.parent = grip.transform;
            transform1.localPosition = gripPosition;
            transform1.localEulerAngles = gripRotation;
        }
    }

    private GameObject GetGrip(GameObject model)
    {
        GameObject grip;
        switch (type)
        {
            case ToolType.RightHand:
                {
                    grip = GetRightGrip(model);
                    break;
                }
            case ToolType.LeftHand:
                {
                    grip = GetLeftGrip(model);
                    break;
                }
            case ToolType.TwoHanded:
                {
                    grip = GetTwoHandedGrip(model);
                    break;
                }
            default:
                {
                    Debug.LogWarning("Type " + type + " is unknown");
                    grip = null;
                    break;
                }
        }

        if (mainTool)
        {
            ClearAllGrips(model);
        } else
        {
            ClearGrip(grip);
        }
        return grip;
    }

    private GameObject GetRightGrip(GameObject model)
    {
        return model.transform.Find("Model/GripRightHandTool").gameObject;
    }

    private GameObject GetLeftGrip(GameObject model)
    {
        return model.transform.Find("Model/GripLeftHandTool").gameObject;
    }

    private GameObject GetTwoHandedGrip(GameObject model)
    {
        // return model.transform.Find("Model/GripRightHandTool").gameObject;
        return null;
    }

    private void ClearAllGrips(GameObject model)
    {
        ClearGrip(GetRightGrip(model));
        ClearGrip(GetLeftGrip(model));
        ClearGrip(GetTwoHandedGrip(model));
    }

    private void ClearGrip(GameObject grip)
    {
        if (grip != null)
        {
            foreach (Transform child in grip.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
