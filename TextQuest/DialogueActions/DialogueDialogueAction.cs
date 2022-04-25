using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class DialogueDialogueAction : DialogueAction
    {
        private Dialogue _nextDialogue;

        public Dialogue NextDialogue => _nextDialogue;

        public DialogueDialogueAction(Dialogue dialogue, string name = null) : 
            base(name ?? dialogue.PlayerPhrase, null)
        {
            if (dialogue == null)
                throw new ArgumentNullException(nameof(dialogue));

            _nextDialogue = dialogue;
        }
    }
}
