using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using System;
using Random = UnityEngine.Random;

namespace SCP079GameShow
{
    public class EventHandlers
    {
        private System.Timers.Timer _gameShowTimer;

        public void OnRoundStarted()
        {
            Log.Info("Round started. Initializing Game Show timer.");
            _gameShowTimer = new System.Timers.Timer(SCP079GameShow.Instance.Config.GameShowIntervalSeconds * 1000);
            _gameShowTimer.Elapsed += OnGameShowTimerElapsed;
            _gameShowTimer.AutoReset = true;
            _gameShowTimer.Start();
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Log.Info("Round ended. Stopping Game Show timer.");
            if (_gameShowTimer != null)
            {
                _gameShowTimer.Stop();
                _gameShowTimer.Dispose();
                _gameShowTimer = null;
            }
            GameShowManager.Instance.EndCurrentChallenge(); // Ensure any active challenge is ended
        }

        private void OnGameShowTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Round.IsEnded || Round.IsLobby)
            {
                if (SCP079GameShow.Instance.Config.Debug)
                {
                    Log.Debug("Game show timer elapsed, but round is not active. Skipping.");

                }
                return;
            }

            if (GameShowManager.Instance.GetCurrentChallenge() != null)
            {
                if (SCP079GameShow.Instance.Config.Debug)
                {
                    Log.Debug("A challenge is already active. Skipping new challenge.");

                }

                return;
            }

            if (Random.value <= SCP079GameShow.Instance.Config.GameShowChance)
            {
                Log.Info("Time for a game show!");
                GameShowManager.Instance.StartRandomChallenge();
            }
            else
            {
                if (SCP079GameShow.Instance.Config.Debug)
                {
                    Log.Debug("Game show chance failed. Waiting for next interval.");

                }
            }
        }
    }
}