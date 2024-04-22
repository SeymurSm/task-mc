using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public void GotoHome(){
        SceneController.instance.GotoHome();
    }
}
