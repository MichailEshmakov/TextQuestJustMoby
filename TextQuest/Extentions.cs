using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public static class Extentions
    {
        public static List<DialogueAction> ToActions(this List<Dialogue> dialogues)
        {
            List<DialogueAction> dialogueActions = new List<DialogueAction>();
            foreach (Dialogue dialogue in dialogues)
            {
                DialogueAction action = dialogue.ToAction();
                dialogueActions.Add(action);
            }

            return dialogueActions;
        }

        public static DialogueAction ToAction(this Dialogue dialogue)
        {
            return new DialogueDialogueAction(name: dialogue.PlayerPhrase, dialogue: dialogue);
        }
    }
}
