using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently not ready for use; Also not needed
public class PlayerBrain : Brain {
  public override State EntryState => RiftState;

  public PlayerAIState AIState;
  public PlayerHubState HubState;
  public PlayerRiftState RiftState;

  public override void Initialize() {
    AIState = new(Entity);
    HubState = new(Entity);
    RiftState = new(Entity);

    Transitions = new List<Transition> {
      new(HubState, RiftState, new WaveStartedCondition()),
      new(RiftState, HubState, new WaveEndedCondition()),
      //new(AIState, HubState, new CharacterActivatedCondition()),
      //new(HubState, AIState, new CharacterDeactivatedCondition()),
    };
  }
}