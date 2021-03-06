﻿using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using HearthStone.Database;
using HearthStone.Database.MySQL;
using HearthStone.Library;
using HearthStone.Server;
using HearthStone.Server.Configurations;
using log4net.Config;
using Photon.SocketServer;
using System.IO;

namespace HearthStone.PhotonServerEnvironment
{
    public class PhotonServerApplication : ApplicationBase
    {
        public static PhotonServerApplication ServerInstance { get; private set; }
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new PhotonServerPeer(initRequest);
        }

        protected override void Setup()
        {
            ServerInstance = this;
            SetupServices();
            SetupFactories();
            LogService.Info("Server Setup Successful!");
        }

        protected override void TearDown()
        {

        }

        private void SetupServices()
        {
            SetupLog();
            DatabaseService.Initial(new MySQL_DatabaseService());
            DatabaseConfiguration databaseConfig;
            DatabaseConfiguration.Load("config/database.config", out databaseConfig);
            if (DatabaseService.Connect(databaseConfig.Hostname, databaseConfig.Username, databaseConfig.Password, databaseConfig.Database))
            {
                LogService.Info("DatabaseService Setup Successful!");
            }
        }
        private void SetupFactories()
        {
            EndPointFactory.Initial();
            PlayerMatchManager.Initial();
            GameManager.Initial(new GameFactory());
        }

        private void SetupLog()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
            LogService.InitialService(Log.Info, Log.InfoFormat, Log.Error, Log.ErrorFormat, Log.Fatal, Log.FatalFormat);
        }
    }
}
