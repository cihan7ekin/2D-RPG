# ⚔️ - 2D Action RPG
![Unity Version] -<Unity 6.2>-
![C#]

2D-RPG is a 2D action RPG developed in Unity, featuring fluid combat mechanics and an extensible skill tree. The primary goal of this project is not just to create a working game, but to engineer a **Scalable, Clean, and Modular** software architecture.

## 🛠️ Technical Architecture & Highlights

Instead of writing monolithic "spaghetti code", this project heavily relies on industry-standard Design Patterns and SOLID principles to ensure maintainability and scalability.

### 1. Hierarchical Finite State Machine (HFSM)
Character and enemy AI logic are not crammed into a single `Update` loop. Instead, they are managed by a robust HFSM.
* `Player` and `Enemy` classes inherit from a core `Entity` base class.
* Behaviors are isolated into specific state classes (e.g., `GroundedState`, `MoveState`, `AttackState`).
* Adding new movements or attacks is as simple as creating a new State class without modifying existing core logic.

### 2. Modular Core Skill System
Skills (Dash, Clone, Fireball, etc.) are not hardcoded. The system utilizes `Template Method` and `Strategy` patterns.
* **`Skill_Base`**: An abstract parent class handling core logic like cooldowns and execution requirements.
* **`SkillManager`**: Acts as a centralized hub to manage and execute player abilities, adhering to dependency injection principles.

### 3. Data-Driven Design
In-game data (skill unlock requirements, stats, upgrades) are separated from the logic using **ScriptableObjects**.
* **`Skill_DataSO`**: Stores data such as `SkillType` and `UpgradeType` (Enums).
* This allows Game Designers to create and tweak new abilities directly in the Unity Editor without touching a single line of code.

### 4. Event-Driven UI & Decoupling
* UI systems and Backend logic are completely decoupled.
* Cross-system communication is handled via `C# Actions/Events` (Observer Pattern), such as `OnPlayerDeath`.
* UI components dynamically update themselves by fetching data objects (utilizing a Factory Pattern approach via `GetSkillByType`).

## 🎮 Gameplay Features

* **Fluid Character Controller:** Physics-based (Rigidbody2D) movement and jumping, fully synchronized with animations.
* **Dynamic Combat System:** Polymorphic hit registration, knockback mechanics, and status effects (e.g., Slow Down).
* **Interconnected Skill Tree:** A complex, node-based unlock system where acquiring one skill dynamically affects the availability of others.
* **Dash & Mirage Mechanics:** Advanced evasion mechanics that leave behind damage-dealing clones.

## 📁 Project Structure

The project is highly modularized, ensuring separation of concerns and easy navigation for team collaboration:

```text
📦 Assets
 ┣ 📂 Scripts
 ┃ ┣ 📂 Enemy              # Enemy AI, behaviors, and specific enemy states
 ┃ ┣ 📂 Entity             # Base classes for all living objects (Health, Knockback, etc.)
 ┃ ┣ 📂 Enum               # Global Enums used across the project (SkillType, UpgradeType)
 ┃ ┣ 📂 InteractiveObjects # World objects the player can interact with
 ┃ ┣ 📂 Interface          # C# Interfaces defining contracts (IDamageable, etc.)
 ┃ ┣ 📂 Parallax           # Background scrolling and visual depth controllers
 ┃ ┣ 📂 Player             # Player controller, input handling, and player states
 ┃ ┣ 📂 SkillSystem        # Modular skill logic (Skill_Base, SkillManager, DataSO)
 ┃ ┣ 📂 StateMachine       # Core HFSM logic (StateMachine, EntityState base classes)
 ┃ ┣ 📂 StatsSystem        # RPG stats calculation, modifiers, and attributes
 ┃ ┣ 📂 UI                 # User interface logic, Skill Tree nodes, and HUD
 ┃ ┗ 📂 VFX                # Visual effects, particle managers, and screen shakes
 ┣ 📂 ScriptableObjects    # Data containers for Skills, Items, etc.
 ┣ 📂 Prefabs              # Preconfigured game objects
 ┗ 📂 Animations           # Animation clips and Animator Controllers


 This project was built from scratch to improve my software architecture and OOP skills, inspired by advanced Unity development courses (e.g., Alex Dev).