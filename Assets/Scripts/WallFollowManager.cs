﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using Unity.MLAgents;

public class WallFollowManager : MonoBehaviour
{
    public float roomSize = 10.0f;
    [SerializeField] float width = 1.0f;
    [SerializeField] Reward rewardPrefab;
    [SerializeField] WallFollower playerPrefab;
    [SerializeField] int rotation = 0;

    List<Reward> rewards = new List<Reward>();
    WallFollower player;

    [SerializeField] List<GameObject> obstacles;
    // Start is called before the first frame update
    private void Awake()
    {
        float d = (roomSize - width) / 2;
        float y = rewardPrefab.transform.position.y;
        Reward r;
        for (float x = -d; x <= d; x += width) {
            for (float z = -d; z <= d; z += width) {
                if (Mathf.Abs(x) < width && Mathf.Abs(z) < width) continue;
                r = Instantiate(rewardPrefab, transform.parent);
                r.transform.localPosition = new Vector3(x, y, z);
                rewards.Add(r);
            }
        }
        Academy.Instance.OnEnvironmentReset += ResetArea;
        player = Instantiate(playerPrefab, transform.parent);
    }
    public void ResetArea()
    {
        //Debug.Log("reset area");
        foreach (Reward r in rewards)
        {
            r.ResetReward();
        }
        player.ResetPlayer();
        player.transform.localPosition = new Vector3(0, player.transform.position.y, 0);
        player.transform.localRotation = Quaternion.Euler(0, rotation, 0);
        //player.transform.localRotation = Quaternion.Euler(0, Academy.Instance.EnvironmentParameters.GetWithDefault("angle", 0), 0);
        //player.transform.position = GetRandomPosition();
    }

    Vector3 GetRandomPosition() {
        float d = roomSize / 2 - width * 1.5f;
        float x = Random.Range(-d, d);
        float y = playerPrefab.transform.position.y;
        float z = Random.Range(-d, d);
        Vector3 p = new Vector3(x, y, z);
        foreach (GameObject g in obstacles) {
            if (Vector3.Distance(p, g.transform.position) < width)
                return GetRandomPosition();
        }
        return p;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
