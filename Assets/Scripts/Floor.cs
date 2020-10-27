using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] int width = 20;
    [SerializeField] int length = 60;
    float height = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= length; i++)
        {
            float x = (float)i - length / 2.0f;
            for (int j = 0; j <= width; j++)
            {
                float z = (float)j - width / 2.0f;
                Instantiate(tile, new  Vector3(x, tile.transform.position.y, z), tile.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
