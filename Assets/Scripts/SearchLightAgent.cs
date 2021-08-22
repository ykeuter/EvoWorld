using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] float angularSpeed = 400.0f;
    [SerializeField] GameObject target;
    float size = 2;
    float height;
    bool idle = true;

    private void Awake()
    {
        height = transform.localPosition.y;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        target.transform.localPosition = new Vector3(Random.Range(-size, size), 0, Random.Range(-size, size));
        transform.localPosition = Vector3.up * height;
        transform.localEulerAngles = Vector3.up * Random.Range(0, 360);
        idle = false;
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   if (!idle) {
            transform.Rotate(0, angularSpeed * vectorAction.ContinuousActions[0] * Time.fixedDeltaTime, 0);
            transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
        }
        SetReward(Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), target.transform.position));
    }

    private void OnCollisionEnter(Collision collision)
    {
        idle = true;
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (v < 0) v = 0;
        continuousActionsOut[0] = h;
        continuousActionsOut[1] = v;
    }
}
