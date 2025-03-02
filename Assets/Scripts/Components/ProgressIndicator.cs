using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ProgressIndicator : MonoBehaviour
    {
        public Slider progressBar;
        private float progress = 0f;

        public void UpdateProgress(float amount)
        {
            progress += amount;
            if (progress > 1) progress = 1f;
            progressBar.value = progress;
        }
    }
}
