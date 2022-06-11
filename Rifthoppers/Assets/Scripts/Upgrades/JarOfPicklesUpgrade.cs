using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jar of Pickles", menuName = "Upgrades/Jar of Pickles")]

public class JarOfPicklesUpgrade : Upgrade {

  public GameObject OrbitalPrefab;
  public Sprite Pickle;

  public float Damage = 10;

  private Orbital Orbital; 

  public override void Add() {
    Orbital = PoolManager.Instance.Orbitals.Objects.Get().GetComponent<Orbital>();
    Orbital.transform.SetParent(Entity.transform);
    Orbital.Initialize(0.3f, Pickle);
    Orbital.OnOrbitalCollide += OnCollide;
    Entity.AddOrbital(Orbital);
  }
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() {
    Orbital.OnOrbitalCollide -= OnCollide;
    Entity.RemoveOrbital(Orbital);
  }
  public void OnCollide(Entity enemy) => enemy.Health.Hurt(Entity, enemy, Damage, false); 
}