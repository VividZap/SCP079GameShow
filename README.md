# Plugin: SCP-079 Game Show

## Overview

This plugin introduces a dynamic, interactive game show hosted by SCP-079 (or an AI announcer) during rounds, adding a layer of unpredictable fun and strategic decision-making for both human and SCP players. The game show would present players with various challenges, riddles, or mini-games, offering rewards for success and penalties for failure. This concept leverages existing game mechanics and the unique role of SCP-079 to create engaging, emergent gameplay scenarios.

## Uniqueness and Innovation

Existing plugins often focus on administrative tools, custom items, or minor game tweaks. 'SCP-079's Game Show' is unique because it:

*   **Integrates directly with SCP-079:** It gives SCP-079 a more active, engaging role beyond just supporting other SCPs, allowing it to directly influence the round in entertaining ways.
*   **Creates emergent narratives:** Each game show event can lead to unexpected player interactions, alliances, or betrayals, fostering unique stories within each round.
*   **Promotes player interaction:** Challenges can require cooperation or competition among players, encouraging communication and strategic thinking.
*   **Adds replayability:** With a variety of mini-games and challenges, no two rounds will feel exactly the same.
*   **Lightweight by design:** The core logic relies on existing EXILED events and features (player location, item usage, communication), minimizing the need for complex new assets or heavy computations.

## Core Functionality

1.  **Event Triggering:**
    *   **Randomly:** The game show can start at random intervals during a round (e.g., every 5-10 minutes).
    *   **Condition-based:** Triggered by specific in-game events (e.g., first SCP recontained, warhead activated, certain number of players remaining).
    *   **SCP-079 initiated:** SCP-079 players could have a command to initiate a game show event, perhaps with a cooldown.

2.  **Challenge Types (Examples):**
    *   **Riddle Challenge:** SCP-079 broadcasts a riddle over the intercom. The first player to type the correct answer in chat (or perform a specific action) wins.
    *   **Scavenger Hunt:** Players are tasked with finding a specific, randomly spawned item within a time limit. The first to bring it to a designated location (e.g., SCP-914, a specific room) wins.
    *   **


Obstacle Course:** Players must navigate a section of the map (e.g., a specific hallway, a series of rooms) under certain conditions (e.g., lights off, doors randomly locking/unlocking, temporary debuffs). First to reach the end wins.
    *   **


Team Challenge:** Two or more players (or teams) are given a cooperative or competitive task, such as escorting an NPC, defending a location, or racing to activate generators.
    *   **Trivia:** SCP-079 asks multiple-choice questions about SCP lore or game mechanics. Players answer by interacting with specific objects or typing numbers.

3.  **Rewards and Penalties:**
    *   **Rewards (for winners):**
        *   Temporary buffs (e.g., increased health, speed, damage).
        *   Access to rare items or weapons.
        *   Temporary invulnerability.
        *   Teleportation to a safe zone or strategic location.
        *   Currency (if a server economy plugin is present).
        *   Positive status effects (e.g., healing, stamina boost).
    *   **Penalties (for losers or those who fail challenges):**
        *   Temporary debuffs (e.g., reduced speed, blurred vision, disarmed).
        *   Teleportation to a dangerous area (e.g., Pocket Dimension, SCP-173 containment chamber).
        *   Minor damage or health reduction.
        *   Temporary loss of certain abilities (e.g., cannot open doors, cannot use items).
        *   Negative status effects (e.g., bleeding, poison).

4.  **Announcer and Communication:**
    *   SCP-079 (or a designated AI) would use the in-game CASSIE system for announcements, instructions, and commentary during the game show.
    *   Custom audio files could be used for specific phrases or sound effects to enhance the game show atmosphere.
    *   Text-based messages (hints, scores) could be displayed via the Hint system.

## Technical Considerations (Lightweight Design)

*   **Leverage Existing Events:** The plugin would primarily hook into existing EXILED events for player actions (e.g., `Player.InteractingDoor`, `Player.PickingUpItem`, `Player.ChangingRole`, `Player.Died`, `Player.EnteringPocketDimension`, `Player.Escaping`, `Player.Spawning`).
*   **Minimal Custom Assets:** Avoids custom models, textures, or complex animations. Relies on existing in-game objects and effects.
*   **Configuration:** All game show parameters (frequency, challenge types, rewards, penalties, messages) would be highly configurable via a YAML file, allowing server owners to customize the experience.
*   **State Management:** Simple in-memory state management for ongoing challenges and player participation. Data can be reset at the end of each round.
*   **No External Dependencies:** Designed to run solely on the EXILED framework without requiring additional external libraries or databases.
*   **Dynamic Object Manipulation:** Utilizes EXILED's capabilities to dynamically spawn/despawn items, modify player stats, and control doors/lights for challenges.

## Potential Impact on Gameplay

*   **Increased Engagement:** Players will be more attentive and involved, anticipating the next game show event.
*   **Strategic Depth:** Challenges can force players to adapt their strategies and interact with the environment and other players in new ways.
*   **Roleplay Opportunities:** Encourages roleplay, especially for SCP-079 players who get to act as the game show host.
*   **Community Building:** Creates memorable moments and shared experiences that players will discuss and remember.
*   **Server Differentiation:** Offers a unique selling point for servers that implement this plugin, attracting players looking for novel gameplay.

This plugin aims to be lightweight by focusing on intelligent use of existing EXILED features and events, rather than introducing heavy new systems. Its primary goal is to enhance the fun and unpredictability of SCP: Secret Laboratory rounds, making each playthrough a unique and entertaining experience.

