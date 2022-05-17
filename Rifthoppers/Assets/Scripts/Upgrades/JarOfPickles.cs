using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jar of Pickles", menuName = "Upgrades/Jar of Pickles")]

public class JarOfPickles : Upgrade
{

  public GameObject OrbitalPrefab;
  public Sprite Pickle;

  public float damage = 10;

  private Orbital Orbital; 

  public override void OnAdd()
  {
    Orbital = Instantiate(OrbitalPrefab, Entity.transform).GetComponent<Orbital>();
    Orbital.Reinitialize(300, 0.35f);
    Orbital.GetComponent<SpriteRenderer>().sprite = Pickle;
    Orbital.onOrbitalCollide += OnCollide;
  }
  public override void OnRemove() => Destroy(Orbital.gameObject);
  public void OnCollide(Entity enemy) => enemy.Health.Hurt(Entity, enemy, damage, false); 
}
