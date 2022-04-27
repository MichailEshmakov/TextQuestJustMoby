using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            DialogueAction robHoboAction = CreateRobHoboAction();
            DialogueAction saveHoboAction = CreateSaveHoboAction();
            Character hobo = CreateHobo(robHoboAction, saveHoboAction);
            Room tavern = CreateTavern(hobo);
            Player player = new Player();
            Game game = new Game(player);
            View view = new View(game);
            game.SetRoom(tavern);
        }

        private static DialogueAction CreateRobHoboAction()
        {
            Dialogue robHoboDialogue = new Dialogue("Забрать всё что у него есть", "Я теперь умру с голоду");
            DialogueDialogueAction robHoboDialogueAction = new DialogueDialogueAction(robHoboDialogue);
            HashSet<string> robHoboEvents = new HashSet<string> { Events.HoboIsRobbed, Events.MadalionIsGiven };
            EventDialogueAction robHoboEventsAction = new EventDialogueAction(robHoboEvents, next: robHoboDialogueAction);
            IValue robHoboMoney = new Money(50);
            return new AddingDialogueAction(robHoboMoney, next: robHoboEventsAction);
        }

        private static DialogueAction CreateSaveHoboAction()
        {
            Dialogue saveHoboDialogue = new Dialogue("Оставить бродягу в покое", "Бродяга всадил Вам нож в спину как только Вы отвернулись");
            DialogueDialogueAction saveHoboDialogueAction = new DialogueDialogueAction(saveHoboDialogue);
            IValue saveHoboDamage = new Health(100);
            return new RemovingDialogueAction(saveHoboDamage, next: saveHoboDialogueAction);
        }

        private static Character CreateHobo(DialogueAction robHoboAction, DialogueAction saveHoboAction)
        {
            List<DialogueAction> hoboActions = new List<DialogueAction> { robHoboAction, saveHoboAction };
            Dialogue hoboBeforeRobeDialogue = new Dialogue("Жизнь или смерть, грязный бродяга!", "Вот возьми всё что у меня есть, только не трогай меня", hoboActions);
            Dialogue hoboAfterRobeDialogue = new Dialogue("Жизнь или смерть, грязный бродяга!", "У меня больше ничего нет");
            Condition defaultHoboCondition = new DefaultCondition();
            Condition isHoboRobedCondition = new EventCondition(Events.HoboIsRobbed);
            Dictionary<Condition, Dialogue> hoboDialogues = new Dictionary<Condition, Dialogue>
            {
                { isHoboRobedCondition, hoboAfterRobeDialogue },
                { defaultHoboCondition, hoboBeforeRobeDialogue }
            };
            DialogueNode hoboDialogueNode = new DialogueNode(hoboDialogues);

            return new Character("Бродяга", "Поговорить с бродягой", hoboDialogueNode);
        }

        private static Room CreateTavern(Character hobo)
        {
            Dialogue lookingAround = new Dialogue("Осмотреть окрестности", "В таверне чисто и тепло");

            List<Dialogue> tavernDialogues = new List<Dialogue> { lookingAround };
            List<Character> tavernCharacters = new List<Character> { hobo };
            return new Room("Таверна", tavernDialogues, tavernCharacters);
        }
    }
}
