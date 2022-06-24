using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State {
  public EnemyBrain Brain;
  private Collider2D[] Colliders;
  private SpriteRenderer SpriteRenderer;

  public EnemyDeathState(EnemyBrain brain) => Brain = brain;

  public override void Enter() {
    Colliders = Brain.GetComponentsInChildren<Collider2D>();
    SpriteRenderer = Brain.GetComponentInChildren<SpriteRenderer>();

    foreach (Collider2D collider in Colliders) collider.enabled = false;
    SpriteRenderer.color = Color.black;
    // Apply on-death effects
  }

  public override IEnumerator Execute() {
    yield return new WaitForSeconds(0.5f);
    (Brain as IPoolable).Release(Brain.gameObject);
  }

  public override void Exit() {
    foreach (Collider2D collider in Colliders) collider.enabled = true;
    SpriteRenderer.color = Color.white;
    // Revert on-death effects
  }
}