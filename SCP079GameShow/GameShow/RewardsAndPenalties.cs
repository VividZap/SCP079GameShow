using Exiled.API.Features;
using System.Collections.Generic;
using Exiled.API.Enums;
using UnityEngine;

namespace SCP079GameShow
{
    public static class RewardsAndPenalties
    {
        public static void GrantReward(Player player)
        {
            // Implement various rewards here
            int rewardType = UnityEngine.Random.Range(0, 3);
            switch (rewardType)
            {
                case 0:
                    player.Health += 50; // Heal player
                    player.Broadcast(5, "<color=green>You received a health boost!</color>");
                    break;
                case 1:
                    player.AddItem(ItemType.Medkit); // Give medkit
                    player.Broadcast(5, "<color=green>You received a Medkit!</color>");
                    break;
                case 2:
                    player.EnableEffect(EffectType.MovementBoost, 10); // Speed boost for 10 seconds
                    player.Broadcast(5, "<color=green>You received a temporary speed boost!</color>");
                    break;
            }
            Log.Info($"Granted reward to {player.Nickname}");
        }

        public static void ApplyPenalty(Player player)
        {
            // Implement various penalties here
            int penaltyType = UnityEngine.Random.Range(0, 2);
            switch (penaltyType)
            {
                case 0:
                    player.Hurt(10); // Deal minor damage
                    player.Broadcast(5, "<color=red>You received a minor penalty!</color>");
                    break;
                case 1:
                    player.DisableEffect(EffectType.MovementBoost); // Slow player
                    player.Broadcast(5, "<color=red>You received a temporary movement penalty!</color>");
                    break;
            }
            Log.Info($"Applied penalty to {player.Nickname}");
        }
    }
}