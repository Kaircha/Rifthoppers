using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State {
  public Entity Entity;
  private Collider2D[] Colliders;
  private SpriteRenderer SpriteRenderer;

  public EnemyDeathState(Entity entity) => Entity = entity;

  public override void Enter() {
    Colliders = Entity.GetComponentsInChildren<Collider2D>();
    SpriteRenderer = Entity.GetComponentInChildren<SpriteRenderer>();

    foreach (Collider2D collider in Colliders) collider.enabled = false;
    SpriteRenderer.color = Color.black;
    // Apply on-death effects
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
}