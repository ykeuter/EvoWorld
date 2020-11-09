using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class WallFollower : Agent
{
    float chargeTime = 5.0f;
    float timeLeft;
    float age = 0.0f;
    float speed = 1.0f;
    WallFollowManager wfm;

    private void Awake()
    {
        wfm = GameObject.Find("Game Manager").GetComponent<WallFollowManager>();
    }

    public override void OnEpisodeBegin()
    {
        timeLeft = chargeTime;
        age = 0;
        wfm.Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float x = transform.position.x;
        float z = transform.position.z;
        sensor.AddObservation(wfm.roomSize / 2 - x);
        sensor.AddObservation(wfm.roomSize / 2 + x);
        sensor.AddObservation(wfm.roomSize / 2 - z);
        sensor.AddObservation(wfm.roomSize / 2 + z);
    }



    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            EndEpisode();
        }
        age += Time.deltaTime;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        transform.position += transform.forward * vectorAction[0] * Time.fixedDeltaTime * speed;
        transform.position += transform.right * vectorAction[1] * Time.fixedDeltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        timeLeft = chargeTime;
        AddReward(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EndEpisode();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");
    }
}
