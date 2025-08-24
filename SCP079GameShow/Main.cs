using Exiled.API.Features;
using System;

namespace SCP079GameShow
{
    public class SCP079GameShow : Plugin<Config>
    {
        public override string Name => "SCP079GameShow";
        public override string Author => "VividZap";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 8, 1);

        public static SCP079GameShow Instance { get; private set; }

        private EventHandlers _eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new EventHandlers();

            Exiled.Events.Handlers.Server.RoundStarted += _eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += _eventHandlers.OnRoundEnded;

            Log.Info("SCP079GameShow enabled!");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= _eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= _eventHandlers.OnRoundEnded;

            _eventHandlers = null;
            Instance = null;

            Log.Info("SCP079GameShow disabled!");
            base.OnDisabled();
        }
    }
}