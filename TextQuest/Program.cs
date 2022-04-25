using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class Program
    {
        const string OptionMark = ") ";

        static void Main(string[] args)
        {
            Dialogue robHoboDialogue = new Dialogue("Забрать всё что у него есть", "Я теперь умру с голоду");
            Dialogue saveHoboDialogue = new Dialogue("Оставить бродягу в покое", "Бродяга всадил Вам нож в спину как только Вы отвернулись");

            HashSet<string> robHoboEvents = new HashSet<string> { Events.HoboIsRobbed, Events.MadalionIsGiven };
            DialogueDialogueAction robHoboDialogueAction = new DialogueDialogueAction(robHoboDialogue);
            EventDialogueAction robHoboEventsAction = new EventDialogueAction(robHoboEvents, next: robHoboDialogueAction);
            DialogueDialogueAction saveHoboDialogueAction = new DialogueDialogueAction(saveHoboDialogue);

            List<DialogueAction> hoboActions = new List<DialogueAction> { robHoboEventsAction, saveHoboDialogueAction };
            Dialogue hoboBeforeRobeDialogue = new Dialogue("Жизнь или смерть, грязный бродяга!", "Вот возьми всё что у меня есть, только не трогай меня", hoboActions);
            Dialogue hoboAfterRobeDialogue = new Dialogue("Жизнь или смерть, грязный бродяга!", "У меня больше ничего нет"); // TODO: сделать это из экшена, а не в начале
            Condition defaultHoboCondition = new DefaultCondition();
            Condition isHoboRobedCondition = new EventCondition(Events.HoboIsRobbed);
            Dictionary<Condition, Dialogue> hoboDialogues = new Dictionary<Condition, Dialogue>
            {
                { isHoboRobedCondition, hoboAfterRobeDialogue },
                { defaultHoboCondition, hoboBeforeRobeDialogue }
            };
            DialogueNode hoboDialogueNode = new DialogueNode(hoboDialogues);

            Character hobo = new Character("Бродяга", "Поговорить с бродягой", hoboDialogueNode);

            Dialogue lookingAround = new Dialogue("Осмотреть окрестности", "В таверне чисто и тепло");

            List<Dialogue> tavernDialogues = new List<Dialogue> { lookingAround };
            List<Character> tavernCharacters = new List<Character> { hobo };
            Room tavern = new Room("Таверна", tavernDialogues, tavernCharacters);
            Player player = new Player();
            Game game = new Game(player);

            game.DialogueSet += OnDialogueSet; // TODO: unsubscribe in gameover

            game.SetRoom(tavern);
        }

        private static void OnDialogueSet(Game game, Dialogue dialogue)
        {
            ShowDialogue(dialogue);
            int input = GetInput(dialogue);
            Console.Clear();
            game.ChooseAction(input - 1);
        }

        static void ShowDialogue(Dialogue dialogue)
        {
            if (string.IsNullOrEmpty(dialogue.PlayerPhrase) == false)
                Console.WriteLine(dialogue.PlayerPhrase);
            
            if (string.IsNullOrEmpty(dialogue.Answer) == false)
                Console.WriteLine(dialogue.Answer);

            Console.WriteLine();
            IReadOnlyList<DialogueAction> actions = dialogue.Actions;
            for (int i = 0; i < actions.Count; i++)
            {
                Console.WriteLine($"{i + 1}{OptionMark}{actions[i].Name}");
            }
        }

        static int GetInput(Dialogue dialogue)
        {
            bool isCorrectInput = false;
            int numericInput = -1;

            while (isCorrectInput == false)
            {
                char input = Console.ReadKey(true).KeyChar;
                numericInput = Convert.ToInt32(char.GetNumericValue(input));
                isCorrectInput = numericInput >= 1 && dialogue.Actions.Count >= numericInput;
            }

            return numericInput;
        }
    }
}
