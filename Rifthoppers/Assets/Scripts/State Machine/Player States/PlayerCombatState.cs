using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : State {
  public PlayerBrain Brain;
  private bool IsShooting => Brain.Input.Shoot;
  private Blaster Blaster => Brain.Blaster;
  private Power Power => Brain.Power;
  private Dodge Dodge => Brain.Dodge;

  public PlayerCombatState(PlayerBrain brain) {
    Brain = brain;
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
      float speedMulti = (IsShooting ? 1 : 1.5f);
      Brain.Entity.Direction = Brain.Stats.Speed * speedMulti * Brain.Input.Move.normalized;
      Brain.Entity.Animator.speed = speedMulti;
      Blaster.Sprite.sortingOrder = (Brain.Target.position - Brain.transform.position).y > 0.6f ? -1 : 1;

      if (Brain.Input.IsMouseLook) Brain.Target.position = Brain.Input.Look;
      else Brain.Target.localPosition = Brain.Input.Look;

      yield return null;
    }
  }

  public override void Exit() {
    Power.gameObject.SetActive(false);
    Blaster.gameObject.SetActive(false);
    Dodge.gameObject.SetActive(false);
  }
}