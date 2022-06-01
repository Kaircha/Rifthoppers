using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {
  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;
  public bool IsDead { get; set; }
  public float Percentage { get; }

  public void Revive();
  public void Heal();
  public void Heal(float amount);
  public float Hurt(Entity dealer, Entity target, float amount, bool isDoT);
  public float Hurt(Entity dealer, Entity target, float amount, bool isDoT, float knockback, Vector3 pos);
  public void Kill();
}