using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser Weapon", menuName = "Weapons/Laser")]
public class LaserWeapon : Weapon {
  public ContactFilter2D ContactFilter;

  public override void ShootingStarted(Entity entity, AmmoStats ammo, Barrel barrel) {
    barrel.Laser.gameObject.SetActive(true);
    barrel.Laser.LaserRoutine = barrel.StartCoroutine(LaserRoutine(entity, ammo, barrel));
    barrel.Laser.StartVFX.Play();
    barrel.Laser.EndVFX.Play();
    barrel.Laser.EdgeCollider.points = new Vector2[2];
    barrel.Laser.LineRenderer.widthMultiplier = ammo.AmmoSize;

    barrel.transform.parent.GetComponent<LookAt>().Speed = 10f;
  }

  public override void ShootingUpdated(Entity entity, AmmoStats ammo, Barrel barrel) {
    //Barrel.Rigidbody.AddForce(-20f * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right, ForceMode2D.Impulse);
    //Barrel.ImpulseSource.GenerateImpulse(0.15f * Barrel.Origin.right);

    Collider2D[] results = new Collider2D[100];
    int numResults = barrel.Laser.EdgeCollider.OverlapCollider(ContactFilter, results);
    if (numResults > 0) {
      for (int i = 0; i < numResults; i++) {
        if (results[i].attachedRigidbody != null && results[i].attachedRigidbody.TryGetComponent(out Entity target)) {
          target.Health.Hurt(entity, target, ammo.AmmoDamage, false);
        }
      }
    }
  }

  public override void ShootingStopped(Entity entity, AmmoStats ammo, Barrel barrel) {
    barrel.Laser.gameObject.SetActive(false);
    barrel.StopCoroutine(barrel.Laser.LaserRoutine);
    barrel.Laser.StartVFX.Stop();
    barrel.Laser.EndVFX.Stop();

    barrel.transform.parent.GetComponent<LookAt>().Speed = 20f;
  }

  public IEnumerator LaserRoutine(Entity entity, AmmoStats ammo, Barrel barrel) {
    Vector2[] points = new Vector2[2];
    points[0] = Vector2.zero;

    while (true) {
      barrel.Rigidbody.AddForce(-20f * ammo.AmmoSize * barrel.Origin.right, ForceMode2D.Force);
      barrel.ImpulseSource.GenerateImpulse(0.1f * barrel.Origin.right);

      barrel.Laser.LineRenderer.SetPosition(0, barrel.Origin.localPosition);
      barrel.Laser.StartVFX.transform.localPosition = barrel.Origin.localPosition;

      barrel.Laser.LineRenderer.SetPosition(1, 40f * Vector3.right);
      barrel.Laser.EndVFX.transform.localPosition = 40f * Vector3.right;
      points[1] = 40f * Vector3.right;

      barrel.Laser.EdgeCollider.points = points;
      yield return null;
    }
  }
}