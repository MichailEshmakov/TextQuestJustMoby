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

        private readonly Player _player;
        private Dialogue _currentDialogue;
        private Room _currentRoom;
        private bool _isOver;

        public bool IsOver => _isOver;

        public event Action<Game, Dialogue> DialogueSet;

        public Game(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            _player = player;
            _player.Dead += OnPlayerDead;
        }

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
            Dialogue roomDialogue = _currentRoom.Enter(_player, enterPhrase);
            SetDialogue(roomDialogue);
        }

        public void ChooseAction(int option)
        {
            _currentDialogue.DoAction(option);
        }

        private void OnPlayerDead()
        {
            _player.Dead -= OnPlayerDead;
            _isOver = true;
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
            action.Doing -= OnActionDoing;
            if (action.Next != null)
                UnsubscribeDeeply(action.Next);
        }

        private void SubscribeDeeply(DialogueAction action)
        {
            action.Doing += OnActionDoing;
            if (action.Next != null)
                SubscribeDeeply(action.Next);
        }

        private void OnActionDoing(DialogueAction action)
        {
            if (action is DialogueDialogueAction dialogueDialogueAction)
            {
                SetDialogue(dialogueDialogueAction.NextDialogue);
            }
            else if (action is MovingDialogueAction movingDialogueAction)
            {
                SetRoom(movingDialogueAction.Room);
            }
            else if (action is EventDialogueAction eventDialogueAction)
            {
                _player.AddEvents(eventDialogueAction.Events);
            }
            else if (action is AddingDialogueAction addingDialogueAction)
            {
                _player.Add((dynamic)addingDialogueAction.Value);
            }
            else if (action is RemovingDialogueAction removingDialogueAction)
            {
                _player.Remove((dynamic)removingDialogueAction.Value);
            }
            else
            {
                throw new ArgumentException(nameof(action));
            }
        }
    }
}
