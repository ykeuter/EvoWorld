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

    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        transform.Rotate(0, angularSpeed * vectorAction.ContinuousActions[0] * Time.fixedDeltaTime, 0);
        transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
    }

 
    private void OnCollisionEnter(Collision collision)
    {
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
