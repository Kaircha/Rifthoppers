using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Brain : MonoBehaviour {
  [HideInInspector] public StateMachine Machine;
  [HideInInspector] public Entity Entity;
  public Transform Target;

  public virtual void Awake() {
    Entity = GetComponent<Entity>();
    Machine = GetComponent<StateMachine>();  
  }
}