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
        timeLeft = chargeTime;
        age = 0;
        wfm.Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //float x = transform.position.x;
        //float z = transform.position.z;
        //float d = wfm.roomSize / 2;
        //float forward = z > 0 ? z / d : 0;
        //float right = x > 0 ? x / d : 0;
        //float back = z < 0 ? -z / d : 0;
        //float left = x < 0 ? -x / d : 0;
        //sensor.AddObservation(forward);
        //sensor.AddObservation(right);
        //sensor.AddObservation(back);
        //sensor.AddObservation(left);
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
        //transform.position += Vector3.forward * vectorAction[0] * Time.fixedDeltaTime * speed;
        //transform.position += Vector3.right * vectorAction[1] * Time.fixedDeltaTime * speed;
        //transform.position += Vector3.back * vectorAction[2] * Time.fixedDeltaTime * speed;
        //transform.position += Vector3.left * vectorAction[3] * Time.fixedDeltaTime * speed;
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
        //float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        actionsOut[0] = h < 0 ? -h : 0;
        actionsOut[1] = h > 0 ? h : 0;
        //actionsOut[2] = v < 0 ? -v : 0;
        //actionsOut[3] = h < 0 ? -h : 0;
    }
}
