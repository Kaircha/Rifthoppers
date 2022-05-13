using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public Entity Entity;
  public Transform BulletOrigin;
  public SpriteRenderer Sprite;
  public AudioSource ShootSFX;
  public Rigidbody2D Rigidbody;

  public List<Weapon> Weapons = new();

  public bool IsShooting => Entity.Input.Shoot;
  public bool IsMoving => Entity.Direction.magnitude > 0;

  public event Action OnShoot;
  public event Action OnShootingStarted;
  public event Action OnShootingStopped;

  public void Shoot() => OnShoot?.Invoke();
  public void ShootingStarted() => OnShootingStarted?.Invoke();
  public void ShootingStopped() => OnShootingStopped?.Invoke();

  private void OnEnable() {
    if (Weapons.Count == 0) {
      AddWeapon(new DefaultWeapon(Entity, BulletOrigin));
    }
  }
  private void OnDisable() => StopAllCoroutines();

  public IEnumerator BlasterRoutine() {
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      ShootingStarted();
      StartCoroutine(ShootRoutine());
      yield return new WaitUntil(() => !IsShooting);
      ShootingStopped();
      StopCoroutine(ShootRoutine());
    }
  }

  public IEnumerator ShootRoutine() {
    while (true) {
      Shoot();
      float firerate = (IsMoving ? 1 : 1.5f) * Entity.Stats.PlayerFirerate;
      yield return new WaitForSeconds(1 / firerate);
    }
  }

  public void AddWeapon(Weapon weapon) {
    Weapons.Add(weapon);
    OnShoot += weapon.Shoot;
    OnShootingStarted += weapon.ShootingStarted;
    OnShootingStopped += weapon.ShootingStopped;
  }

  public void RemoveWeapon(Weapon weapon) {
    if (Weapons.Remove(weapon)) {
      OnShoot -= weapon.Shoot;
      OnShootingStarted -= weapon.ShootingStarted;
      OnShootingStopped -= weapon.ShootingStopped;
    }
  }
}

//Rigidbody.AddForce(-10f * Stats.ProjectileSizeMulti * BulletOrigin.right, ForceMode2D.Impulse);
//ImpulseSource.GenerateImpulse(0.15f * Stats.ProjectileSizeMulti * BulletOrigin.right);
//ShootSFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
//ShootSFX.Play();