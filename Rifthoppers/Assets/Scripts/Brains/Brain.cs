using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Brain : MonoBehaviour {
  [HideInInspector] public StateMachine Machine;
  public virtual Entity Entity { get; set; }

  public virtual void Awake() {
    Entity = GetComponent<Entity>();
    Machine = GetComponent<StateMachine>();  
  }
}