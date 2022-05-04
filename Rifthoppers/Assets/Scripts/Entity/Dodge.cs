using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour {
  public Entity Entity;
  public SpriteRenderer Renderer;
  public ParticleSystem DodgeSystem;
  public AudioSource DodgeSource;
  private Collider2D[] Colliders;

  public void Use(bool isFlipped) {
    var main = DodgeSystem.main;
    main.startSize = new ParticleSystem.MinMaxCurve(Renderer.sprite.rect.width / Renderer.sprite.pixelsPerUnit);
    DodgeSystem.textureSheetAnimation.SetSprite(0, Renderer.sprite);
    DodgeSystem.GetComponent<ParticleSystemRenderer>().flip = new Vector3(isFlipped ? 1 : 0, 0);

    DodgeSystem.Play();
    DodgeSource.Play();
  }

  public IEnumerator DodgeRoutine() {
    Colliders = Entity.GetComponentsInChildren<Collider2D>();
    while (true) {
      yield return new WaitUntil(() => Entity.Input.Dodge && Entity.Input.Move != Vector2.zero);
      Use(Entity.transform.position.x > Entity.Target.position.x);
      Entity.Input.Dodge = false;
      Entity.Rigidbody.AddForce(150f * Entity.Input.Move, ForceMode2D.Impulse);
      RiftManager.Instance.Energy.Hurt(Entity, null, 5f * RiftManager.Instance.EnergyMultiplier, false);

      // Instead use a new iFrame system
      foreach (Collider2D collider in Colliders) collider.enabled = false;
      yield return new WaitForSeconds(0.2f);
      foreach (Collider2D collider in Colliders) collider.enabled = true;
      yield return new WaitForSeconds(0.2f);
    }
  }
}