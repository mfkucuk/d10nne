using UnityEngine;
using System.Collections;
using TMPro;

public class TriggerKey3 : MonoBehaviour
{
    [SerializeField] private DialogueHolder dialogueHolder;

    [SerializeField] private TMP_Text dialogueText;
    
    [SerializeField] private GameObject gameObject;

    [SerializeField] private Transition2 transition2;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Cinematic");
    }

    public void Init()
    {
        dialogueHolder.HolderOnStartDialogueActions += HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions += HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions += HolderOnEndDialogueActions;
        dialogueHolder.HolderOnOneDialogueEndActions += HolderOnOneDialogueEndActions;
        
        DialogueTrigger.Instance.TriggerDialogue();
        
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        dialogueHolder.HolderOnStartDialogueActions -= HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions -= HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions -= HolderOnEndDialogueActions;
        dialogueHolder.HolderOnOneDialogueEndActions -= HolderOnOneDialogueEndActions;
    }

    private void HolderOnStartDialogueActions()
    {
        
    }
    
    private void HolderOnCustomDialogueActions(RealDialogue realDialogue, int index)
    {

    }
    
    private void HolderOnOneDialogueEndActions()
    {
        StartCoroutine(WaitAndStartDialogue2());
    }
    
    private IEnumerator WaitAndStartDialogue2()
    {
        yield return new WaitForSeconds(1f);
        DialogueTrigger.Instance.TriggerDialogue();
    }
    
    private void HolderOnEndDialogueActions()
    {
        dialogueHolder.HolderOnStartDialogueActions -= HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions -= HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions -= HolderOnEndDialogueActions;
        dialogueHolder.HolderOnOneDialogueEndActions -= HolderOnOneDialogueEndActions;

        StartCoroutine(WaitEndLoad());
    }
    
    private IEnumerator WaitEndLoad()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.StopMusic();
        StartCoroutine(transition2.EndTransition("Tutorial 1"));
    }
    

}
