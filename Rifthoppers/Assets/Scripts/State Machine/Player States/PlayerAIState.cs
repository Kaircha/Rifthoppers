using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAIState : State {
  public Entity Entity;
  private CharacterInteractable Interactable;
  private float NormalSpeed, AISpeed;

  public PlayerAIState(Entity entity) { 
    Entity = entity;
    AISpeed = Entity.AISpeed;
    NormalSpeed = Entity.Speed;
    Interactable = Entity.GetComponentInChildren<CharacterInteractable>(true);
  }

  public override void Enter() {
    Interactable.gameObject.SetActive(true);
    Entity.Speed = AISpeed;
  }

  public override IEnumerator Execute() {
    while (true) {
      Vector3 target = new Vector2(Random.Range(-11, 11), Random.Range(-11, 2));

      while (Vector2.Distance(target, Entity.transform.position) > 1.5f) {
        Entity.Direction = (target - Entity.transform.position).normalized;
        yield return null;
      }

      Entity.Direction = Vector2.zero;

      yield return new WaitForSeconds(Random.Range(4, 5));
    }
  }

  public override void Exit() {
    Interactable.gameObject.SetActive(false);
    Entity.Speed = NormalSpeed;
  }
}