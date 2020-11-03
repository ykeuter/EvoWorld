using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float roomSize = 10.0f;
    [SerializeField] float margin = .2f;
    [SerializeField] float width = 1.0f;
    [SerializeField] float density = 4.0f;
    [SerializeField] float height = 1.5f;
    [SerializeField] GameObject pillPrefab;
    [SerializeField] WallFollower wfPrefab;

    List<GameObject> pills = new List<GameObject>();
    WallFollower wf;

    // Start is called before the first frame update
    private void Start()
    {
        float length = roomSize - 2 * margin;
        float area = length * width * 4; // let's not deal with double counting in corners
        int numPills = Mathf.CeilToInt(area * density);
        for (int i = 0; i < numPills; i++)
        {
            pills.Add(Instantiate(pillPrefab));
        }
        wf = Instantiate(wfPrefab);
    }
    public void ResetArea()
    {
        float length = roomSize - 2 * margin;
        float x, z;
        foreach (GameObject p in pills)
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
            p.transform.position = new Vector3(x, height, z);
            p.SetActive(true);
        }
        float r = roomSize / 2 - margin - width;
        x = Random.Range(-r, r);
        z = Random.Range(-r, r);
        wf.transform.position = new Vector3(x, height, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
