using UnityEngine;
using Unity.MLAgents.SideChannels;
using System;

public class AgeChannel : SideChannel
{
    public AgeChannel()
    {
        ChannelId = new Guid("130f04e4-fb3d-41b6-bc6a-2b796678dd47");
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        Debug.Log("Should not happen");
    }

    public void Age(float age)
    {
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteFloat32(age);
            QueueMessageToSend(msgOut);
        }
    }
}