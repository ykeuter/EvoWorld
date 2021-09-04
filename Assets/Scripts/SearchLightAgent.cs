using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    float speed = 1.0f;
    [SerializeField] GameObject target;
    Vector3 startPos;
    bool idle = true;

    (Vector3 pos, Vector3 rot)[] cases = new (Vector3 pos, Vector3 rot)[] {
        (Vector3.forward * 2, Vector3.zero),
        (Vector3.back * 2, Vector3.zero),
        (Vector3.left * 2, Vector3.up * 90),
        (Vector3.right * 2, Vector3.up * 90)
    };

    private void Awake()
    {
        startPos = transform.localPosition;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    public void ResetPlayer()
    {
        transform.localPosition = startPos;
        transform.localEulerAngles = Vector3.zero;
        int caseId = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("case_id", 0);
        target.transform.localPosition = cases[caseId].pos;
        target.transform.localEulerAngles = cases[caseId].rot;
        idle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (idle) return;
        if (other.gameObject == target)
        {
            AddReward(1);
            Debug.Log("you win");
        }
        else
        {
            AddReward(-1);
            Debug.Log("you lose");
        }
        idle = true;
        EndEpisode();
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   if (idle) return;
        transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0];
        transform.position += transform.right * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
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
