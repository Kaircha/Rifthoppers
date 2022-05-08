using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiftState : State {
  public Entity Entity;
  private bool IsShooting => Entity.Input.Shoot;
  private bool IsMoving => Entity.Direction.magnitude > 0;
  private Stats Stats => Entity.Stats;
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

    Machine.StartCoroutine(ShootRoutine());
    Machine.StartCoroutine(UpdateBlaster());
    Machine.StartCoroutine(Dodge.DodgeRoutine());
    Machine.StartCoroutine(Power.PowerRoutine());
  }

  public override IEnumerator Execute() {
    while (true) {
      Entity.Direction = (IsShooting ? 1 : 1.5f) * Entity.Input.Move.normalized;
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

  public IEnumerator UpdateBlaster(){

    while(true){
      yield return new WaitUntil(() => IsShooting);
      yield return new WaitUntil(() => !IsShooting);
      Blaster.StopShooting();
    }
  }

  public IEnumerator ShootRoutine() {
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      Entity.HasAttacked();
      Blaster.Shoot();
      float firerate = (IsMoving ? 1 : 1.5f) * Stats.PlayerFirerate;
      yield return new WaitForSeconds(1 / firerate);
    }
  }
}