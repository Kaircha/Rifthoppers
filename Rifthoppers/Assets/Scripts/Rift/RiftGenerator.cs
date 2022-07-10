using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftGenerator : MonoBehaviour {
  public AreaGenerator AreaGenerator;

  public List<Wave> GenerateRift() {
    List<Wave> rift = new();
    rift.Add(GenerateWave(0));
    rift.Add(GenerateWave(0)); // added another one so I can test the reward system

    return rift;
  }

  public Wave GenerateWave(int index) {
    GameObject wave = new GameObject($"Wave {index}");
    wave.transform.SetParent(transform);

    Encounter encounter = new GameObject("Encounter").AddComponent<CollectionEncounter>();
    //Encounter encounter = new GameObject("Encounter").AddComponent<WormEncounter>();
    encounter.transform.SetParent(wave.transform);
    encounter.Area = AreaGenerator.EncounterArea(0);
    encounter.Area.transform.SetParent(encounter.transform);

    Reward reward = new Reward();

    return new Wave(index, encounter, reward);
  }
}