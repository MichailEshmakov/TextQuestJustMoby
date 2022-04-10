using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Game
    {
        private const string EmptyDialogueAnswer = "Ок";

        private Dialogue _currentDialogue;
        private Room _currentRoom;

        public event Action<Game, Dialogue> DialogueSet;

        public void SetDialogue(Dialogue dialogue)
        {
            if (dialogue == null)
                throw new ArgumentNullException(nameof(dialogue));

            if (_currentDialogue != null)
                UnsubscribeOnActions();

            _currentDialogue = dialogue;
            if (_currentDialogue.Actions.Count == 0)
                AddReturnAction();

            SubscribeOnActions();
            DialogueSet?.Invoke(this, _currentDialogue);
        }

        public void SetRoom(Room room, string enterPhrase = "")
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _currentRoom = room;
            Dialogue roomDialogue = _currentRoom.Enter(enterPhrase);
            SetDialogue(roomDialogue);
        }

        public void ChooseAction(int option)
        {
            _currentDialogue.DoAction(option);
        }

        private void AddReturnAction()
        {
            _currentDialogue.SetReturnAction(new MovingDialogueAction(room: _currentRoom, name: EmptyDialogueAnswer));
        }

        private void SubscribeOnActions()
        {
            foreach (DialogueAction action in _currentDialogue.Actions)
            {
                SubscribeDeeply(action);
            }
        }

        private void UnsubscribeOnActions()
        {
            foreach (DialogueAction action in _currentDialogue.Actions)
            {
                UnsubscribeDeeply(action);
            }
        }

        private void UnsubscribeDeeply(DialogueAction action)
        {
            action.Done -= OnActionDone;
            if (action.Next != null)
                UnsubscribeDeeply(action.Next);
        }

        private void SubscribeDeeply(DialogueAction action)
        {
            action.Done += OnActionDone;
            if (action.Next != null)
                SubscribeDeeply(action.Next);
        }

        private void OnActionDone(DialogueAction action)
        {
            if (action is DialogueDialogueAction)
            {
                DialogueDialogueAction dialogueDialogueAction = (DialogueDialogueAction)action;
                SetDialogue(dialogueDialogueAction.NextDialogue);
            }
            else if (action is MovingDialogueAction)
            {
                MovingDialogueAction movingDialogueAction = (MovingDialogueAction)action;
                SetRoom(movingDialogueAction.Room);
            }
        }
    }
}
