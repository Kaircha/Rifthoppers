using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftLoader : MonoBehaviour {
  // Temporary until a second rift is needed
  public List<GameObject> Waves = new List<GameObject>();
  public GameObject Upgrade;
  public GameObject Dead;
  public Material FadeMaterial;

  public GameObject Current;
  private GameObject Temp;

  // These should play an animation for the load
  public void LoadDead() {
    StopAllCoroutines();
    Temp = Instantiate(Dead, Current.transform);
  }

  public void LoadWave() {
    StopAllCoroutines();
    StartCoroutine(Load(Waves[Random.Range(0, Waves.Count)]));
  }

  public void LoadUpgrade() {
    StopAllCoroutines();
    Temp = Instantiate(Upgrade, Current.transform);
  }

  public IEnumerator Load(GameObject ToSpawn) {
    if (Temp != null) Destroy(Temp);
    GameObject newArea = Instantiate(ToSpawn, transform);

    FadeMaterial.SetFloat("_Alpha", 0f);

    if (Current.TryGetComponent(out SpriteRenderer mainRenderer)) {
      mainRenderer.material = FadeMaterial;
      mainRenderer.sortingOrder = 10;
    }
    foreach (Transform child in Current.transform) {
      if (child.TryGetComponent(out SpriteRenderer childRenderer)) {
        childRenderer.material = FadeMaterial;
        childRenderer.sortingOrder = 11;
      }
    }

    float timer = 0f;
    while (timer < 1f) {
      FadeMaterial.SetFloat("_Alpha", timer);
      timer += Time.deltaTime;
      yield return null;
    }
    timer = 1f;
    FadeMaterial.SetFloat("_Alpha", timer);
    
    Destroy(Current);
    Current = newArea;
  }
}