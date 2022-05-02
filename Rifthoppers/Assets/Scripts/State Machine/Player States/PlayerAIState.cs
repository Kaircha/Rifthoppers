using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAIState : State {
  public Entity Entity;
  private CharacterInteractable Interactable;

  public PlayerAIState(Entity entity) { 
    Entity = entity;
    Interactable = Entity.GetComponentInChildren<CharacterInteractable>(true);
  }

  public override void Enter() {
    Interactable.gameObject.SetActive(true);
  }

  public override IEnumerator Execute() {
    while (true) {
      // Logic for walking to a location
      Entity.Direction = Vector2.zero;
      yield return null;
    }
  }

  public override void Exit() {
    Interactable.gameObject.SetActive(false);
  }
}
