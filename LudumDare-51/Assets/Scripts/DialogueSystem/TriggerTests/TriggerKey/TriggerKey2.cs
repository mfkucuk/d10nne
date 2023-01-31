using UnityEngine;
using System.Collections;
using TMPro;

public class TriggerKey2 : MonoBehaviour
{
    [SerializeField] private DialogueHolder dialogueHolder;

    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private TriggerKey3 triggerKey3;

    [SerializeField] private GameObject gameObject;

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
        
        gameObject.SetActive(false);
        
        triggerKey3.Init();
    }
    

}
