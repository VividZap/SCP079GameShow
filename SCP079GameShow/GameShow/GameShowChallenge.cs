using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using System;
using Exiled.API.Enums;
using Exiled.CustomItems.API.EventArgs;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using Exiled.Events.EventArgs.Scp914;

namespace SCP079GameShow
{
    // Basic API for start Challenge in Server 
    public abstract class GameShowChallenge
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract void StartChallenge();
        public abstract void EndChallenge();
        public abstract void AnnounceChallenge();
        public abstract void RegisterEvents();
        public abstract void UnregisterEvents();
    }

    public class RiddleChallenge : GameShowChallenge
    {
        public override string Name => "Riddle Challenge";
        public override string Description => "SCP-079 will broadcast a riddle. The first player to enter the correct room wins!";

        private Dictionary<string, string> _riddles = new Dictionary<string, string>()
        {
            { "I have cities, but no houses; forests, but no trees; and water, but no fish. What am I?", "Map" },
            { "What has an eye, but cannot see?", "Needle" },
            { "What is full of holes but still holds water?", "Sponge" }
        };

        private string _currentRiddle;
        private string _currentAnswerRoomName; // The room name that is the answer
        private List<Player> _participants = new List<Player>();

        public override void AnnounceChallenge()
        {
            Cassie.Message("Attention all personnel. SCP 0 7 9 has initiated a game show. Prepare for a riddle challenge.", isHeld: false, isNoisy: false, isSubtitles: true);
        }

        public override void StartChallenge()
        {
            var randomRiddle = _riddles.ElementAt(UnityEngine.Random.Range(0, _riddles.Count));
            _currentRiddle = randomRiddle.Key;
            _currentAnswerRoomName = randomRiddle.Value; // Assuming the answer is a room name

            AnnounceChallenge();
            Cassie.Message(_currentRiddle, isNoisy: false, isHeld: false, isSubtitles: true);
            Log.Info($"Riddle Challenge started: {_currentRiddle} (Answer Room: {_currentAnswerRoomName})");

            _participants = Player.List.Where(p => p.IsHuman).ToList(); // Only human players participate
            RegisterEvents();
        }

        public override void EndChallenge()
        {
            Cassie.Message("The riddle challenge has ended.", isHeld: false, isNoisy: false, isSubtitles: true);
            Log.Info("Riddle Challenge ended.");
            UnregisterEvents();
        }

        public override void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.RoomChanged += OnPlayerChangingRoom;
        }

        public override void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.RoomChanged -= OnPlayerChangingRoom;
        }

        private void OnPlayerChangingRoom(RoomChangedEventArgs ev)
        {
            if (_participants.Contains(ev.Player) && ev.NewRoom.Name.Equals(_currentAnswerRoomName, StringComparison.OrdinalIgnoreCase))
            {
                Log.Info($"Player {ev.Player.Nickname} entered the correct room: {ev.NewRoom.Name}!");
                Cassie.Message($"Correct. Player {ev.Player.Nickname} is the winner.", isNoisy: false, isHeld: false, isSubtitles: true);
                RewardsAndPenalties.GrantReward(ev.Player);
                GameShowManager.Instance.EndCurrentChallenge(); // End the challenge
            }
        }
    }

    public class ScavengerHuntChallenge : GameShowChallenge
    {
        public override string Name => "Scavenger Hunt";
        public override string Description => "Find a specific item and bring it to SCP-914!";

        private ItemType _targetItem;
        private List<Player> _participants = new List<Player>();

        public override void AnnounceChallenge()
        {
            Cassie.Message("Attention all personnel. SCP 0 7 9 has initiated a game show. Prepare for a scavenger hunt.", isHeld: false, isNoisy: false, isSubtitles: true);
        }

        public override void StartChallenge()
        {
            // Randomly select a common item to find
            ItemType[] commonItems = { ItemType.Medkit, ItemType.Radio, ItemType.Flashlight, ItemType.KeycardJanitor };
            _targetItem = commonItems[UnityEngine.Random.Range(0, commonItems.Length)];

            AnnounceChallenge();
            Cassie.Message($"Find a {_targetItem.ToString().Replace("_", " ")} and bring it to SCP 9 1 4.", isHeld: false, isNoisy: false, isSubtitles: true);
            Log.Info($"Scavenger Hunt started. Target item: {_targetItem}");

            _participants = Player.List.Where(p => p.IsHuman).ToList();
            RegisterEvents();
        }

        public override void EndChallenge()
        {
            Cassie.Message("The scavenger hunt has ended.", isHeld: false, isNoisy: false, isSubtitles: true);
            Log.Info("Scavenger Hunt ended.");
            UnregisterEvents();
        }

        public override void RegisterEvents()
        {
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += OnUpgradingInventoryItem;
        }

        public override void UnregisterEvents()
        {
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem -= OnUpgradingInventoryItem;
        }

        private void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (_participants.Contains(ev.Player) && ev.Item.Type == _targetItem)
            {
                Log.Info($"Player {ev.Player.Nickname} brought the correct item ({_targetItem}) to SCP-914!");
                Cassie.Message($"Excellent. Player {ev.Player.Nickname} is the winner.", isHeld: false, isNoisy: false, isSubtitles: true);
                RewardsAndPenalties.GrantReward(ev.Player);
                GameShowManager.Instance.EndCurrentChallenge(); // End the challenge
            }
            
        }
    public class ObstacleCourseChallenge : GameShowChallenge
    {
        public override string Name => "Obstacle Course";
        public override string Description => "Navigate a hazardous path to reach the goal!";

        private Room _startRoom;
        private Room _endRoom;
        private List<Player> _participants = new List<Player>();

        public override void AnnounceChallenge()
        {
            Cassie.Message("Attention all personnel. SCP 0 7 9 has initiated a game show. Prepare for an obstacle course.", isHeld: false, isNoisy: false, isSubtitles: true);
        }

        public override void StartChallenge()
        {
            // Select random start and end rooms that are not too close
            List<Room> allRooms = Room.List.Where(r => r.Type != RoomType.Pocket && r.Type != RoomType.Surface).ToList();
            _startRoom = allRooms[UnityEngine.Random.Range(0, allRooms.Count)];
            _endRoom = allRooms[UnityEngine.Random.Range(0, allRooms.Count)];

            // Ensure start and end rooms are different and reasonably far apart
            while (_startRoom == _endRoom || Vector3.Distance(_startRoom.Position, _endRoom.Position) < 30f)
            {
                _endRoom = allRooms[UnityEngine.Random.Range(0, allRooms.Count)];
            }

            AnnounceChallenge();
            Cassie.Message($"Navigate from {_startRoom.Name.ToString().Replace("_", " ")} to {_endRoom.Name.ToString().Replace("_", " ")} .", isHeld: false, isNoisy: false, isSubtitles: true);
            Log.Info($"Obstacle Course started: From {_startRoom.Name} to {_endRoom.Name}");

            _participants = Player.List.Where(p => p.IsHuman).ToList();
            RegisterEvents();

            // Optionally, add some hazards or effects along the path
            // For example, temporarily disable some doors, or add temporary environmental hazards
        }

        public override void EndChallenge()
        {
            Cassie.Message("The obstacle course has ended.", isHeld: false, isNoisy: false, isSubtitles: true);
            Log.Info("Obstacle Course ended.");
            UnregisterEvents();
        }

        public override void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.RoomChanged += OnPlayerChangingRoom;
        }

        public override void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.RoomChanged -= OnPlayerChangingRoom;
        }

        private void OnPlayerChangingRoom(RoomChangedEventArgs ev)
        {
            if (_participants.Contains(ev.Player) && ev.NewRoom == _endRoom)
            {
                Log.Info($"Player {ev.Player.Nickname} reached the end of the obstacle course in room: {ev.NewRoom.Name}!");
                Cassie.Message($"Congratulations. Player {ev.Player.Nickname} has completed the obstacle course.", isHeld: false, isNoisy: false, isSubtitles: true);
                RewardsAndPenalties.GrantReward(ev.Player);
                GameShowManager.Instance.EndCurrentChallenge(); // End the challenge
            }
        }
    } 
  }  
} 

    

