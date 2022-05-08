using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbState : State {
  public Entity Entity;

  public EnemyOrbState(Entity entity) => Entity = entity;

  public override void Enter() {
    Entity.Target = Object.FindObjectOfType<Checkpoint>().transform;
  }

  public override IEnumerator Execute() {
    float angle = 0;

    while (true) {
      Entity.Direction = (Entity.Target.position - Entity.transform.position + 2 * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle))).normalized;
      angle = (angle + 3f * Time.deltaTime) % (2 * Mathf.PI);

      // Siphon energy? Grow bigger?

      yield return null;
    }
  }
}