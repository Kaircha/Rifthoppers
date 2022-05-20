using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiftState : State {
  public Entity Entity;
  private bool IsShooting => Entity.Input.Shoot;
  private Blaster Blaster;
  private Power Power;
  private Dodge Dodge;

  public PlayerRiftState(Entity entity) {
    Entity = entity;
    Blaster = Entity.GetComponentInChildren<Blaster>(true);
    Power = Entity.GetComponentInChildren<Power>(true);
    Dodge = Entity.GetComponentInChildren<Dodge>(true);
  }

  public override void Enter() {
    Blaster.gameObject.SetActive(true);
    Power.gameObject.SetActive(true);
    Dodge.gameObject.SetActive(true);

    // These could technically also be started OnEnable on their own object
    Machine.StartCoroutine(Blaster.BlasterRoutine());
    Machine.StartCoroutine(Dodge.DodgeRoutine());
    Machine.StartCoroutine(Power.PowerRoutine());
  }

  public override IEnumerator Execute() {
    while (true) {
      Entity.Direction = (IsShooting ? 1 : 1.5f) * Entity.Input.Move.normalized;
      Entity.Animator.speed = (IsShooting ? 1 : 1.5f);
      Blaster.Sprite.sortingOrder = (Entity.Target.position - Entity.transform.position).y > 0.6f ? -1 : 1;

      if (Entity.Input.IsMouseLook) Entity.Target.position = Entity.Input.Look;
      else Entity.Target.localPosition = Entity.Input.Look;

      yield return null;
    }
  }

  public override void Exit() {
    Power.gameObject.SetActive(false);
    Blaster.gameObject.SetActive(false);
    Dodge.gameObject.SetActive(false);
  }
}