using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SCP079GameShow
{
    public class GameShowManager
    {
        private static GameShowManager _instance;
        public static GameShowManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameShowManager();
                return _instance;
            }
        }

        private List<GameShowChallenge> _availableChallenges = new List<GameShowChallenge>();
        private GameShowChallenge _currentChallenge;
        private Timer _challengeTimer;

        private GameShowManager()
        {
            // Initialize available challenges
            _availableChallenges.Add(new RiddleChallenge());
            _availableChallenges.Add(new ScavengerHuntChallenge());
            _availableChallenges.Add(new ScavengerHuntChallenge.ObstacleCourseChallenge());
        }

        public void StartRandomChallenge()
        {
            if (_currentChallenge != null)
            {
                Log.Warn("A game show challenge is already active!");
                return;
            }

            if (_availableChallenges.Any())
            {
                _currentChallenge = _availableChallenges[UnityEngine.Random.Range(0, _availableChallenges.Count)];
                _currentChallenge.StartChallenge();
                _currentChallenge.RegisterEvents();

                // Set a timer for the challenge duration (e.g., 60 seconds)
                _challengeTimer = new Timer(60 * 1000); 
                _challengeTimer.Elapsed += (sender, e) => EndCurrentChallenge();
                _challengeTimer.AutoReset = false;
                _challengeTimer.Start();

                Log.Info($"Started new challenge: {_currentChallenge.Name}");
            }
            else
            {
                Log.Warn("No game show challenges available to start.");
            }
        }

        public void EndCurrentChallenge()
        {
            if (_currentChallenge != null)
            {
                _currentChallenge.UnregisterEvents();
                _currentChallenge.EndChallenge();
                _currentChallenge = null;
                _challengeTimer?.Stop();
                _challengeTimer?.Dispose();
                _challengeTimer = null;
                Log.Info("Current game show challenge ended.");
            }
        }

        public GameShowChallenge GetCurrentChallenge()
        {
            return _currentChallenge;
        }
    }
}
