using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ToggleSound : MonoBehaviour
    {
        [SerializeField] private Sprite off;
        [SerializeField] private Sprite on;
        [SerializeField] private Image image;

        private bool isMuted = false;

        public void ToggleMute()
        {
            isMuted = !isMuted;
            AudioListener.pause = isMuted;

            image.sprite = isMuted ? off : on;
        }
    }
}