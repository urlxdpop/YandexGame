using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game.UI
{
    public class LoadLevels : MonoBehaviour
    {
        public void Easy()
        {
            if(YG2.isTimerAdvCompleted) YG2.InterstitialAdvShow();
            SceneManager.LoadScene(1);
        }

        public void Normal()
        {
            if (YG2.isTimerAdvCompleted) YG2.InterstitialAdvShow();
            SceneManager.LoadScene(2);
        }

        public void Hard()
        {
            if (YG2.isTimerAdvCompleted)
            {
                YG2.InterstitialAdvShow();
            }
            SceneManager.LoadScene(3);
        }
    }
}
