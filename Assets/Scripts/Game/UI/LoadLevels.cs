using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class LoadLevels : MonoBehaviour
    {
        public void Easy()
        {
            SceneManager.LoadScene(1);
        }

        public void Normal()
        {
            SceneManager.LoadScene(2);
        }

        public void Hard()
        {
            SceneManager.LoadScene(3);
        }
    }
}
