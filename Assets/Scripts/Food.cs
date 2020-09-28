using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  [SerializeField] Material blue;
  [SerializeField] Material red;
  Renderer rend;

  [SerializeField] float chargeTime = 5;
  float timeLeft;
  bool isActive = true;

  // Start is called before the first frame update
  void Start()
  {
    rend = GetComponent<Renderer>();
  }

    // Update is called once per frame
  void Update()
  {
    if (!isActive)
    {
      if (timeLeft < 0)
      {
        isActive = true;
        rend.material = blue;
      }
      timeLeft -= Time.deltaTime;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (isActive)
    {
      isActive = false;
      rend.material = red;
      timeLeft = chargeTime;
    }
  }
}
