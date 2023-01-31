using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(10f);
        
        AudioManager.Instance.StopMusic();
        LevelController.Instance.LoadSceneWithName("MainMenu");
    }

}
