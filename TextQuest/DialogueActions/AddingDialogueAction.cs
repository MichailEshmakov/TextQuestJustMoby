using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class AddingDialogueAction : ValueDialogueAction
    {
        public AddingDialogueAction(IValue value, string name = "", DialogueAction next = null) : base(value, name, next) { }
    }
}
