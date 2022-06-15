using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour {
  public float Damage = 1f;
  public bool UseTime = false;
  public Entity Dealer;
  private List<Entity> Entities = new();

  public void Initialize(float damage, bool useTime, Entity dealer) {
    Damage = damage;
    UseTime = useTime;
    Dealer = dealer;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.attachedRigidbody == null) return;
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      if (!UseTime) entity.Health.Hurt(Dealer, entity, Damage, false);
      else Entities.Add(entity); 
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.attachedRigidbody == null) return;
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      if (Entities.Contains(entity)) Entities.Remove(entity);
    }
  }

  private void Update() {
    if (UseTime && Entities.Count > 0) Entities.ForEach(entity => entity.Health.Hurt(Dealer, entity, Damage * Time.deltaTime, true));
  }
}