using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    Collider coll;
    Renderer rend;
    [SerializeField] Material rewarded;
    [SerializeField] Material reward;

    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        coll.enabled = false;
        rend.material = rewarded;
    }

    public void Reset()
    {
        coll.enabled = true;
        rend.material = reward;
    }
}
