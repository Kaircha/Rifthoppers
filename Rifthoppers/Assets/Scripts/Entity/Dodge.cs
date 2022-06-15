using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour {
  public PlayerBrain Brain;
  public SpriteRenderer Renderer;
  public ParticleSystem DodgeSystem;
  public AudioSource DodgeSource;

  public void Use(bool isFlipped) {
    var main = DodgeSystem.main;
    main.startSize = new ParticleSystem.MinMaxCurve(Renderer.sprite.rect.width / Renderer.sprite.pixelsPerUnit);
    DodgeSystem.textureSheetAnimation.SetSprite(0, Renderer.sprite);
    DodgeSystem.GetComponent<ParticleSystemRenderer>().flip = new Vector3(isFlipped ? 1 : 0, 0);

    DodgeSystem.Play();
    DodgeSource.Play();
  }

  public IEnumerator DodgeRoutine() {
    while (true) {
      yield return new WaitUntil(() => Brain.Input.Dodge && Brain.Input.Move != Vector2.zero);
      Use(Brain.transform.position.x > Brain.Target.position.x);
      Brain.Input.Dodge = false;
      Brain.Entity.Rigidbody.AddForce(150f * Brain.Input.Move, ForceMode2D.Impulse);
      RiftManager.Instance.Energy.Hurt(Brain.Entity, null, 5f * RiftManager.Instance.Energy.StaticPercentage * RiftManager.Instance.Energy.StaticPercentage, false);

      StartCoroutine(Brain.Immunity.ImmunityRoutine(0.2f));
      yield return new WaitForSeconds(0.4f);
    }
  }
}