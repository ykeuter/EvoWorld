using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    readonly float speed = 1.0f;
    readonly float rotSpeed = 180.0f;
    [SerializeField] GameObject target;
    Vector3 startPos;
    readonly float bound = 2.0f;
    //bool idle = true;

    //(Vector3 pos, Vector3 rot)[] cases = new (Vector3 pos, Vector3 rot)[] {
    //    (Vector3.forward * 2, Vector3.zero),
    //    (Vector3.back * 2, Vector3.zero),
    //    (Vector3.left * 2, Vector3.up * 90),
    //    (Vector3.right * 2, Vector3.up * 90)
    //};

    private void Awake()
    {
        startPos = transform.localPosition;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("new episode");
    }

    public void ResetPlayer()
    {
        transform.localPosition = startPos;
        transform.localEulerAngles = Vector3.zero;
        //int caseId = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("case_id", 0);
        //target.transform.localPosition = cases[caseId].pos;
        //target.transform.localEulerAngles = cases[caseId].rot;
        //idle = false;
        target.transform.localPosition = GetRandomPosition();
    }

    private void OnTriggerStay(Collider other)
    {
        //if (idle) return;
        //if (other.gameObject == target)
        //{
        AddReward(1);
        Debug.Log("you win");
        target.transform.localPosition = GetRandomPosition();
        //}
        //else
        //{
        //    AddReward(-1);
        //    Debug.Log("you lose");
        //}
        //idle = true;
        //EndEpisode();
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-bound, bound), startPos.y, Random.Range(-bound, bound));
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   
        //if (idle) return;
        transform.position += Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0] * transform.forward;
        transform.Rotate(0, Time.fixedDeltaTime * rotSpeed * (vectorAction.ContinuousActions[2] - vectorAction.ContinuousActions[1]), 0);
        //transform.position += Time.fixedDeltaTime * speed * (vectorAction.ContinuousActions[0] - vectorAction.ContinuousActions[1]) * transform.forward;
        //transform.position += Time.fixedDeltaTime * speed * (vectorAction.ContinuousActions[2] - vectorAction.ContinuousActions[3]) * transform.right;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        //float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        //if (v > 0) continuousActionsOut[0] = v;
        //else continuousActionsOut[1] = -v;
        if (h > 0) continuousActionsOut[2] = h;
        else continuousActionsOut[1] = -h;
        continuousActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
    }
}
