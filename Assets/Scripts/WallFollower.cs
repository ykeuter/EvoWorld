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
    [SerializeField] float speed = 2.0f;
    [SerializeField] float angularSpeed = 4.0f;
    WallFollowManager wfm;

    private void Awake()
    {
        wfm = GameObject.Find("Game Manager").GetComponent<WallFollowManager>();
    }

    public override void OnEpisodeBegin()
    {
        wfm.Reset();
    }

    public void Reset()
    {
        timeLeft = chargeTime;
        age = 0;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            EndEpisode();
        }
        age += Time.deltaTime;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] > .5) transform.Rotate(0, -angularSpeed, 0);
        if (vectorAction[1] > .5) transform.Rotate(0, angularSpeed, 0);
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
        float h = Input.GetAxis("Horizontal");
        actionsOut[0] = h < 0 ? -h : 0;
        actionsOut[1] = h > 0 ? h : 0;
    }
}
