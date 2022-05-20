using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser Weapon", menuName = "Weapons/Laser")]
public class LaserWeapon : Weapon {
  public ContactFilter2D ContactFilter;

  public override void ShootingStarted() {
    Barrel.Laser.gameObject.SetActive(true);
    Barrel.Laser.LaserRoutine = Barrel.StartCoroutine(LaserRoutine());
    Barrel.Laser.StartVFX.Play();
    Barrel.Laser.EndVFX.Play();
    Barrel.Laser.EdgeCollider.points = new Vector2[2];
    Barrel.Laser.LineRenderer.widthMultiplier = Entity.Stats.ProjectileSizeMulti;

    Barrel.transform.parent.GetComponent<LookAt>().Speed = 10f;
  }

  public override void ShootingUpdated() {
    //Barrel.Rigidbody.AddForce(-20f * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right, ForceMode2D.Impulse);
    //Barrel.ImpulseSource.GenerateImpulse(0.15f * Barrel.Origin.right);

    Collider2D[] results = new Collider2D[100];
    int numResults = Barrel.Laser.EdgeCollider.OverlapCollider(ContactFilter, results);
    if (numResults > 0) {
      for (int i = 0; i < numResults; i++) {
        if (results[i].attachedRigidbody != null && results[i].attachedRigidbody.TryGetComponent(out Entity entity)) {
          entity.Health.Hurt(Entity, entity, Entity.Stats.ProjectileDamage, false);
        }
      }
    }
  }

  public override void ShootingStopped() {
    Barrel.Laser.gameObject.SetActive(false);
    Barrel.StopCoroutine(Barrel.Laser.LaserRoutine);
    Barrel.Laser.StartVFX.Stop();
    Barrel.Laser.EndVFX.Stop();

    Barrel.transform.parent.GetComponent<LookAt>().Speed = 20f;
  }

  public IEnumerator LaserRoutine() {
    Vector2[] points = new Vector2[2];
    points[0] = Vector2.zero;

    while (true) {
      Barrel.Rigidbody.AddForce(-20f * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right, ForceMode2D.Force);
      Barrel.ImpulseSource.GenerateImpulse(0.1f * Barrel.Origin.right);

      Barrel.Laser.LineRenderer.SetPosition(0, Barrel.Origin.localPosition);
      Barrel.Laser.StartVFX.transform.localPosition = Barrel.Origin.localPosition;

      Barrel.Laser.LineRenderer.SetPosition(1, 40f * Vector3.right);
      Barrel.Laser.EndVFX.transform.localPosition = 40f * Vector3.right;
      points[1] = 40f * Vector3.right;

      //RaycastHit2D hit = Physics2D.Raycast(Barrel.Origin.position, Barrel.Origin.right, 40f, ContactFilter.layerMask);
      //if (hit.collider != null) {
      //  Barrel.Laser.LineRenderer.SetPosition(1, hit.point);
      //  Barrel.Laser.EndVFX.transform.position = hit.point;
      //  points[1] = Barrel.Origin.InverseTransformPoint(hit.point);
      //} else {
      //  Barrel.Laser.LineRenderer.SetPosition(1, 40f * Barrel.Origin.right);
      //  Barrel.Laser.EndVFX.transform.position = 40f * Barrel.Origin.right;
      //  points[1] = Barrel.Origin.InverseTransformPoint(40f * Barrel.Origin.right);
      //}

      Barrel.Laser.EdgeCollider.points = points;
      yield return null;
    }
  }
}