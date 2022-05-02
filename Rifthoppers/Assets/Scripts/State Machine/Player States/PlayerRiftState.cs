using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRiftState : State {
  public Entity Entity;
  private bool IsShooting => Entity.Input.Shoot;
  private bool IsMoving => Entity.Direction.magnitude > 0;
  private StatManager Stats => StatManager.Instance;
  private Power Power;
  private Gun Gun;
  private Dodge Dodge;

  public PlayerRiftState(Entity entity) {
    Entity = entity;
    Power = Entity.GetComponentInChildren<Power>(true);
    Gun = Entity.GetComponentInChildren<Gun>(true);
    Dodge = Entity.GetComponentInChildren<Dodge>(true);
  }

  public override void Enter() {
    Power.gameObject.SetActive(true);
    Gun.gameObject.SetActive(true);
    Dodge.gameObject.SetActive(true);

    Machine.StartCoroutine(ShootRoutine());
    Machine.StartCoroutine(Dodge.DodgeRoutine());
    Machine.StartCoroutine(Power.PowerRoutine());
  }

  public override IEnumerator Execute() {
    while (true) {
      Entity.Direction = (IsShooting ? 1 : 1.5f) * Entity.Input.Move.normalized;
      Gun.Sprite.sortingOrder = (Entity.Target.position - Entity.transform.position).y > 0.6f ? -1 : 1;

      if (Entity.Input.IsMouseLook) Entity.Target.position = Entity.Input.Look;
      else Entity.Target.localPosition = Entity.Input.Look;

      yield return null;
    }
  }

  public override void Exit() {
    Power.gameObject.SetActive(false);
    Gun.gameObject.SetActive(false);
    Dodge.gameObject.SetActive(false);
  }

  public IEnumerator ShootRoutine() {
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      Entity.HasAttacked();
      Gun.Shoot();
      float firerate = (IsMoving ? 1 : 1.5f) * Mathf.Max(0.01f, Stats.Get(StatType.PlayerFirerate));
      yield return new WaitForSeconds(1 / firerate);
    }
  }
}