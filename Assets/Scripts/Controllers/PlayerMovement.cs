using UnityEngine;                                                                                                                                      
                                                                                                                                                         
 namespace Controllers                                                                                                                                   
 {                                                                                                                                                       
     public class PlayerMovement : MonoBehaviour                                                                                                         
     {                                                                                                                                                   
         public float speed = 5f;                                                                                                                        
         private Rigidbody2D rb;                                                                                                                         
         private Animator animator;                                                                                                                      
         public Transform companionTransform;                                                                                                            
         private bool isPlayerControl = true; // Bandera para alternar entre jugador y compañero                                                         
                                                                                                                                                         
         void Start()                                                                                                                                    
         {                                                                                                                                               
             rb = GetComponent<Rigidbody2D>();                                                                                                           
             animator = GetComponent<Animator>();                                                                                                        
         }                                                                                                                                               
                                                                                                                                                         
         void Update()                                                                                                                                   
         {                                                                                                                                               
             if (isPlayerControl)                                                                                                                        
             {                                                                                                                                           
                 // Movimiento del jugador                                                                                                               
                 float horizontal = Input.GetAxis("Horizontal");                                                                                         
                 float vertical = Input.GetAxis("Vertical");                                                                                             
                                                                                                                                                         
                 Vector2 movement = new Vector2(horizontal, vertical);                                                                                   
                                                                                                                                                         
                 rb.MovePosition(rb.position + movement * speed * Time.deltaTime);                                                                       
                 animator.SetFloat("Horizontal", horizontal);                                                                                            
                 animator.SetFloat("Vertical", vertical);                                                                                                
             }                                                                                                                                           
             else                                                                                                                                        
             {                                                                                                                                           
                 // Movimiento del compañero                                                                                                             
                 float horizontal = Input.GetAxis("Horizontal");                                                                                         
                 float vertical = Input.GetAxis("Vertical");                                                                                             
                                                                                                                                                         
                 companionTransform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;                                           
             }                                                                                                                                           
         }                                                                                                                                               
                                                                                                                                                         
         public void SwitchControl()                                                                                                                     
         {                                                                                                                                               
             isPlayerControl = !isPlayerControl;                                                                                                         
         }                                                                                                                                               
     }                                                                                                                                                   
 }  