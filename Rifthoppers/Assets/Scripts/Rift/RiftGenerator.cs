using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftGenerator : MonoBehaviour {
  public AreaGenerator AreaGenerator;

  public List<Wave> GenerateRift() {
    List<Wave> rift = new();
    rift.Add(GenerateWave());
    rift.Add(GenerateWave());
    rift.Add(GenerateWave());

    return rift;
  }

  public Wave GenerateWave() {
    GameObject wave = new($"Wave {0}");
    wave.transform.SetParent(transform);

    Encounter encounter = new CollectionEncounter();
    encounter.Area = AreaGenerator.EncounterArea(0);
    encounter.Area.transform.SetParent(wave.transform);

    Reward reward = new Reward();

    return new Wave(encounter, reward);
  }
}