using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float roomSize = 10.0f;
    [SerializeField] float margin = .2f;
    [SerializeField] float width = 1.0f;
    [SerializeField] float density = 4.0f;
    [SerializeField] float height = 1.5f;
    [SerializeField] GameObject pill;
    [SerializeField] GameObject agent;

    // Start is called before the first frame update
    void Start()
    {
        float length = roomSize - 2 * margin;
        float area = length * width * 4; // let's not deal with double counting in corners
        int numPills = Mathf.CeilToInt(area * density);
        float x, z;
        for (int i = 0; i < numPills; i++)
        {
            x = Random.Range(-length / 2, length / 2);
            z = Random.Range(0, width) + roomSize / 2 - width - margin;
            if (Random.value < .5)
            {
                z = -z;
            }
            if (Random.value < .5)
            {
                float temp = x;
                x = z;
                z = temp;
            }
            Instantiate(pill, new Vector3(x, height, z), pill.transform.rotation);
        }
        float r = roomSize / 2 - margin - width;
        x = Random.Range(-r, r);
        z = Random.Range(-r, r);
        Instantiate(agent, new Vector3(x, height, z), agent.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
