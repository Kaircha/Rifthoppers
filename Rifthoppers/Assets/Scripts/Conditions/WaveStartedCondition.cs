using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartedCondition : Condition {
  private bool WaveStarted;

  public override bool Satisfied => WaveStarted;
  public override void Initialize() => RiftManager.Instance.OnEncounterStarted += SetWaveStarted;
  public override void Reset() => WaveStarted = false;
  public override void Terminate() => RiftManager.Instance.OnEncounterStarted -= SetWaveStarted;

  public void SetWaveStarted() => WaveStarted = true;
}