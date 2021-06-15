﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class BasicManager : MonoBehaviour
{
    public float roomSize = 10f;
    float birthDistance = 2f;
    [SerializeField] float width = 1.0f;
    [SerializeField] Food foodPrefab;
    [SerializeField] Agent agentPrefab;
    int numAgents = 0;
    BirthChannel birthChannel;

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
        birthChannel = new BirthChannel();
        SideChannelManager.RegisterSideChannel(birthChannel);
        Conceive();
    }

    public void OnDestroy()
    {
        SideChannelManager.UnregisterSideChannel(birthChannel);
    }

    public void Eliminate(Agent a)
    {
        numAgents--;
        a.gameObject.SetActive(false);
        Debug.Log("Destroy " + a.Id);
        Destroy(a.gameObject);
        if (numAgents <= 0)
        {
            Conceive();
        }
    }

    public void Conceive(Agent parent1 = null)
    {
        numAgents++;
        Agent newborn = Instantiate(agentPrefab, transform.parent);
        newborn.transform.Rotate(0, Random.Range(0, 360), 0);
        if (parent1 is null)
        {
            birthChannel.Conceive(newborn.Id);
        }
        else
        {
            newborn.transform.position = parent1.transform.position
                + Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * birthDistance;
            birthChannel.Conceive(newborn.Id, parent1.Id);
        }
    }
}
