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
    Orbital.transform.SetParent(Brain.Orbitals.transform);
    Orbital.Initialize(0.3f, Pickle);
    Orbital.OnOrbitalCollide += OnCollide;
    Brain.Orbitals.Add(Orbital);
  }
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() {
    Orbital.OnOrbitalCollide -= OnCollide;
    Brain.Orbitals.Remove(Orbital);
  }
  public void OnCollide(Entity enemy) => enemy.Health.Hurt(Brain.Entity, enemy, Damage, false); 
}