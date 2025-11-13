# 🐍🎲 Snakes & Ladders — C# OOP Console Game  
A fully object-oriented, event-driven implementation of the classic **Snakes & Ladders** game built using **C#**, showcasing clean architecture, encapsulation, inheritance, polymorphism, and robust board generation with exception handling.

---
## 📌 Game Overview

This project recreates the traditional Snakes & Ladders board game using an extensible, maintainable, and fully OOP-driven architecture.

It includes:

✔ Random Snake / Ladder / Gold cell placement  
✔ Dice-based movement  
✔ Player swapping on Gold cells  
✔ Event-driven UI updates  
✔ Auto-regeneration of invalid boards  
✔ SPACE key turn control  
✔ Multi-player support  

---
## 🧠 Architecture Highlights

### ✔️ Clean OOP Principles
- **Abstract `Cell` class** inherited by:
  - `EmptyCell`
  - `TopOrBottomCell`
  - `GoldCell`
- `SnakeLink` and `LadderLink` store **head/tail** or **bottom/top** pairs.
- `GameManager` encapsulates:
  - Turn logic  
  - Dice rolls  
  - Leader tracking  
  - Special cell handling  

---

### ✔️ Event-Driven UI
Events:
- `OnTurnStarted`
- `OnRollDice`
- `OnTurnFinished`
- `OnGameOver`

The Console UI **subscribes to events** and updates the game state visually.

---
## 🗂 Project Structure
```
Ladders_and_snakes_game/
│
├── Program.cs
│
├── Core/
│   ├── Board.cs
│   ├── Cell.cs
│   ├── EnumCellType.cs
│   ├── EmptyCell.cs
│   ├── GoldCell.cs
│   ├── LadderLink.cs
│   ├── SnakeLink.cs
│   ├── TopOrBottomCell.cs
│   └── BoardInitializationException.cs
│
├── Factory/
│   └── CellsFactory.cs
│
├── Game_Logic/
│   ├── GameManager.cs
│   └── Dice.cs
│
├── Players/
│   ├── IPlayer.cs
│   └── Player.cs
│
├── Front/
│   ├── UserInterface.cs
│   └── UserInputValidation.cs
│
├── Utilities/
│   └── RandomProvider.cs
│
└── Configuration/
    └── GameSettings.cs
```

## 📸 Gameplay Screenshots (Step-by-Step)

Below is a complete walkthrough of the game flow with real console screenshots.

---

### 🧩 1. Game Starts — User Chooses Number of Snakes
<img width="696" height="560" alt="Screenshot 2025-11-13 193602" src="https://github.com/user-attachments/assets/c5ac6897-57a7-4bc5-b514-ac8daefdef5e" />

The game begins by asking the player to enter how many **Snakes** should be placed on the board.  
Input validation ensures the user enters a number within the allowed range.

---

### 🪜 2. User Chooses Number of Ladders
<img width="634" height="561" alt="Screenshot 2025-11-13 193615" src="https://github.com/user-attachments/assets/9186815b-2d16-4b58-85fd-067c2a36faf1" />

Next, the player is asked to enter how many **Ladders** to generate.  
The UI validates this input as well.

---

### 🎲 3. First Board Rendering — Game Begins


The board is generated randomly:  
- `SH` = Snake Head  
- `ST` = Snake Tail  
- `LB` = Ladder Bottom  
- `LT` = Ladder Top  
- `G`  = Gold Cell  
- `P1`, `P2` = Player positions  

<img width="731" height="486" alt="Screenshot 2025-11-13 203946" src="https://github.com/user-attachments/assets/d9024192-14b3-4926-aefb-254a35a75d7f" />

Player 1 rolls the dice and the game starts.

---

### 🧍‍♂️➡️ 4. Player Makes the First Move
<img width="644" height="551" alt="Screenshot 2025-11-13 193632" src="https://github.com/user-attachments/assets/37a88ad2-81eb-4eb7-b612-ced2487f4103" />

Player 1 rolled a **3** and moved from **0 → 3**.  
Now it's Player 2’s turn.  
The UI prompts Player 2 to press **SPACE** to roll the dice.

---

### 🎮 5. Endgame Scenario — Player is Near Winning
<img width="656" height="556" alt="Screenshot 2025-11-13 193746" src="https://github.com/user-attachments/assets/401d31ae-42aa-47ad-8d26-0319741c77c5" />

Here Player 1 rolled an **8** and moved from **88 → 96**.  
Player 2 is prompted for their turn, but the win is close.

---

### 🏁 6. Player Wins the Game
<img width="656" height="556" alt="Screenshot 2025-11-13 193746" src="https://github.com/user-attachments/assets/d39aeceb-8d63-4270-a6fa-cee4063115de" />

Player 1 reaches the final cell (100) and wins:  
- Dice rolled: 8  
- Move: **96 → 100**  
- UI displays **"Game Over! Congrats! Player 1 Won!"**

---

### 🔁 7. Play Again or Exit
<img width="643" height="561" alt="Screenshot 2025-11-13 193757" src="https://github.com/user-attachments/assets/e43f0f89-a33d-445b-b2ed-3b470aa8ed4e" />

The player can choose:  
- Press **Y** → restart the game  
- Press any other key → exit  

## 🎉 End of Game


This concludes a full gameplay cycle.
