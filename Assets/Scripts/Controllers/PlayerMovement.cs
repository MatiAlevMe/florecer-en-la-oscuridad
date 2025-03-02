using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class PlayerMovement : MonoBehaviour
    {
        // Movement and dash parameters
        public float speed = 5f;
        public float dashSpeed = 10f;
        public float dashDuration = 0.5f;

        // Component references
        private Rigidbody2D rb;
        private Animator animator;

        // Companion-related variables
        public Transform companionTransform;
        public float companionFollowDistance = 1.5f;
        public float companionAdviceInterval = 10f;

        // Control and state variables
        private float dashTimer;
        private bool isPlayerControl = true;
        private bool isDashing = false;
        private float lastAdviceTime;

        // Input axis configuration
        private string horizontalAxis;
        private string verticalAxis;

        void Start()
        {
            // Get required components
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            // Initialize advice timer
            lastAdviceTime = Time.time;

            // Set default input axes
            SetDefaultInputAxes();
        }

        void SetDefaultInputAxes()
        {
            // Toggle between player and companion input axes
            horizontalAxis = isPlayerControl ? "Horizontal" : "JIKLHorizontal";
            verticalAxis = isPlayerControl ? "Vertical" : "JIKLVertical";
        }

        void Update()
        {
            // Get input axes
            float horizontal = Input.GetAxisRaw(horizontalAxis);
            float vertical = Input.GetAxisRaw(verticalAxis);

            // Calculate movement vector
            Vector2 movement = new Vector2(horizontal, vertical).normalized;

            // Handle dash input
            if (Input.GetButtonDown("Dash") && !isDashing)
            {
                StartDash(movement);
            }

            // Manage dash duration
            if (isDashing)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    StopDash();
                }
            }

            // Move character
            Vector2 movePosition = rb.position + movement * (isDashing ? dashSpeed : speed) * Time.deltaTime;
            rb.MovePosition(movePosition);

            // Update animator parameters
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", movement.magnitude);
            animator.SetBool("IsDashing", isDashing);

            // Companion logic when not player-controlled
            if (!isPlayerControl)
            {
                FollowPlayer();
                CheckCompanionAdvice();
            }
        }

        void StartDash(Vector2 direction)
        {
            isDashing = true;
            dashTimer = dashDuration;
            animator.SetBool("IsDashing", true);
        }

        void StopDash()
        {
            isDashing = false;
            animator.SetBool("IsDashing", false);
        }

        void FollowPlayer()
        {
            // Calculate direction to player
            Vector2 direction = (Vector2)transform.position - (Vector2)companionTransform.position;
            
            // Move companion if too far from player
            if (direction.magnitude > companionFollowDistance)
            {
                direction = direction.normalized;
                companionTransform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            }
        }

        void CheckCompanionAdvice()
        {
            // Give advice at specified intervals
            if (Time.time - lastAdviceTime >= companionAdviceInterval)
            {
                GiveCompanionAdvice();
                lastAdviceTime = Time.time;
            }
        }

        void GiveCompanionAdvice()
        {
            // Placeholder for companion advice system
            Debug.Log("Companion advice: Keep going, you're doing great!");
        }

        public void SwitchControl()
        {
            // Toggle control between player and companion
            isPlayerControl = !isPlayerControl;
            SetDefaultInputAxes();
        }

        public void IlluminateMap(Tilemap map, TileBase[] groundTiles, TileBase illuminatedGroundTile)
        {
            // Get player position in Tilemap coordinates
            Vector3Int cellPosition = map.WorldToCell(transform.position);

            // Illuminate tiles around the player
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int neighborPosition = cellPosition + new Vector3Int(x, y, 0);
                    TileBase groundTile = map.GetTile(neighborPosition);

                    // Check if tile is a ground tile
                    if (groundTile != null && System.Array.IndexOf(groundTiles, groundTile) != -1)
                    {
                        // Change tile to illuminated version
                        map.SetTile(neighborPosition, illuminatedGroundTile);
                    }
                }
            }
        }
    }
}
