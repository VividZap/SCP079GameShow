using Exiled.API.Interfaces;
using System.ComponentModel;
using Exiled.API.Features;
using Cassie = Exiled.Events.Handlers.Cassie;

namespace SCP079GameShow
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true; 

        [Description("Should debug messages be shown in the console?")]
        public bool Debug { get; set; } = false; 

        [Description("The interval in seconds at which the game show can potentially start.")]
        public int GameShowIntervalSeconds { get; set; } = 180; // Every 3 minutes

        [Description("The chance (0.0 to 1.0) for a game show to start at each interval.")]
        public float GameShowChance { get; set; } = 0.3f; // 30% chance
    }
}