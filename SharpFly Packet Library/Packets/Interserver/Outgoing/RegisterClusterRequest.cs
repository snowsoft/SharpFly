﻿using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterClusterRequest
    {
        public RegisterClusterRequest(string authorizationPassword, uint id, string name, string ip, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.REGISTER_CLUSTER_REQUEST);
            packet.Write(authorizationPassword);
            packet.Write(id);
            packet.Write(name);
            packet.Write(ip);
            packet.Send(socket);
        }
    }
}
