using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static event Action<Tool> ToolChanged;
    public Tool EquipItem(string itemName)
    {
        var item = ItemManager.Instance.Get(itemName);
        var newItem = Instantiate(item);
        var tool = newItem.GetComponent<Tool>();
        if (tool != null)
        {
            tool.TakeTool(gameObject);
            if (tool.mainTool)
            {
                if (ToolChanged != null)
                {
                    ToolChanged.Invoke(tool);
                }
            }

            return tool;
        }

        return null;
    }
}
