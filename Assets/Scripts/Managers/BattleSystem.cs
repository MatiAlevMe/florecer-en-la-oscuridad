using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Managers 
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

    [System.Serializable]
    public class Character 
    {
        public string characterName;
        public int maxHealth;
        public int currentHealth;
        public int attack;
        public int defense;
    }

    public class BattleSystem : MonoBehaviour 
    {
        public BattleState state;

        public Character playerCharacter;
        public Character enemyCharacter;

        public Text dialogueText;
        public Text playerHealthText;
        public Text enemyHealthText;

        public Button attackButton;
        public Button defendButton;

        void Start()
        {
            state = BattleState.START;
            SetupBattle();
        }

        void SetupBattle()
        {
            // Initialize player and enemy characters
            playerCharacter = new Character {
                characterName = "Protagonist", 
                maxHealth = 100, 
                currentHealth = 100, 
                attack = 15, 
                defense = 10
            };

            enemyCharacter = new Character {
                characterName = "Demon", 
                maxHealth = 80, 
                currentHealth = 80, 
                attack = 12, 
                defense = 8
            };

            UpdateHealthUI();
            dialogueText.text = $"A {enemyCharacter.characterName} appears!";

            attackButton.onClick.AddListener(OnAttackButton);
            defendButton.onClick.AddListener(OnDefendButton);

            state = BattleState.PLAYERTURN;
        }

        void UpdateHealthUI()
        {
            playerHealthText.text = $"{playerCharacter.characterName}: {playerCharacter.currentHealth}/{playerCharacter.maxHealth}";
            enemyHealthText.text = $"{enemyCharacter.characterName}: {enemyCharacter.currentHealth}/{enemyCharacter.maxHealth}";
        }

        void OnAttackButton()
        {
            if (state != BattleState.PLAYERTURN) return;

            int damage = CalculateDamage(playerCharacter, enemyCharacter);
            enemyCharacter.currentHealth -= damage;

            dialogueText.text = $"{playerCharacter.characterName} deals {damage} damage!";
            UpdateHealthUI();

            if (enemyCharacter.currentHealth <= 0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else 
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        void OnDefendButton()
        {
            if (state != BattleState.PLAYERTURN) return;

            dialogueText.text = $"{playerCharacter.characterName} prepares to defend!";
            playerCharacter.defense *= 2;

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        IEnumerator EnemyTurn()
        {
            dialogueText.text = $"{enemyCharacter.characterName} attacks!";
            yield return new WaitForSeconds(1f);

            int damage = CalculateDamage(enemyCharacter, playerCharacter);
            playerCharacter.currentHealth -= damage;

            dialogueText.text = $"{enemyCharacter.characterName} deals {damage} damage!";
            UpdateHealthUI();

            if (playerCharacter.currentHealth <= 0)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else 
            {
                state = BattleState.PLAYERTURN;
                playerCharacter.defense = 10; // Reset defense
            }
        }

        int CalculateDamage(Character attacker, Character defender)
        {
            int baseDamage = attacker.attack;
            int damageReduction = defender.defense;
            int finalDamage = Mathf.Max(0, baseDamage - damageReduction);
            return finalDamage;
        }

        void EndBattle()
        {
            switch(state)
            {
                case BattleState.WON:
                    dialogueText.text = "You defeated the enemy!";
                    // Unlock companion logic here
                    break;
                case BattleState.LOST:
                    dialogueText.text = "You were defeated...";
                    // Game over logic here
                    break;
            }
        }
    }
}
