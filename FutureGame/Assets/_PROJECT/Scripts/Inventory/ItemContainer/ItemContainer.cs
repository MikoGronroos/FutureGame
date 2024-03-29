﻿using UnityEngine;
using System;

[Serializable]
public class ItemContainer
{

    [SerializeField] private int containerSize;

    private GameObject slotGameObject;
    private GameObject containerGameObject;

    private CameraLook _playerCameraLook;
    private CharacterMovement _characterMovement;
    private PlayerAttack _playerAttack;

    public void InitContainer(GameObject containerObject)
    {
        _playerCameraLook = MonoBehaviour.FindObjectOfType<CameraLook>();
        _characterMovement = MonoBehaviour.FindObjectOfType<CharacterMovement>();
        _playerAttack = MonoBehaviour.FindObjectOfType<PlayerAttack>();
        slotGameObject = Resources.Load("InventorySlot") as GameObject;
        containerGameObject = containerObject;
    }

    public int GetContainerSize()
    {
        return containerSize;
    }

    public GameObject GetSlotGameObject()
    {
        return slotGameObject;
    }

    public bool ToggleContainer(bool value)
    {
        if (value)
        {
            CursorVisibility.SetCursorHidden();
            _playerCameraLook.enabled = true;
            _characterMovement.enabled = true;
            _playerAttack.enabled = true;
            containerGameObject.SetActive(false);
            return false;
        }
        else
        {
            CursorVisibility.SetCursorVisible();
            _playerCameraLook.enabled = false;
            _characterMovement.enabled = false;
            _playerAttack.enabled = false;
            containerGameObject.SetActive(true);
            return true;
        }
    }
}
