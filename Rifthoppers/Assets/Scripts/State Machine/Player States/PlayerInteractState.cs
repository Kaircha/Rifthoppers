using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : State {
  public PlayerBrain Brain;
  private LayerMask InteractMask;
  private readonly float InteractRange = 3f;
  private IInteractable _interactable;
  public IInteractable Interactable {
    get => _interactable;
    set {
      if (value == _interactable) return;
      _interactable?.HideHighlight();
      _interactable = value;
      _interactable?.ShowHighlight();
    }
  }

  public PlayerInteractState(PlayerBrain brain) {
    Brain = brain;
    InteractMask = LayerMask.GetMask("Interactable");
  }

  public override void Enter() {
    Machine.StartCoroutine(InteractionRoutine());
  }

  public override IEnumerator Execute() {
    // Create a yield point as to avoid infinite indirect recursion between this and Interactable.Interact.
    // Without it, when interacting to change characters, Execute will not be able to reach a yield point.
    yield return null;
    
    while (true) {
      Brain.Entity.Direction = Brain.PlayerStats.Speed * 1.5f * Brain.Input.Move.normalized;

      if (Brain.Input.IsMouseLook) Brain.Target.position = Brain.Input.Look;
      else Brain.Target.localPosition = Brain.Input.Look;

      yield return null;
    }
  }

  public override void Exit() { }

  public IEnumerator InteractionRoutine() {
    while (true) {
      Interactable = GetInteractable();
      if (Brain.Input.Interact) {
        // Interaction occurs upon releasing the interact button
        // Both to really confirm the intent to interact, and to prevent a bug where a power is used when using an interaction to swap states
        yield return new WaitUntil(() => !Brain.Input.Interact);
        if (Interactable == GetInteractable()) {
          Interactable?.Interact(Brain);
          // Short delay to prevent interaction spamming
          Interactable = null;
          yield return new WaitForSeconds(0.5f);
        }
      }
      yield return null;
    }
  }

  public IInteractable GetInteractable() {
    RaycastHit2D hit = Physics2D.Raycast(Brain.transform.position, Brain.Input.Look, InteractRange, InteractMask);
    // Can probably squeeze some more performance out of this
    if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable)) {
      return interactable;
    } else {
      return null;
    }
  }
}