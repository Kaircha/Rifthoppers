using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : Entity {
  private StateMachine Machine;
  [HideInInspector] public InputDevice Device;
  [HideInInspector] public InputData Input;
  public PlayerStats Stats;
  public Blaster Blaster;
  public List<Upgrade> Upgrades = new();
  public List<Orbital> Orbitals = new();

  public override void Awake() {
    base.Awake();
    Machine = GetComponent<StateMachine>();
    Stats = GetComponent<PlayerStats>();
    Blaster = GetComponentInChildren<Blaster>(true);
  }

  public override void Update() {
    base.Update();
    Orbiting();
  }

  public void EnterAIState() => Machine.ChangeState(new PlayerAIState(this));
  public void EnterLabState() => Machine.ChangeState(new PlayerLabState(this));
  public void EnterRiftState() => Machine.ChangeState(new PlayerRiftState(this));

  #region Upgrades
  public void AddUpgrade(Upgrade upgrade) {
    //DataManager.Instance.Set($"{ID}TimesObtained", DataManager.Instance.Get<int>($"{ID}TimesObtained") + 1);
    upgrade.Entity = this;
    upgrade.Add();
    upgrade.Coroutine = StartCoroutine(upgrade.UpgradeRoutine());
    Upgrades.Add(upgrade);
  }

  public void RemoveUpgrades() {
    foreach (Upgrade upgrade in Upgrades) {
      upgrade.Remove();
      StopCoroutine(upgrade.Coroutine);
    }
    Upgrades = new();
  }
  #endregion

  #region Orbitals
  public void Orbiting() {
    foreach (Orbital orbital in Orbitals) {
      orbital.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 60f * Time.deltaTime);
      orbital.transform.rotation = Quaternion.identity;
    }
  }

  public void AddOrbital(Orbital orbital) {
    Orbitals.Add(orbital);
    StartCoroutine(OrderOrbitalsRoutine());
  }

  public void RemoveOrbital(Orbital orbital) {
    if (Orbitals.Contains(orbital)) {
      Orbitals.Remove(orbital);
      (orbital as IPoolable).Release(orbital.gameObject);
      StartCoroutine(OrderOrbitalsRoutine());
    }
  }

  public void RemoveOrbitals() {
    foreach (Orbital orbital in Orbitals) {
      (orbital as IPoolable).Release(orbital.gameObject);
    }
    Orbitals = new();
  }

  public IEnumerator OrderOrbitalsRoutine() {
    float timer = 0f;
    List<Vector3> origins = Orbitals.Select(x => x.transform.localPosition).ToList();
    List<Vector3> targets = new();
    for (int i = 0; i < Orbitals.Count; i++) {
      float angle = i * 360f / Orbitals.Count;
      targets.Add(4f * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
    }

    while (timer < 1) {
      for (int i = 0; i < Orbitals.Count; i++) Orbitals[i].transform.localPosition = Vector3.Lerp(origins[i], targets[i], timer);
      timer += Time.deltaTime;
      yield return null;
    }
  }
  #endregion
}