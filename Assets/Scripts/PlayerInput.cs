using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Actions playerInput;
    Player playerController;

    
    void Awake() {
        playerInput = new Actions();
        playerController = gameObject.GetComponent<Player>();
        playerInput.Player.Jump.performed += ctx => playerController.Jump();
        playerInput.Player.SpellA.performed += ctx => playerController.CastPrimarySpell();
        playerInput.Player.SpellB.performed += ctx => playerController.CastSecondarySpell();
    }

    
    void Update() {
        playerController.Locomotion(playerInput.Player.Locomotion.ReadValue<Vector2>());
        playerController.BaseAttack(playerInput.Player.Aim.ReadValue<Vector2>());
    }

    void OnEnable() {
        playerInput.Player.Enable();
    }

    void OnDisable() {
        playerInput.Player.Disable();
    }
}
