using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] GameObject target;
    float height;
    bool idle = true;

    private void Awake()
    {
        height = transform.localPosition.y;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    private void OnTriggerStay(Collider other)
    {
        idle = true;
        AddReward(1);
        EndEpisode();
    }

    public void ResetPlayer()
    {

        transform.localPosition = Vector3.up * height;
        idle = false;
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   if (!idle) {
            transform.position += transform.right * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0];
            transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
        }
    }

    private void OnCollisionEnter()
    {
        idle = true;
        AddReward(-1);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        continuousActionsOut[0] = h;
        continuousActionsOut[1] = v;
    }
}
