using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtFX : MonoBehaviour {
  public PlayerHealth Health;
  public SpriteRenderer Renderer;
  public Material DefaultMaterial;
  public Material HurtMaterial;

  private void OnEnable() => Health.OnDamageTaken += OnHurt;
  private void OnDisable() => Health.OnDamageTaken -= OnHurt;

  private void OnHurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (isDoT) return;
    StopAllCoroutines();
    StartCoroutine(HurtRoutine());
  }

  private IEnumerator HurtRoutine() {
    Renderer.material = HurtMaterial;
    yield return new WaitForSeconds(0.2f);
    Renderer.material = DefaultMaterial;
  }
}