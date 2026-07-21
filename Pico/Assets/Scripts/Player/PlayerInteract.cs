using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public InputControl inputControl;

    private bool canPress;
    private IInteractable targetItem;

    private void Awake()
    {
        inputControl = new InputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();

        inputControl.Gameplay.Interact.started += OnInteract;
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interacted"))
        {
            canPress = false;
            targetItem = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = false;
            targetItem = null;
        }
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
        }
    }
}
