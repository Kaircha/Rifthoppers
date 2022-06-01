using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEndedCondition : Condition {
  private bool WaveEnded;

  public override bool Satisfied => WaveEnded;
  public override void Initialize() => RiftManager.Instance.OnEncounterEnded += SetWaveStarted;
  public override void Reset() => WaveEnded = false;
  public override void Terminate() => RiftManager.Instance.OnEncounterEnded -= SetWaveStarted;

  public void SetWaveStarted() => WaveEnded = true;
}