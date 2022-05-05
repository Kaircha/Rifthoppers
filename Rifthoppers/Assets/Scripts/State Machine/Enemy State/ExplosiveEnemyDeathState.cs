using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyDeathState : State{
  public Entity Entity;
  private Collider2D[] Colliders;
  private SpriteRenderer SpriteRenderer;

  public ExplosiveEnemyDeathState(Entity entity) => Entity = entity;

  public override void Enter()
  {
    Colliders = Entity.GetComponentsInChildren<Collider2D>();
    SpriteRenderer = Entity.GetComponentInChildren<SpriteRenderer>();

    foreach (Collider2D collider in Colliders) collider.enabled = false;
    SpriteRenderer.color = Color.red;
    // Apply on-death effects

    Explode();
  }

  private void Explode() 
  {
    LayerMask mask = LayerMask.GetMask("Feet");
    Collider2D[] entities = Physics2D.OverlapCircleAll(Entity.transform.position, 5f, mask);

    foreach(Collider2D entity in entities)
    {
      if (entity.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
      {
        Vector2 direction = entity.transform.position - Entity.transform.position;
        float distance = 10f / (Vector2.Distance(entity.transform.position, Entity.transform.position) + 0.01f);

        rig.AddForce(direction * distance * 30f, ForceMode2D.Impulse);
        if (entity.transform.parent.TryGetComponent<Entity>(out Entity en))
          en.Health.Hurt(Entity, en, 10 * distance, true);
      }
    }
  }

  public override IEnumerator Execute()
  {
    yield return new WaitForSeconds(0.5f);
    (Entity as IPoolable).Release(Entity.gameObject);
  }

  public override void Exit()
  {
    foreach (Collider2D collider in Colliders) collider.enabled = true;
    SpriteRenderer.color = Color.white;
    // Revert on-death effects
  }
}
