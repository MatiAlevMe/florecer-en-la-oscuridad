using UnityEngine;                                                                                                                                      
                                                                                                                                                         
namespace Controllers                                                                                                                                   
{                                                                                                                                                       
    public class PlayerMovement : MonoBehaviour                                                                                                         
    {                                                                                                                                                   
        public float speed = 5f;
        private Rigidbody2D rb;                                                                                                                         
        private Animator animator;                                                                                                                      
        public Transform companionTransform;                                                                                                            
        private bool isPlayerControl = true;

        void Start()                                                                                                                                    
        {                                                                                                                                               
            rb = GetComponent<Rigidbody2D>();                                                                                                           
            animator = GetComponent<Animator>();                                                                                                        
        }                                                                                                                                               
                                                                                                                                                         
        void Update()                                                                                                                                   
        {                                                                                                                                               
            if (isPlayerControl)                                                                                                                        
            {                                                                                                                                           
                // Player movement                                                                                                                               
                float horizontal = Input.GetAxisRaw("Horizontal");                                                                                         
                float vertical = Input.GetAxisRaw("Vertical");                                                                                             
                                                                                                                                                         
                Vector2 movement = new Vector2(horizontal, vertical).normalized;                                                                   
                                                                                                                                                         
                rb.MovePosition(rb.position + movement * speed * Time.deltaTime);                                                                       
                animator.SetFloat("Horizontal", horizontal);                                                                                            
                animator.SetFloat("Vertical", vertical);
                animator.SetFloat("Speed", movement.magnitude);
            }                                                                                                                                           
            else                                                                                                                                        
            {                                                                                                                                           
                // Companion movement                                                                                                             
                float horizontal = Input.GetAxisRaw("Horizontal");                                                                                         
                float vertical = Input.GetAxisRaw("Vertical");                                                                                             
                                                                                                                                                         
                Vector2 movement = new Vector2(horizontal, vertical).normalized;
                companionTransform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;                                           
            }                                                                                                                                           
        }                                                                                                                                               
                                                                                                                                                         
        public void SwitchControl()                                                                                                                     
        {                                                                                                                                               
            isPlayerControl = !isPlayerControl;                                                                                                         
        }                                                                                                                                               
    }                                                                                                                                                   
}
