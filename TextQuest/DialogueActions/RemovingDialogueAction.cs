using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class RemovingDialogueAction : ValueDialogueAction
    {
        public RemovingDialogueAction(IValue value, string name = "", DialogueAction next = null) : base(value, name, next) { }
    }
}
