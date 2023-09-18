using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.HUD
{
    [DisallowMultipleComponent]
    public class RestartButton : MonoBehaviour
    {
        private Button restartButton;

        private void Awake()
        {
            restartButton = gameObject.GetComponent<Button>();
            restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}