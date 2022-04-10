using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Dialogue
    {
        private readonly string _playerPhrase;
        private readonly string _answer;
        private readonly List<DialogueAction> _actions;

        public string PlayerPhrase => _playerPhrase;
        public string Answer => _answer;
        public IReadOnlyList<DialogueAction> Actions => _actions.ToList();

        public Dialogue(string playerPhrase, string answer, List<DialogueAction> actions = null)
        {
            if (actions == null)
                actions = new List<DialogueAction>();

            _playerPhrase = playerPhrase;
            _answer = answer;
            _actions = actions;
        }

        public void DoAction(int index)
        {
            _actions[index].Do();
        }

        public void SetReturnAction(MovingDialogueAction action)
        {
            _actions.Add(action);
        }
    }
}
