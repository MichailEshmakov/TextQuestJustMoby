using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class DialogueNode
    {
        private readonly Dictionary<Condition, Dialogue> _dialogues;

        public DialogueNode(Dialogue dialogue)
        {
            if (dialogue == null)
                throw new ArgumentNullException(nameof(dialogue));

            DefaultCondition defaultCondition = new DefaultCondition();
            _dialogues = new Dictionary<Condition, Dialogue> 
            {
                { defaultCondition, dialogue }
            };
        }

        public DialogueNode(Dictionary<Condition, Dialogue> dialogues)
        {
            if (dialogues == null)
                throw new ArgumentNullException(nameof(dialogues));

            List<Condition> conditions = dialogues.Keys.ToList();

            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i] == null)
                    throw new ArgumentNullException($"{nameof(conditions)}[{i}]");

                if (dialogues[conditions[i]] == null)
                    throw new ArgumentNullException($"{nameof(dialogues)}[{nameof(conditions)}[{i}]]");

                if ((i < conditions.Count - 1) && (conditions[i] is DefaultCondition))
                    throw new ArgumentException($"{nameof(conditions)}[{i}]");

                if ((i == conditions.Count - 1) && (conditions[i] is DefaultCondition == false))
                    throw new ArgumentException($"{nameof(conditions)}[{i}]");
            }

            _dialogues = dialogues;
        }

        public Dialogue GetActualDialogue(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            foreach (Condition condition in _dialogues.Keys)
            {
                if (condition.IsMet(player))
                    return _dialogues[condition];
            }

            throw new InvalidOperationException();
        }
    }
}
