using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class WallFollowManager : MonoBehaviour
{
    public float roomSize = 10.0f;
    [SerializeField] float width = 1.0f;
    [SerializeField] Reward rewardPrefab;
    [SerializeField] WallFollower playerPrefab;

    List<Reward> rewards = new List<Reward>();
    WallFollower player;

    // Start is called before the first frame update
    private void Awake()
    {
        float d = (roomSize - width) / 2;
        float y = rewardPrefab.transform.position.y;
        Reward r;
        for (float x = -d; x <= d; x += width)
        {
            r = Instantiate(rewardPrefab, new Vector3(x, y, d), rewardPrefab.transform.rotation);
            rewards.Add(r);
            r = Instantiate(rewardPrefab, new Vector3(x, y, -d), rewardPrefab.transform.rotation);
            rewards.Add(r);
        }
        for (float z = -d + width; z <= d - width; z += width)
        {
            r = Instantiate(rewardPrefab, new Vector3(d, y, z), rewardPrefab.transform.rotation);
            rewards.Add(r);
            r = Instantiate(rewardPrefab, new Vector3(-d, y, z), rewardPrefab.transform.rotation);
            rewards.Add(r);
        }

        player = Instantiate(playerPrefab);
    }
    public void Reset()
    {
        foreach (Reward r in rewards)
        {
            r.Reset();
        }

        float d = roomSize / 2 - width * 1.5f;
        float x = Random.Range(-d, d);
        float y = playerPrefab.transform.position.y;
        float z = Random.Range(-d, d);
        player.transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
