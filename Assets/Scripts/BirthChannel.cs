using UnityEngine;
using Unity.MLAgents.SideChannels;
using System;

public class BirthChannel : SideChannel
{
    public BirthChannel()
    {
        ChannelId = new Guid("51d41610-6239-4ef1-98c4-cad4b8d70a17");
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        Debug.Log("Should not happen");
    }

    public void Conceive(int id, int parent1=0, int parent2=0)
    {
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteInt32(id);
            msgOut.WriteInt32(parent1);
            msgOut.WriteInt32(parent2);
            QueueMessageToSend(msgOut);
        }
    }
}