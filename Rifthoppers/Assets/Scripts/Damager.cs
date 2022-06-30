using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour {
  public float Damage = 1f;
  public float Force = 1f;
  public bool UseTime = false;
  public float Delay = 0f;
  public Entity Dealer;
  private List<Entity> Entities = new();

  public void Initialize(float damage, bool useTime, Entity dealer) {
    Damage = damage;
    UseTime = useTime;
    Dealer = dealer;
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody == null) return;
    if (collider.TryGetComponent(out Orbital _)) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      if (!UseTime) {
        entity.Health.Hurt(Dealer, entity, Damage, false);
        entity.Rigidbody.AddForce(Force * (transform.position - entity.transform.position).normalized, ForceMode2D.Impulse);
        if (Delay > 0) Dealer.StartCoroutine(DelayRoutine());
      }
      else Entities.Add(entity); 
    }
  }

  private void OnTriggerExit2D(Collider2D collider) {
    if (collider.attachedRigidbody == null) return;
    if (collider.TryGetComponent(out Orbital _)) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      if (Entities.Contains(entity)) Entities.Remove(entity);
    }
  }

  private void Update() {
    if (UseTime && Entities.Count > 0) Entities.ForEach(entity => entity.Health.Hurt(Dealer, entity, Damage * Time.deltaTime, true));
  }

  public IEnumerator DelayRoutine() {
    gameObject.SetActive(false);
    yield return new WaitForSeconds(Delay);
    gameObject.SetActive(true);
  }
}