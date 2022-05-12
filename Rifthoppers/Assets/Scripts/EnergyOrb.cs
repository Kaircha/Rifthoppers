using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
  public float Energy = 30;
  public int Progress = 25;

  public float speedMulti = 1;

  private void Update()
  {
    Vector3 closest = Vector3.zero;
    float minDistance = int.MaxValue;

    // might want to change how this works in the future

    foreach(Player player in LobbyManager.Instance.Players){
      float dis = Vector2.Distance(player.Entity.transform.position, transform.position);
      if(dis < minDistance){
        minDistance = dis;
        closest = player.Entity.transform.position;
      }
    }

    float speed = 7 / minDistance;

    transform.position = Vector2.MoveTowards(transform.position, closest, speed * speedMulti * Time.deltaTime);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    RiftManager.Instance.Energy.Heal(Energy);
    RiftManager.Instance.Experience.Learn(Progress);
    RiftManager.Instance.EnergyCollected();
    Destroy(this.gameObject);
  }
}
