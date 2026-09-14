# Yes Chef! 

This repository contains the "Yes Chef!" 3D kitchen prototype, developed as a technical assessment using Unity and C#. The project focuses on clean, modular architecture, utilizing native Unity systems without relying on external plugins.

## Project Overview

"Yes Chef!" is a fast-paced, top-down 3D cooking game where a single player manages a kitchen, prepares ingredients, and fulfills randomized customer orders before a 3-minute timer expires. 

**Engine:** Unity 6000.3.23f1
**Language:** C#
**Perspective:** 3D Top-Down 

## Core Architecture

The codebase is structured to prioritize scalability and maintainability, acting as a robust foundation for a full-fledged game.

*   **Interface-Driven Interactions:** All interactive kitchen elements (Refrigerator, Stove, Table, Trash, Windows) implement the `InteractableStation` interface. This heavily decouples the `PlayerInteraction` logic from the specific station behaviors. The player simply casts an overlap sphere and calls `.Interact()`, allowing for easy addition of new station types in the future.
*   **Manager Singletons:** Game state is centralized in lightweight singletons.
    *   `GameManager`: Handles the overarching 3-minute timer, tracks the score, manages the core gameplay state, and persists the High Score using `PlayerPrefs`.
    *   `UIManager`: Controls canvas transitions, pause functionality, and core HUD updates.
    *   `OrderManager`: Encapsulates the logic for generating randomized 2-3 ingredient orders and storing base score values.
*   **Asynchronous Timers:** Native Unity `Coroutines` handle the preparation timers for the Table (2 seconds) and the Stove slots (6 seconds). This ensures non-blocking time management, allowing the player to freely navigate the kitchen while ingredients are processing.
*   **Physics-Based Movement:** The `PlayerController` utilizes `Rigidbody.MovePosition` for smooth, collision-aware movement that prevents clipping through the station primitives.

## Gameplay & Design Decisions

While strictly adhering to the provided design document, several specific implementation decisions were made to improve playability and modularity:

*   **Single-Source Refrigerator:** Instead of requiring three separate refrigerators for Vegetables, Meat, and Cheese, the Refrigerator script was designed to cycle through the available raw ingredients upon repeated interactions. This simplifies the physical kitchen layout and reduces player travel time.
*   **Modular Stove Slots:** The Stove requirement (2 slots processing meat over 6 seconds) was implemented by creating a discrete `StoveSlot.cs` script attached to two distinct child box colliders on the main Stove object. This avoids complex array management within a single script and allows for easily expanding the stove to 4 or 6 slots just by duplicating GameObjects.
*   **Dynamic Order Scoring:** Order score calculation dynamically subtracts the time elapsed (rounded down) from the combined base value of the required ingredients. If an order takes too long, it can result in a negative score, heavily encouraging efficient time management and prioritization.
*   **Visible Hand State:** Instead of a complex UI inventory system, a 3D TextMeshPro object is parented to the player, dynamically updating to visually represent the `heldIngredient` string (e.g., displaying " ", "Vegetable", or "Meat"). This provides immediate, clear feedback without requiring custom 3D models.

## Controls

*   **WASD or Arrow Keys:** Move the Chef
*   **E:** Interact with stations (Pick up, Place down, Chop, Throw away)

## Installation & Setup

1. Clone the repository.
2. Open the project using Unity version 6000+.
3. Open the main Scene.
4. Ensure the TextMeshPro package is imported (you may be prompted upon opening).
5. Press Play in the editor to test the prototype.
