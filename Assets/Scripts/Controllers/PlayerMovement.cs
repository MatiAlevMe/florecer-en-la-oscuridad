using UnityEngine;

namespace Controllers
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical);

            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }
}
