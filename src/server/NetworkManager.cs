using Godot;
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;

namespace DC.Backend
{
    public class NetworkManager : Node
    {
        public static NetworkManager instance;

        public Server server { get; private set; }

        [Export] private ushort port = 80;
        [Export] private ushort maxClients = 4;

        public override void _EnterTree()
        {
            if (instance == null) instance = this;
            else if (instance != this) GetParent().RemoveChild(this);
        }

        public override void _Ready()
        {
            RiptideLogger.Initialize((String arg) => 
            {
                GD.Print(arg);
            }, false);

            server = new Server();
            server.Start(port, maxClients);
        }

        public override void _Process(float delta)
        {
            server.Tick();
        }

        public override void _ExitTree()
        {
            server.Stop();
        }
    }
}
