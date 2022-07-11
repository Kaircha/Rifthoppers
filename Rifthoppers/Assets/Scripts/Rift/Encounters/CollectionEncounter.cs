using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionEncounter : Encounter {
  public override bool IsFinished => Progress >= 1f;
  public override float Progress => Mathf.Clamp01(Amount / Required);
  public float Amount = 0f;
  public float Required = 100f;

  public override void EncounterStart() {
    base.EncounterStart();
    RiftManager.Instance.OnExperienceCollected += OnExperienceCollected;
    StartCoroutine(EnergyOrbSpawnRoutine());
    IsStarted = true;
  }

  public override IEnumerator EncounterRoutine() {
    while (true) {
      RiftManager.Instance.Energy.Hurt(null, null, 5f * Time.deltaTime, true);
      yield return null;
    }
  }

  public override void EncounterEnd() {
    base.EncounterEnd();
    RiftManager.Instance.OnExperienceCollected -= OnExperienceCollected;
    StopAllCoroutines();
  }

  private void OnExperienceCollected(float amount) => Amount += amount;

  private IEnumerator EnergyOrbSpawnRoutine() {
    while (true) {
      yield return new WaitUntil(() => PoolManager.Instance.EnergyOrbs.Objects.CountActive < 1);
      yield return new WaitForSeconds(2f);
      GameObject energyOrb = PoolManager.Instance.EnergyOrbs.Objects.Get();
      energyOrb.transform.position = 18f * Random.insideUnitCircle;
    }
  }
}