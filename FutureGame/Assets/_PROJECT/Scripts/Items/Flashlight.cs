using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [SerializeField] private Light lighSource;

    private CharacterOwner _charOwner;

    private bool _inventoryOpenState;

    private void Awake()
    {
        _charOwner = CharacterOwner.Instance;
    }

    private void Update()
    {
        if (_charOwner.Input.DefendInput())
        {
            if (_inventoryOpenState)
            {
                lighSource.enabled = false;
                _inventoryOpenState = false;
            }
            else if (!_inventoryOpenState)
            {
                lighSource.enabled = true;
                _inventoryOpenState = true;
            }
        }
    }

}
