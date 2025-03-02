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

        // New companion follow parameters
        public float companionFollowDistance = 1.5f;
        public float companionAdviceInterval = 10f;
        private float lastAdviceTime;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            lastAdviceTime = Time.time;
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

            // Companion follow logic when not player-controlled
            if (!isPlayerControl)
            {
                FollowPlayer();
            }
        }

        void FollowPlayer()
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)companionTransform.position;
            if (direction.magnitude > companionFollowDistance)
            {
                direction = direction.normalized;
                companionTransform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            }
        }

        public void SwitchControl()
        {
            isPlayerControl = !isPlayerControl;
        }
    }
}
