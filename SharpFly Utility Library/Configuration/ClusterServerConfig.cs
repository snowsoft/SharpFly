﻿namespace SharpFly_Utility_Library.Configuration
{
    public class ClusterServerConfig : Config
    {
        public ClusterServerConfig(string path) : base(path)
        {
            base.Read("MySQLAddress", "Address", "MySQL", typeof(string));
            base.Read("MySQLUsername", "Username", "MySQL", typeof(string));
            base.Read("MySQLPassword", "Password", "MySQL", typeof(string));
            base.Read("MySQLDatabaseLogin", "DatabaseLogin", "MySQL", typeof(string));
            base.Read("MySQLDatabaseCluster", "DatabaseCluster", "MySQL", typeof(string));
            base.Read("MySQLPort", "Port", "MySQL", typeof(int));
            base.Read("ClusterAuthorizationPassword", "ClusterAuthorizationPassword", "Security", typeof(string));
            base.Read("Md5Salt", "Md5Salt", "Security", typeof(string));
            base.Read("ClientBuildDate", "ClientBuildDate", "Security", typeof(string));
        }
    }
}
