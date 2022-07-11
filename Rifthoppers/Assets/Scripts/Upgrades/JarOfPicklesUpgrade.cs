using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jar of Pickles", menuName = "Upgrades/Jar of Pickles")]
public class JarOfPicklesUpgrade : UpgradeObject {
  public GameObject Prefab;
  public override Upgrade Upgrade() => new JarOfPickles(this, Prefab);

  public class JarOfPickles : Upgrade {
    public Orbital Orbital;
    public float Damage = 10;

    private readonly GameObject Prefab;

    public JarOfPickles(UpgradeObject obj, GameObject prefab) {
      Object = obj;
      Prefab = prefab;
    }

    public override void Add() {
      Orbital = GameObject.Instantiate(Prefab, Brain.Orbitals.transform).GetComponent<Orbital>();
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
}