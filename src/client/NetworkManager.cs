using Godot;
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;

namespace DC.Frontend
{
    public class NetworkManager : Node
    {
        public static NetworkManager instance;

        public Client client { get; private set; }

        [Export] private ushort port = 80;
        [Export] private string ip = "127.0.0.1";

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

            client = new Client();
            client.Connect($"{ip}:{port}");
        }

        public override void _Process(float delta)
        {
            client.Tick();
        }

        public override void _ExitTree()
        {
            client.Disconnect();
        }
    }
}
