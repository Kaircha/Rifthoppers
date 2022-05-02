using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubState : State {
  public Entity Entity;
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

  public PlayerHubState(Entity entity) {
    Entity = entity;
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
      Entity.Direction = 1.5f * Entity.Input.Move.normalized;

      if (Entity.Input.IsMouseLook) Entity.Target.position = Entity.Input.Look;
      else Entity.Target.localPosition = Entity.Input.Look;

      yield return null;
    }
  }

  public override void Exit() { }

  public IEnumerator InteractionRoutine() {
    while (true) {
      Interactable = GetInteractable();
      if (Entity.Input.Interact) {
        // Interaction occurs upon releasing the interact button
        // Both to really confirm the intent to interact, and to prevent a bug where a power is used when using an interaction to swap states
        yield return new WaitUntil(() => !Entity.Input.Interact);
        if (Interactable == GetInteractable()) {
          Interactable?.Interact(Entity as PlayerEntity);
          // Short delay to prevent interaction spamming
          Interactable = null;
          yield return new WaitForSeconds(0.5f);
        }
      }
      yield return null;
    }
  }

  public IInteractable GetInteractable() {
    // Does the raycast aim at the mouse, or in the last direction the player moved?
    Vector2 direction = (Entity.Target.position - Entity.transform.position).normalized;
    RaycastHit2D hit = Physics2D.Raycast(Entity.transform.position, direction, InteractRange, InteractMask);
    // Can probably squeeze some more performance out of this
    if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable)) {
      return interactable;
    } else {
      return null;
    }
  }
}