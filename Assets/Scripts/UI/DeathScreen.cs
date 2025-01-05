using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    public void ReturnToMain() {
        SceneManager.LoadScene(0);
    }
}
