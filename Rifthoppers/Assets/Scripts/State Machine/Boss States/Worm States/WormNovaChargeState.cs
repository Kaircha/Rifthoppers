using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormNovaChargeState : State {
  public WormBrain Brain;
  public const float Radius = 25f;
  public int Amount;
  public override float ExecutionTime => 6f;

  public WormNovaChargeState(WormBrain brain, int amount) {
    Brain = brain;
    Amount = amount;
  }

  public override void Enter() {
    Brain.Segments[0].Blaster.CanShoot = true;
    // Activate Head Damage?
  }

  public override IEnumerator Execute() {
    Vector3 A = Brain.Target.position;
    float opposite = (Brain.Angle + Mathf.PI) % (2f * Mathf.PI);
    Vector3 B = 25f * new Vector2(Mathf.Sin(opposite), Mathf.Cos(opposite));
    Brain.Angle = opposite;

    float timer = 0f;
    while (timer < 0.15f) {
      Brain.Target.position = Vector3.Lerp(A, B, timer);
      timer += Time.deltaTime / 2f;
      yield return null;
    }

    yield return Machine.StartCoroutine(NovaBulletRoutine());

    while (timer < 1f) {
      Brain.Target.position = Vector3.Lerp(A, B, timer);
      timer += Time.deltaTime / 2f;
      yield return null;
    }
  }

  public override void Exit() {
    Brain.Segments[0].Blaster.CanShoot = false;
    // Deactivate Head Damage?
    Brain.Radius = Radius;
  }

  public IEnumerator NovaBulletRoutine() {
    Brain.Segments[0].Blaster.ShootingStarted(Brain.Entity, Brain.Ammo, Brain.Segments[0].Blaster.Barrels[0]);

    for (int i = 0; i < 10; i++) {
      Brain.Segments[0].Blaster.ShootingUpdated(Brain.Entity, Brain.Ammo, Brain.Segments[0].Blaster.Barrels[0]);
      yield return new WaitForSeconds(0.25f);
    }

    Brain.Segments[0].Blaster.ShootingStopped(Brain.Entity, Brain.Ammo, Brain.Segments[0].Blaster.Barrels[0]);
    yield return new WaitForSeconds(1.5f);
  }
}