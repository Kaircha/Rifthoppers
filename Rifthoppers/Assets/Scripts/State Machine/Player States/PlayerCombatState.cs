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
    Brain.Entity.Health.OnDamageTaken += OnDamageTaken;

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
      Brain.Entity.Direction = Brain.PlayerStats.Speed * speedMulti * Brain.Input.Move.normalized;
      Brain.Entity.Animator.speed = speedMulti;
      Blaster.Sprite.sortingOrder = (Brain.Target.position - Brain.transform.position).y > 0.6f ? -1 : 1;

      if (Brain.Input.IsMouseLook) Brain.Target.position = Brain.Input.Look;
      else Brain.Target.localPosition = Brain.Input.Look;

      yield return null;
    }
  }

  public override void Exit() {
    Brain.Entity.Health.OnDamageTaken -= OnDamageTaken;

    Power.gameObject.SetActive(false);
    Blaster.gameObject.SetActive(false);
    Dodge.gameObject.SetActive(false);
  }

  public void OnDamageTaken(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (isDoT) return;

    Brain.Impulse.GenerateImpulse(1f);
    Brain.StartCoroutine(Brain.Immunity.ImmunityRoutine(0.4f));
    Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(Brain.transform.position, 5f, ~LayerMask.NameToLayer("Enemy"));
    foreach (Collider2D enemy in nearbyEnemies) {
      if (enemy.attachedRigidbody == null) return;
      if (enemy.TryGetComponent(out Ammo _)) return;
      Vector3 direction = (enemy.transform.position - Brain.transform.position).normalized;
      enemy.attachedRigidbody.AddForce(10f * enemy.attachedRigidbody.mass * direction, ForceMode2D.Impulse);
    }
  }
}