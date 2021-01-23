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
    [SerializeField] float angularSpeed = 400.0f;
    WallFollowManager wfm;
    bool idle = true;

    private void Awake()
    {
        wfm = GameObject.Find("Game Manager").GetComponent<WallFollowManager>();
    }

    public override void OnEpisodeBegin()
    {
        //wfm.Reset();
    }

    public void ResetPlayer()
    {
        timeLeft = chargeTime;
        age = 0;
        idle = false;
    }

    new void EndEpisode(){
        transform.localPosition = new Vector3(0, transform.position.y, 0);
        idle = true;
        base.EndEpisode();
    }

    private void FixedUpdate()
    {
        if (idle) return;

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
        transform.Rotate(0, angularSpeed * vectorAction[0] * Time.fixedDeltaTime, 0);
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
        actionsOut[0] = h;
    }
}
