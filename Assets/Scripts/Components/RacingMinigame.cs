using UnityEngine;

namespace Components
{
    public class RacingMinigame : MonoBehaviour
    {
        public Transform playerCar;
        public float speed = 10f;

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerCar.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
        }
    }
}
