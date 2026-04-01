using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class Sign : MonoBehaviour
{
    public GameObject signSprite;
    public Transform playerTrans;

    private Animator anim;
    private PlayerInputControl inputControl;
    private IInteractable targetItem;

    private bool canPress;
    private string animName;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();

        inputControl = new PlayerInputControl();
        inputControl.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        inputControl.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnDisable()
    {
        canPress = false;
    }

    private void Update()
    {
        signSprite.SetActive(canPress);
        signSprite.transform.localScale = playerTrans.localScale;

        if (canPress)
            anim.Play(animName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }

        if (collision.CompareTag("Interacted"))
            canPress = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
            canPress = false;
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;

            switch (d.device)
            {
                case Keyboard:
                    animName = "Keyboard";
                    break;
                case DualShockGamepad:
                    animName = "PS";
                    break;
            }
        }
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
            targetItem.TriggerAction();
    }
}
