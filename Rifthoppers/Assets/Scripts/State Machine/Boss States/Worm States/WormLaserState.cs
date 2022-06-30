using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WormLaserState : State {
  public WormBrain Brain;
  public float Direction = 1f;
  public float Duration = 1f;
  public const float Radius = 19f;
  public SimpleBlaster BlasterA;
  public SimpleBlaster BlasterB;
  public Barrel LaserA;
  public Barrel LaserB;

  public WormLaserState(WormBrain brain, float direction, float duration) {
    Brain = brain;
    Direction = direction;
    Duration = Mathf.Max(0.01f, duration); // Prevent dividing by <= 0
  }

  public override void Enter() {
    BlasterA = Brain.Segments[1].GetComponent<SimpleBlaster>();
    BlasterB = Brain.Segments[25].GetComponent<SimpleBlaster>();
    if (Direction < 0) {
      LaserA = BlasterA.Barrels[0];
      LaserB = BlasterB.Barrels[0];
    } else {
      LaserA = BlasterA.Barrels[1];
      LaserB = BlasterB.Barrels[1];
    }
  }

  public override IEnumerator Execute() {
    if (Brain.Radius != Radius) yield return Machine.StartCoroutine(EnterRoutine());
    Machine.StartCoroutine(BlasterRoutine());

    BlasterA.CanShoot = false;
    BlasterB.CanShoot = false;
    
    float timer = 0f;
    while (timer < 0.7f * Duration) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = Radius * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      Brain.UpdateLaser(LaserA.Origin.position, LaserB.Origin.position);
      timer += Time.deltaTime;
      yield return null;
    }

    Brain.LaserRenderer.enabled = true;
    Brain.LaserRenderer.widthMultiplier = 0.25f;

    while (timer < Duration) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = Radius * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      Brain.UpdateLaser(LaserA.Origin.position, LaserB.Origin.position);
      timer += Time.deltaTime;
      yield return null;
    }

    Brain.LaserCollider.enabled = true;
    Brain.LaserRenderer.widthMultiplier = 1f;

    while (true) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = Radius * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      Brain.UpdateLaser(LaserA.Origin.position, LaserB.Origin.position);
      yield return null;
    }
  }

  public override void Exit() {
    Brain.Radius = Radius;
    Brain.LaserRenderer.enabled = false;
    Brain.LaserCollider.enabled = false;
    BlasterA.CanShoot = true;
    BlasterB.CanShoot = true;

    foreach (WormSegment segment in Brain.Segments) {
      segment.Energy = 0f;
      if (segment.Blaster) {
        foreach (Barrel barrel in segment.Blaster.Barrels) {
          segment.Blaster.ShootingStopped(Brain.Entity, Brain.Ammo, barrel);
        }
      }
    }
  }

  public IEnumerator EnterRoutine() {
    float timer = 0f;
    while (timer < 1) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = Mathf.Lerp(Brain.Radius, Radius, timer) * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      timer += Time.deltaTime;
      yield return null;
    }
  }

  public IEnumerator BlasterRoutine() {
    while (true) {
      Machine.StartCoroutine(BlasterWave());
      yield return new WaitForSeconds(6f); // Shoot delay
    }
  }

  public IEnumerator BlasterWave() {
    foreach (WormSegment segment in Brain.Segments) {
      if (segment.Blaster && segment.Blaster.CanShoot) {
        if (Direction < 0) segment.Blaster.ShootingStarted(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[0]);
        else segment.Blaster.ShootingStarted(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[1]);
      }

      float timer = 0f;
      while (timer < 0.5f) {
        segment.Energy = Mathf.PingPong(timer, 1f);
        timer += 4f * Time.deltaTime;
        yield return null;
      }

      if (segment.Blaster && segment.Blaster.CanShoot) {
        if (Direction < 0) segment.Blaster.ShootingUpdated(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[0]);
        else segment.Blaster.ShootingUpdated(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[1]);
      }

      while (timer < 1f) {
        segment.Energy = Mathf.PingPong(timer, 1f);
        timer += 4f * Time.deltaTime;
        yield return null;
      }
      segment.Energy = 0f;

      if (segment.Blaster && segment.Blaster.CanShoot) {
        if (Direction < 0) segment.Blaster.ShootingStopped(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[0]);
        else segment.Blaster.ShootingStopped(Brain.Entity, Brain.Ammo, segment.Blaster.Barrels[1]);
      }
    }
  }
}