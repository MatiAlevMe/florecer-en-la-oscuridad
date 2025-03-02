                                                                                                                                                        
 using UnityEngine;                                                                                                                                      
                                                                                                                                                         
 namespace Components                                                                                                                                    
 {                                                                                                                                                       
     public class RacingMinigame : MonoBehaviour                                                                                                         
     {                                                                                                                                                   
         public Transform playerCar;                                                                                                                     
         public float speed = 10f;                                                                                                                       
         public float jumpForce = 5f;                                                                                                                    
         public Transform finishLine;                                                                                                                    
         private bool isRacing = false;                                                                                                                  
                                                                                                                                                         
         void Update()                                                                                                                                   
         {                                                                                                                                               
             if (isRacing)                                                                                                                               
             {                                                                                                                                           
                 float horizontal = Input.GetAxis("Horizontal");                                                                                         
                 playerCar.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;                                                           
                                                                                                                                                         
                 if (Input.GetButtonDown("Jump") && IsGrounded())                                                                                        
                 {                                                                                                                                       
                     playerCar.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);                                    
                 }                                                                                                                                       
                                                                                                                                                         
                 if (IsNearFinishLine())                                                                                                                 
                 {                                                                                                                                       
                     WinRace();                                                                                                                          
                 }                                                                                                                                       
             }                                                                                                                                           
         }                                                                                                                                               
                                                                                                                                                         
         void StartRace()                                                                                                                                
         {                                                                                                                                               
             isRacing = true;                                                                                                                            
             playerCar.position = new Vector3(-10, 0, 0);                                                                                                
         }                                                                                                                                               
                                                                                                                                                         
         void WinRace()                                                                                                                                  
         {                                                                                                                                               
             isRacing = false;                                                                                                                           
             // Añade lógica para ganar la carrera                                                                                                       
             Debug.Log("Victoria!");                                                                                                                     
         }                                                                                                                                               
                                                                                                                                                         
         bool IsGrounded()                                                                                                                               
         {                                                                                                                                               
             return playerCar.position.y <= 0.1f;                                                                                                        
         }                                                                                                                                               
                                                                                                                                                         
         bool IsNearFinishLine()                                                                                                                         
         {                                                                                                                                               
             return Vector3.Distance(playerCar.position, finishLine.position) < 1f;                                                                      
         }                                                                                                                                               
     }                                                                                                                                                   
 }   