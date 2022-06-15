using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyDeathState : State{
  public Entity Entity;
  private Collider2D[] Colliders;
  private SpriteRenderer SpriteRenderer;

  public ExplosiveEnemyDeathState(Entity entity) => Entity = entity;

  public override void Enter() {
    Colliders = Entity.GetComponentsInChildren<Collider2D>();
    SpriteRenderer = Entity.GetComponentInChildren<SpriteRenderer>();

    foreach (Collider2D collider in Colliders) collider.enabled = false;
    SpriteRenderer.color = Color.red;
    // Apply on-death effects

    Explode();
  }

  public override IEnumerator Execute() {
    yield return new WaitForSeconds(0.5f);
    (Entity as IPoolable).Release(Entity.gameObject);
  }

  public override void Exit() {
    foreach (Collider2D collider in Colliders) collider.enabled = true;
    SpriteRenderer.color = Color.white;
    // Revert on-death effects
  }

  private void Explode()  {
    LayerMask mask = LayerMask.GetMask("Feet");
    Collider2D[] colliders = Physics2D.OverlapCircleAll(Entity.transform.position, 5f, mask);

    foreach (Collider2D collider in colliders) {
      if (collider.transform.IsChildOf(Entity.transform)) continue;
      if (collider.attachedRigidbody == null) continue;

      Vector2 direction = collider.transform.position - Entity.transform.position;
      float distance = 10f / (Vector2.Distance(collider.transform.position, Entity.transform.position) + 0.01f);

      collider.attachedRigidbody.AddForce(30f * distance * direction, ForceMode2D.Impulse);
      if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
        entity.Health.Hurt(Entity, entity, 10 * distance, false);
      }
    }
  }
}