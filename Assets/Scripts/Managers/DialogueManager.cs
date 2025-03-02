using UnityEngine;                                                                                                                                      
 using UnityEngine.UI;                                                                                                                                   
                                                                                                                                                         
 namespace Managers                                                                                                                                      
 {                                                                                                                                                       
     [System.Serializable]                                                                                                                               
     public class DialogueSet                                                                                                                            
     {                                                                                                                                                   
         public string[] dialogueLines;                                                                                                                  
         public Button continueButton;                                                                                                                   
     }                                                                                                                                                   
                                                                                                                                                         
     public class DialogueManager : MonoBehaviour                                                                                                        
     {                                                                                                                                                   
         public Text dialogueText;                                                                                                                       
         public DialogueSet[] dialogues;                                                                                                                 
         private int currentSet = 0;                                                                                                                     
         private int currentLine = 0;                                                                                                                    
                                                                                                                                                         
         void Start()                                                                                                                                    
         {                                                                                                                                               
             UpdateDialogue();                                                                                                                           
             foreach (var dialogue in dialogues)                                                                                                         
             {                                                                                                                                           
                 if (dialogue.continueButton != null)                                                                                                    
                 {                                                                                                                                       
                     dialogue.continueButton.onClick.AddListener(NextLine);                                                                              
                 }                                                                                                                                       
             }                                                                                                                                           
         }                                                                                                                                               
                                                                                                                                                         
         void UpdateDialogue()                                                                                                                           
         {                                                                                                                                               
             if (currentLine < dialogues[currentSet].dialogueLines.Length)                                                                               
             {                                                                                                                                           
                 dialogueText.text = dialogues[currentSet].dialogueLines[currentLine];                                                                   
             }                                                                                                                                           
         }                                                                                                                                               
                                                                                                                                                         
         void NextLine()                                                                                                                                 
         {                                                                                                                                               
             if (currentLine < dialogues[currentSet].dialogueLines.Length - 1)                                                                           
             {                                                                                                                                           
                 currentLine++;                                                                                                                          
                 UpdateDialogue();                                                                                                                       
             }                                                                                                                                           
             else                                                                                                                                        
             {                                                                                                                                           
                 // Si terminamos una conversación, pasamos a la siguiente                                                                               
                 if (currentSet < dialogues.Length - 1)                                                                                                  
                 {                                                                                                                                       
                     currentSet++;                                                                                                                       
                     currentLine = 0;                                                                                                                    
                     UpdateDialogue();                                                                                                                   
                 }                                                                                                                                       
             }                                                                                                                                           
         }                                                                                                                                               
     }                                                                                                                                                   
 }            