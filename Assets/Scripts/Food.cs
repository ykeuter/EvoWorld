using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Collider coll;
    Renderer rend;
    [SerializeField] Material chargingMaterial;
    [SerializeField] Material foodMaterial;
    float chargingTime = 0.0f;
    [SerializeField] float chargingTimeNeeded = 5.0f;
    bool isCharging = false;

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

    private void FixedUpdate()
    {
        if (!isCharging) return;

        chargingTime += Time.deltaTime;
        if (chargingTime > chargingTimeNeeded)
        {
            chargingTime = 0.0f;
            isCharging = false;
            rend.material = foodMaterial;
            coll.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        coll.enabled = false;
        rend.material = chargingMaterial;
        isCharging = true;
    }
}
