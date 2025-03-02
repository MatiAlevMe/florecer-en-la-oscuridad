using UnityEngine;

namespace Controllers
{
    public class CompanionAI : MonoBehaviour
    {
        public string[] adviceLines = new string[]
        {
            "Recuerda respirar profundamente cuando te sientas abrumada.",
            "Cada paso que das es un paso hacia tu crecimiento personal.",
            "No temas a los desafíos, son oportunidades de aprendizaje.",
            "Eres más fuerte de lo que crees.",
            "La luz interior siempre está contigo, incluso en los momentos más oscuros.",
            "Confía en tu intuición y en tu corazón.",
            "Cada obstáculo es una oportunidad para florecer.",
            "Tu valentía es tu mayor fortaleza."
        };

        public float adviceInterval = 10f;
        private float lastAdviceTime;

        void Start()
        {
            lastAdviceTime = Time.time;
        }

        void Update()
        {
            if (Time.time - lastAdviceTime > adviceInterval)
            {
                GiveAdvice();
                lastAdviceTime = Time.time;
            }
        }

        void GiveAdvice()
        {
            string advice = adviceLines[Random.Range(0, adviceLines.Length)];
            Debug.Log("Companion: " + advice);
            // TODO: Implement UI or dialogue system to display advice
        }
    }
}
