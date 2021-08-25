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

    (Vector3 pos, Vector3 rot)[] cases = new (Vector3 pos, Vector3 rot)[] {
        (Vector3.forward, Vector3.zero),
        (Vector3.back, Vector3.zero),
        (Vector3.left, Vector3.up * 90),
        (Vector3.right, Vector3.up * 90)
    };

    private void Awake()
    {
        height = transform.localPosition.y;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    public void ResetPlayer()
    {
        transform.localPosition = Vector3.up * height;
        transform.localEulerAngles = Vector3.zero;
        int caseId = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("case_id", 0);
        target.transform.localPosition = cases[caseId].pos;
        target.transform.localEulerAngles = cases[caseId].rot;
        idle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        idle = true;
        AddReward(1);
        EndEpisode();
    }

    private void OnCollisionEnter()
    {
        idle = true;
        AddReward(-1);
        EndEpisode();
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   if (!idle) {
            transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0];
            transform.position += transform.right * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        continuousActionsOut[0] = v;
        continuousActionsOut[1] = h;
    }
}
