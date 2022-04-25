using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class DefaultCondition : Condition
    {
        public override bool IsMet(Player player)
        {
            return true;
        }
    }
}
