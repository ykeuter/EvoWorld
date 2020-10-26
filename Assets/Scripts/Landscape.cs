using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Landscape : MonoBehaviour
{
  [SerializeField] GameObject foodPrefab;
  [SerializeField] int width = 100;
  float height = 3.5f;
  // Start is called before the first frame update
  void Awake()
  {
    for (int i = 0; i <= width; i++)
    {
      float x = (float)i - width / 2.0f;
      for (int j = 0; j < width; j++)
      {
        float z = (float)j - width / 2.0f + .5f;
        //Instantiate(foodPrefab, new  Vector3(x, height, z), foodPrefab.transform.rotation);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
        
  }
}
