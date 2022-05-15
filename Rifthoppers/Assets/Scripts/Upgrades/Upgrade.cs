using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject {
  public Sprite Sprite;
  public string Quote;
  public List<Modifier> Modifiers;
  public int Weight;

  [HideInInspector] public Entity Entity;

  // public bool IsUnlocked => DataManager.Instance.Get<bool>($"{ID}IsUnlocked");
  // public int TimesObtained => DataManager.Instance.Get<int>($"{ID}TimesObtained");
  // Some sort of Unlock Condition

  // public void Unlock() => DataManager.Instance.Set($"{ID}IsUnlocked", true);

  public void Add(Entity entity) {
    //DataManager.Instance.Set($"{ID}TimesObtained", DataManager.Instance.Get<int>($"{ID}TimesObtained") + 1);
    Entity = entity;
    OnAdd();
    entity.Upgrades.Add(this);
  }
  public void Remove(Entity entity) {
    // Upgrades are currently being removed on Exiting the RiftDeadState; Will not work with a quick-reset!
    Entity = entity;
    OnRemove();
  }

  public abstract void OnAdd();
  public abstract void OnRemove();
}