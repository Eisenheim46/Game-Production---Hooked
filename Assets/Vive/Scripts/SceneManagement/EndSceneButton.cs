using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private bool loadScene;
    [SerializeField] private bool quitGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hook")
        {
            if (loadScene)
            {
                LoadScene();
            }
            else if (quitGame)
            {
                QuitGame();
            }
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
}
