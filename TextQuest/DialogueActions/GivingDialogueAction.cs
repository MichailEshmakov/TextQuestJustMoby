using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class GivingDialogueAction : DialogueAction
    {
        protected override void PerformFeatures()
        {
            Give();
        }

        private void Give()
        {
            
        }
    }
}
