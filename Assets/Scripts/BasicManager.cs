using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using Unity.MLAgents;

public class BasicManager : MonoBehaviour
{
    public float roomSize = 100.0f;
    [SerializeField] float width = 1.0f;
    [SerializeField] Food foodPrefab;
    [SerializeField] Agent agentPrefab;

    private void Awake()
    {
        float d = (roomSize - width) / 2;
        float y = foodPrefab.transform.position.y;
        Food r;
        for (float x = -d; x <= d; x += width) {
            for (float z = -d; z <= d; z += width) {
                if (Mathf.Abs(x) < width && Mathf.Abs(z) < width) continue;
                r = Instantiate(foodPrefab, transform.parent);
                r.transform.localPosition = new Vector3(x, y, z);
            }
        }
        Instantiate(agentPrefab, transform.parent);
    }
}
