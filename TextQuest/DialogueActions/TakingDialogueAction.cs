﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class TakingDialogueAction : DialogueAction
    {
        protected override void PerformFeatures()
        {
            Take();
        }

        private void Take()
        {
            
        }
    }
}