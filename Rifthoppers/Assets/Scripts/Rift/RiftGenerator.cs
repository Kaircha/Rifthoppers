using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftGenerator : MonoBehaviour {
  public AreaGenerator AreaGenerator;

  public Wave GenerateRift() {
    Wave rift = GenerateWave(0);
    //rift.SetChild(GenerateWave(1));
    //rift.Child.SetChild(GenerateWave(2));
    //rift.Child.Child.SetChild(GenerateWave(3));

    return rift;
  }

  public Wave GenerateWave(int index) {
    GameObject wave = new GameObject($"Wave {index}");
    wave.transform.SetParent(transform);

    //Encounter encounter = new GameObject("Encounter").AddComponent<ExperienceEncounter>();
    Encounter encounter = new GameObject("Encounter").AddComponent<WormEncounter>();
    encounter.transform.SetParent(wave.transform);
    encounter.Area = AreaGenerator.EncounterArea(0);
    encounter.Area.transform.SetParent(encounter.transform);

    Reward reward = new GameObject("Reward").AddComponent<UpgradeReward>();
    reward.transform.SetParent(wave.transform);
    reward.Area = AreaGenerator.RewardArea();
    reward.Area.transform.SetParent(reward.transform);

    return new Wave(index, encounter, reward);
  }
}