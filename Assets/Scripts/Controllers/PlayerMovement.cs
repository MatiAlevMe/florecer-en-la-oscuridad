using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float dashSpeed = 10f;
        public float dashDuration = 0.5f;
        private float dashTimer;
        private Rigidbody2D rb;
        private Animator animator;
        public Transform companionTransform;
        private bool isPlayerControl = true;
        private bool isDashing = false;

        // Parámetros de seguimiento del compañero
        public float companionFollowDistance = 1.5f;
        public float companionAdviceInterval = 10f;
        private float lastAdviceTime;

        // Nuevos campos para control de entrada
        private string horizontalAxis;
        private string verticalAxis;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            lastAdviceTime = Time.time;

            // Configurar ejes de entrada por defecto
            SetDefaultInputAxes();
        }

        void SetDefaultInputAxes()
        {
            horizontalAxis = isPlayerControl ? "Horizontal" : "JIKLHorizontal";
            verticalAxis = isPlayerControl ? "Vertical" : "JIKLVertical";
        }

        void Update()
        {
            float horizontal = Input.GetAxisRaw(horizontalAxis);
            float vertical = Input.GetAxisRaw(verticalAxis);

            Vector2 movement = new Vector2(horizontal, vertical).normalized;

            // Dash input
            if (Input.GetButtonDown("Dash") && !isDashing)
            {
                StartDash(movement);
            }

            if (isDashing)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    StopDash();
                }
            }

            // Mover el personaje
            if (!isDashing)
            {
                rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + movement * dashSpeed * Time.deltaTime);
            }

            // Actualizar parámetros del Animator
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", movement.magnitude);
            animator.SetBool("IsDashing", isDashing);

            // Lógica de seguimiento del compañero
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
            Vector2 direction = (Vector2)transform.position - (Vector2)companionTransform.position;
            if (direction.magnitude > companionFollowDistance)
            {
                direction = direction.normalized;
                companionTransform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            }
        }

        void CheckCompanionAdvice()
        {
            // Lógica para dar consejos al jugador en intervalos
            if (Time.time - lastAdviceTime >= companionAdviceInterval)
            {
                GiveCompanionAdvice();
                lastAdviceTime = Time.time;
            }
        }

        void GiveCompanionAdvice()
        {
            // Implementar lógica de consejos del compañero
            // Podría ser un diálogo o un mensaje en pantalla
            Debug.Log("Companion advice: Keep going, you're doing great!");
        }

        public void SwitchControl()
        {
            isPlayerControl = !isPlayerControl;
            SetDefaultInputAxes();
        }

        public void IlluminateMap(Tilemap map, TileBase[] groundTiles, TileBase illuminatedGroundTile)
        {
            // Obtener la posición del jugador en coordenadas del Tilemap
            Vector3Int cellPosition = map.WorldToCell(transform.position);

            // Iluminar los tiles alrededor del jugador
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int neighborPosition = cellPosition + new Vector3Int(x, y, 0);
                    TileBase groundTile = map.GetTile(neighborPosition);

                    // Verificar si el tile es un tile de tierra
                    if (groundTile != null && System.Array.IndexOf(groundTiles, groundTile) != -1)
                    {
                        // Cambiar el tile a un tile iluminado
                        map.SetTile(neighborPosition, illuminatedGroundTile);
                    }
                }
            }
        }
    }
}
