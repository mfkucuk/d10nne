using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LevelController.Instance.LoadSceneWithName("Cinematic");   
        }
    }
}
