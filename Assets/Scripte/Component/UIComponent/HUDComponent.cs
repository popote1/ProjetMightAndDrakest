﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUDComponent : MonoBehaviour
{
    
    [Header("TechnicalShit")] public Slider InventorySlyder;
    public float TimeToOpeninventory;
    public CanvasGroup canvasGroup;
    public InventoryComponent InventoryComponent;
    public GameObject PanelInventory;
    public PlayerInput PlayerInput;
    public GameObject PanelHUD;
    public static bool IsExploring;
    private float _inventorytimer;
    private bool isClicked;
    private bool _showInventoryBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (isClicked)
        {
            _inventorytimer += Time.deltaTime;
            if (_inventorytimer > TimeToOpeninventory / 5) _showInventoryBar = true;
            if (_inventorytimer > TimeToOpeninventory)
            {
                _inventorytimer = TimeToOpeninventory;
                PanelInventory.SetActive(true);
                PanelHUD.SetActive(false);
                PlayerInput.actions.FindActionMap("ExplorationControl").Disable();
                PlayerInput.actions.FindActionMap("MenuControl").Enable();
                InventoryComponent.LaunchInventory();
            }
            InventorySlyder.value = _inventorytimer / TimeToOpeninventory;
        }
        else if(!isClicked&&_inventorytimer!=0)
        {
            _inventorytimer -= Time.deltaTime;
            if (_inventorytimer < TimeToOpeninventory / 5) _showInventoryBar = false;
            if (_inventorytimer < 0) _inventorytimer = 0;
            InventorySlyder.value = _inventorytimer / TimeToOpeninventory;
        }

        if (_showInventoryBar)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 0.1f);
        }
        else
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, 0.1f);
        }

        
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Click");
        if (callbackContext.started)
        {
            isClicked = true;
        }
        if (callbackContext.canceled)
        {
            isClicked = false;
        }
    }

    public void CloseInventory()
    {
        PanelInventory.SetActive(false);
        PanelHUD.SetActive(true);
        PlayerInput.actions.FindActionMap("ExplorationControl").Enable();
        PlayerInput.actions.FindActionMap("MenuControl").Disable();
    }
}
