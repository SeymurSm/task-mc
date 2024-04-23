using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;
    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
    }

    public void PlayGame(){
        SceneManager.LoadScene(Constants.CARD_MATCHING_SCENE);
    }

    public void GotoHome(){
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE);
    }

    private void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
