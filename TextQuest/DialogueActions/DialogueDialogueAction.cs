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

        public DialogueDialogueAction(Dialogue dialogue, string name = "", DialogueAction next = null) : base(name, next)
        {
            if (dialogue == null)
                throw new ArgumentNullException(nameof(dialogue));

            _nextDialogue = dialogue;
            // TODO: сделать глубокую проверку вперед, чтобы не было двух диалоговых событий в одной цепочке
        }
    }
}
