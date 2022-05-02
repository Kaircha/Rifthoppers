using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputData : MonoBehaviour {
  public Vector2 Move;
  public Vector2 Look;
  private Vector2 _look; // Mouse input needs to be recalculated when camera moves
  public bool IsMouseLook; // :(
  public bool Shoot;
  public bool Power;
  public bool Interact;
  public bool Dodge;
  
  public void OnMove(InputAction.CallbackContext context) {
    Move = context.ReadValue<Vector2>();
  }
  public void OnLook(InputAction.CallbackContext context) {
    Vector2 value = context.ReadValue<Vector2>();
    if (value == Vector2.zero) return;
    IsMouseLook = context.control.device.name == "Mouse";
    _look = value;
  }
  public void OnShoot(InputAction.CallbackContext context) {
    if (context.started) Shoot = true;
    if (context.canceled) Shoot = false;
  }
  public void OnPower(InputAction.CallbackContext context) {
    if (context.started) Power = true;
    if (context.canceled) Power = false;
  }
  public void OnInteract(InputAction.CallbackContext context) {
    if (context.started) Interact = true;
    if (context.canceled) Interact = false;
  }
  public void OnDodge(InputAction.CallbackContext context) {
    if (context.started) Dodge = true;
    if (context.canceled) Dodge = false;
  }

  private void Update() {
    // Mouse input needs to be recalculated when camera moves
    Look = IsMouseLook ? Camera.main.ScreenToWorldPoint(_look) : 15f * _look;
  }
}