﻿using SharpFly_Packet_Library.Security;
using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.LoginDatabase;
using SharpFly_Utility_Library.Database.ClusterDatabase;
using SharpFly_Cluster.Player;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SharpFly_Cluster.Server
{
    public class ClusterServer : IDisposable
    {
        private Socket m_PlayerSocket { get; set; }

        public static Config Config { get; private set; }
        public static string IpAddress { get; private set; } = "127.0.0.1";
        public static LoginDatabase LoginDatabase;
        public static LoginServerConnector LoginServerConnector;
        public static PlayerManager PlayerManager;
        public static ClusterDatabase ClusterDatabase;

        public ClusterServer(int Port)
        {
            Rijndael.Initiate();
            Config = new ClusterServerConfig("Resources/Config/Cluster.ini");
            LoginDatabase = new LoginDatabase(Config);
            ClusterDatabase = new ClusterDatabase(Config);
            if (ClusterDatabase.Connection.CheckConnection() && LoginDatabase.Connection.CheckConnection())
            {
                Console.WriteLine("Connecting to login server...");
                LoginServerConnector = new LoginServerConnector();

                if (LoginServerConnector.TryConnectToLoginServer())
                {
                    this.m_PlayerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.m_PlayerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
                    this.m_PlayerSocket.Listen(100);

                    PlayerManager = new PlayerManager();

                    Thread acceptPlayerThread = new Thread(() => PlayerManager.AcceptPlayers(this.m_PlayerSocket));
                    acceptPlayerThread.Start();

                    Thread processPlayerThread = new Thread(() => PlayerManager.ProcessPlayers());
                    processPlayerThread.Start();
                }
            }
        }

        public void Dispose()
        {
            this.m_PlayerSocket.Shutdown(SocketShutdown.Both);
            this.m_PlayerSocket.Close();
        }
    }
}
