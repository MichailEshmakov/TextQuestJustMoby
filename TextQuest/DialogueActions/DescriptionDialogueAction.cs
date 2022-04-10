using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class DescriptionDialogueAction : DialogueAction
    {
        private string _description;

        protected override void PerformFeatures()
        {
            ShowDescription();
        }

        private void ShowDescription()
        {
            
        }
    }
}
