using System;
using UnityEngine;

public class ToolUse : MonoBehaviour
{
    private InventoryManager _inventoryManager;

    private ToolHandler _stubHandler;
    private ToolHandler _bowFiring;

    // private Tool _equippedTool;
    private ToolHandler _toolHandler;

    private void Awake()
    {
        InventoryManager.ToolChanged += tool => {
            // _equippedTool = tool;
             
            if (tool == null)
            {
                _toolHandler = _stubHandler;
            } else if (tool.GetType() == typeof(Bow))
            {
                _toolHandler = _bowFiring;
            } else
            {
                _toolHandler = _stubHandler;
            }
        };
    }

    private void Start()
    {
        //Получаем данные о камере
        // _camera = Camera.main;

        _bowFiring = GetComponent<BowFiring>();
        if (_bowFiring == null)
        {
            Debug.Log("BowFiring component not found");
        }
        _inventoryManager = GetComponent<InventoryManager>();
        if (_inventoryManager == null)
        {
            Debug.Log("InventoryManager component not found");
        }

        _stubHandler = gameObject.AddComponent<ToolHandler>();

        //Скроем указатель мыши в Game окошке (чтоб его обратно активировать нажми - esp)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _inventoryManager.EquipItem("Bow");
    }

    private void Update()
    {
        // Проверяем, когда нажимаем ЛКМ нажата
        if (Input.GetMouseButtonDown(0))
        {
            _toolHandler.MouseDown();
        }
        // Проверяем, когда нажимаем ЛКМ зажата
        if (Input.GetMouseButton(0))
        {
            _toolHandler.Mouse();
        }
        // Проверяем, когда нажимаем ЛКМ отжата
        if (Input.GetMouseButtonUp(0))
        {
            _toolHandler.MouseUp();
        }

        // Проверяем, когда нажимаем ЛКМ отжата
        if (Input.GetMouseButtonDown(1))
        {
            // _inventoryManager.EquipItem("Bow");
        }
    }
}
