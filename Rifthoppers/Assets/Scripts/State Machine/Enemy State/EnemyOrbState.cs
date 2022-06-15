using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbState : State {
  public EnemyBrain Brain;

  public EnemyOrbState(EnemyBrain brain) => Brain = brain;

  public override void Enter() {
    Brain.Target = Object.FindObjectOfType<EnergyOrb>().transform;
  }

  public override IEnumerator Execute() {
    float angle = 0;

    while (true) {
      angle = (angle + 3f * Time.deltaTime) % (2 * Mathf.PI);
      Vector2 direction = (Brain.Target.position - Brain.transform.position + 2 * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle))).normalized;
      Brain.Entity.Direction = Brain.Stats.Speed * direction;

      // Siphon energy? Grow bigger?

      yield return null;
    }
  }
}