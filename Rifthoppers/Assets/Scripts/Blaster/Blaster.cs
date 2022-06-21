using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public PlayerBrain Brain;
  public Barrel Barrel;
  public SpriteRenderer Sprite;

  private Weapon _weapon;
  public Weapon Weapon {
    get => _weapon;
    set {
      OnShootingStarted -= _weapon.ShootingStarted;
      OnShootingUpdated -= _weapon.ShootingUpdated;
      OnShootingStopped -= _weapon.ShootingStopped;
      _weapon = value;
      OnShootingStarted += _weapon.ShootingStarted;
      OnShootingUpdated += _weapon.ShootingUpdated;
      OnShootingStopped += _weapon.ShootingStopped;
    }
  }
  public bool IsShooting => Brain.Input.Shoot;
  public bool IsMoving => Brain.Entity.Direction.magnitude > 0;
  private bool CanShoot = true;

  public event Action<PlayerBrain, Barrel> OnShootingStarted;
  public event Action<PlayerBrain, Barrel> OnShootingUpdated;
  public event Action<PlayerBrain, Barrel> OnShootingStopped;

  public void ShootingStarted(PlayerBrain brain, Barrel barrel) => OnShootingStarted?.Invoke(brain, barrel);
  public void ShootingUpdated(PlayerBrain brain, Barrel barrel) => OnShootingUpdated?.Invoke(brain, barrel);
  public void ShootingStopped(PlayerBrain brain, Barrel barrel) => OnShootingStopped?.Invoke(brain, barrel);


  private void OnEnable() {
    Weapon = GetWeapon(Brain.Stats.WeaponType);
  }
  private void OnDisable() {
    StopAllCoroutines();
    if (IsShooting) ShootingStopped(Brain, Barrel);
  }

  public IEnumerator BlasterRoutine() {
    CanShoot = true;
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      ShootingStarted(Brain, Barrel);
      while (IsShooting) {
        if (CanShoot) {
          ShootingUpdated(Brain, Barrel);
          StartCoroutine(FirerateRoutine());
        }
        yield return null;
      }
      ShootingStopped(Brain, Barrel);
    }
  }

  public IEnumerator FirerateRoutine() {
    float firerate = (IsMoving ? 1 : 1.5f) * Brain.Stats.Firerate;
    CanShoot = false;
    yield return new WaitForSeconds(1 / firerate);
    CanShoot = true;
  }

  public Weapon GetWeapon(WeaponType type) => UpgradeWeaponManager.Instance.Weapons[(int)type];
}