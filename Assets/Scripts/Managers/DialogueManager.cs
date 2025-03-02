using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public Text dialogueText;
        public Button continueButton;
        public string[] dialogueLines;
        private int currentLine = 0;

        void Start()
        {
            UpdateDialogue();
            continueButton.onClick.AddListener(NextLine);
        }

        void UpdateDialogue()
        {
            dialogueText.text = dialogueLines[currentLine];
        }

        void NextLine()
        {
            if (currentLine < dialogueLines.Length - 1)
            {
                currentLine++;
                UpdateDialogue();
            }
        }
    }
}
