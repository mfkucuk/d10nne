using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class TriggerKey1 : MonoBehaviour
{
    [SerializeField] private DialogueHolder dialogueHolder;

    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private TriggerKey2 triggerKey2;
    
    [SerializeField] private GameObject gameObject;
    
    private bool flag = false;

    private void Start()
    {
        dialogueHolder.HolderOnStartDialogueActions += HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions += HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions += HolderOnEndDialogueActions;
        dialogueHolder.HolderOnOneDialogueEndActions += HolderOnOneDialogueEndActions;

        StartCoroutine(WaitAndStartDialogue());

    }
    
    private IEnumerator WaitAndStartDialogue()
    {
        yield return new WaitForSeconds(2f);
        DialogueTrigger.Instance.TriggerDialogue();

        flag = true;
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
        
        triggerKey2.Init();
    }
    

}
