using UnityEngine;

public class FlagObject : MonoBehaviour
{
    [SerializeField] private Transition transition;
    [SerializeField] private string nextSceneName;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AudioManager.Instance.PlayCustomSoundEffect("G4-B5-A5-C5");

            transition.transform.position = transform.position;
            
            StartCoroutine(transition.EndTransition(nextSceneName) );
        }
    }
}
