using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAIState : State {
  public PlayerBrain Brain;
  private CharacterInteractable Interactable;

  public PlayerAIState(PlayerBrain brain) {
    Brain = brain;
    Interactable = Brain.GetComponentInChildren<CharacterInteractable>(true);
  }

  public override void Enter() {
    Interactable.gameObject.SetActive(true);
  }

  public override IEnumerator Execute() {
    while (true) {
      Vector3 target = new Vector2(Random.Range(-11, 11), Random.Range(-11, 2));

      while (Vector2.Distance(target, Brain.transform.position) > 1.5f) {
        Brain.Entity.Direction = (target - Brain.transform.position).normalized;
        yield return null;
      }

      Brain.Entity.Direction = Vector2.zero;

      yield return new WaitForSeconds(Random.Range(4, 5));
    }
  }

  public override void Exit() {
    Interactable.gameObject.SetActive(false);
  }
}