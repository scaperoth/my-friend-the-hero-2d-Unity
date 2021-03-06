using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Quest
{
    public bool active = false;
    public string name;
    public int progress;
    public int success;

    public Dictionary<string, string[]> customCharacterDialogs;
    public Dictionary<string, bool> charactersSpokenTo;

    public Quest(string _name, int _progress, int _success)
    {
        name = _name;
        progress = _progress;
        success = _success;
        active = false;
        customCharacterDialogs = new Dictionary<string, string[]>();
        charactersSpokenTo = new Dictionary<string, bool>();
    }
}

public static class GameData
{
    public static bool DialogOpen = false;
    public static string PreviousScene;

    // quests
    public static string Quest1Name = "Talk to people in town";
    public static string Quest2Name = "Kill the forest monsters";
    public static string Quest3Name = "Take Hiro home to the Mayor";

    public static Quest CurrentQuest;
    public static List<Quest> Quests = new List<Quest>();

    public static UnityEvent<string[]> OnDialogOpen = new UnityEvent<string[]>();
    public static UnityEvent OnDialogChange = new UnityEvent();
    public static UnityEvent OnDialogClose = new UnityEvent();

    public static UnityEvent<Quest> OnQuestStarted = new UnityEvent<Quest>();
    public static UnityEvent<Quest> OnQuestUpdated = new UnityEvent<Quest>();
    public static UnityEvent<Quest> OnQuestComplete = new UnityEvent<Quest>();
    public static Dictionary<string, string[]> DefaultCharacterDialog = new Dictionary<string, string[]>();

    static public string CurrentActiveDialogCharacter;

    static GameData()
    {
        DefaultCharacterDialog.Add("Fisherman", new string[] { "Fisherman: Go away, kid." });
        DefaultCharacterDialog.Add("Gardener", new string[] { "Gardener: ... *singsong* La di da di diiiii..." });
        DefaultCharacterDialog.Add("Guard", new string[] { "Guard: All is well, son." });
        DefaultCharacterDialog.Add("Shopaholic", new string[] { "Shopaholic: What am I going to do now..." });
        DefaultCharacterDialog.Add("Hiro", new string[] { "Hiro: Hey, buddy." });
    }

    public static bool StartQuest1()
    {

        string[] introTextString = new string[]
        {
            "You: Hey, Hiro! How's it going?",
            "Hiro: Pretty good. I got a weird call to go see the mayor.",
            "You: The mayor?! What for?",
            "Hiro: I don't know. I think it has something to do with those monsters in the forest.",
            "You: Oh...That's weird. You are a terrible fighter...",
            "Hiro: $#^%! I know! It's probably just because I'm bigger than everyone else. I hate being typecast. I'm a gentle soul, man.",
            "You: Yeah. Well, you better go see what they want.",
            "Hiro: Alright, fine. Why don't you hang out around town while I'm gone.",
        };

        CurrentQuest = new Quest(Quest1Name, 0, 4);

        CurrentQuest.customCharacterDialogs.Add("Fisherman", new string[]{
            "Fisherman: Oh, come on you dang fish...",
            "Fisherman: Woah! What's wrong with you sneaking up on a fisherman like that. You should be ashamed.",
            "You: Sorry. Just trying to kill time.",
            "Fisherman: You kids these days. So violent... \"killing time\", he says. Sheesh.",
            "Fisherman: You know, your friend, Hiro, needs to calm down a bit, too. Always getting in fights. Ever since you were kids. So awful.",
            "You: That's a lie. People always fight Hiro, but he doesn't fight back!",
            "Fisherman: Whatever you say, kid. Someone that big is bound to cause trouble and I won't stand for it. Now, get away from me."
        });
        CurrentQuest.charactersSpokenTo.Add("Fisherman", false);

        CurrentQuest.customCharacterDialogs.Add("Gardener", new string[]{
            "You: Hey, Gardener. How are you?",
            "Gardener: Oh, darling, so good to see you! I'm doing very well, thank you! Have you seen my trees? Bigger than Hiro, those are!",
            "You: Yeah, they're growing really well.",
            "Gardener: Speaking of your friend, what's he been up to? Causing trouble? Hehe. I remember when I was your age... Oh the trouble I got into...",
            "You: Hiro doesn't cause trouble. He attracts it.",
            "Gardener: Oh, what a boisterous defense for a friend! You're a good one, you. Stay safe out there!"
        });
        CurrentQuest.charactersSpokenTo.Add("Gardener", false);

        CurrentQuest.customCharacterDialogs.Add("Guard", new string[]{
            "Guard: HALT! Where do you think you're going?!",
            "You: Just out for a walk. Calm down, guardsman.",
            "Guard: Calm down? CALM DOWN?!",
            "Guard: That's good advice. But don't you know there are monsters that have our beautiful, perfect, picturesque, wonderful, village under SIEGE!",
            "Guard: You'd best be careful lad. Stick with that Hiro. He'll show them monsters what-what with a flick of his wrist.",
            "You: But Hiro is an awful fighter.",
            "Guard: Jealousy is not a good color on you, son. A sidekick should always know their place. Now, scoot along so I can keep watch.",
            "You: *under breath* what a doof...",
            "Guard: I heard that."
        });
        CurrentQuest.charactersSpokenTo.Add("Guard", false);

        CurrentQuest.customCharacterDialogs.Add("Shopaholic", new string[]{
            "Shopaholic: Hey, what you got what you need? They've got everything in there. Usually. They're closed!",
            "You: Woah, slow down. What are you talking about?",
            "Shopaholic: What am i... What am I talking about?!",
            "Shopaholic: THE SHOP!",
            "Shopaholic: It's closed!",
            "Shopaholic: How in the world am I supposed too return my blender for a hat and my shoes for a blender now?!",
            "You: You want to do what?",
            "Shopaholic: Ugh, get outta here, you don't even care. You wouldn't know a deal if it hit you in the face.",
            "Shopaholic: You should have Hiro slap some sense into you. Oven mitt like that might knock a few things loose."
        });
        CurrentQuest.charactersSpokenTo.Add("Shopaholic", false);

        CurrentQuest.customCharacterDialogs.Add("Hiro", new string[] {
            "Hiro: Hey, I can't talk right now...*sigh*"
        });

        Quests.Add(CurrentQuest);
        OpenDialog(introTextString);

        return true;
    }

    public static bool StartQuest2()
    {
        string[] introTextString = new string[]
        {
            "Hiro: Ok, so let me fill you in on what's going on.",
            "Hiro: It turned out exactly as I feared. The mayor wants me to go \"Kill some monsters in the forest\"",
            "You: That makes sense. Everyone in town believes you are the only one who can do it.",
            "Hiro: But I can't! I don't know how to fight. Please help me!",
            "You: Oh, alright. It'll be just like when we were kids."
        };

        CurrentQuest = new Quest(Quest2Name, 0, 10);
        Quests.Add(CurrentQuest);
        CurrentQuest.customCharacterDialogs.Add("Hiro", introTextString);

        return true;
    }

    public static bool StartQuest3()
    {
        string[] introTextString = new string[]
        {
            "Hiro: I guess we should go home to and tell everyone we did it.",
            "You: You mean YOU did it. *wink*",
        };

        CurrentQuest = new Quest(Quest3Name, 0, 0);
        Quests.Add(CurrentQuest);
        CurrentQuest.customCharacterDialogs.Add("Hiro", introTextString);
        OnQuestStarted.Invoke(CurrentQuest);
        CurrentQuest.active = true;
        return true;
    }

    public static void OpenDialog(string[] text)
    {
        DialogOpen = true;
        OnDialogOpen.Invoke(text);
    }

    public static void UpdateQuest1Progress()
    {
        CurrentQuest.progress++;
        OnQuestUpdated.Invoke(CurrentQuest);
        if (CurrentQuest.progress == CurrentQuest.success)
        {
            CurrentQuest.active = false;
            OnQuestComplete.Invoke(CurrentQuest);
            CurrentQuest = null;
        }
    }

    public static void UpdateQuest2Progress()
    {
        CurrentQuest.progress++;
        OnQuestUpdated.Invoke(CurrentQuest);
        if (CurrentQuest.progress == CurrentQuest.success)
        {
            CurrentQuest.active = false;
            OnQuestComplete.Invoke(CurrentQuest);
            CurrentQuest = null;
            StartQuest3();
        }
    }

    public static void CloseDialog()
    {
        if(CurrentQuest != null && CurrentQuest.active == false)
        {
            CurrentQuest.active = true;
            OnQuestStarted.Invoke(CurrentQuest);

            if(CurrentQuest.name == Quest2Name)
            {
                CurrentQuest.customCharacterDialogs["Hiro"] = new string[]
                {
                    "Hiro: Let's go take out those monsters...I guess?",
                };
            }
        }
        CurrentActiveDialogCharacter = null;
        DialogOpen = false;
        OnDialogClose.Invoke();
    }

    public static void StartCharacterDialog(string characterName)
    {
        string[] dialog;
        CurrentActiveDialogCharacter = characterName;
        if (CurrentQuest == null)
        {
            dialog = DefaultCharacterDialog[characterName];
            OpenDialog(dialog);
        }
        else
        {
            bool hasDialog = CurrentQuest.customCharacterDialogs.TryGetValue(characterName, out dialog);

            if (hasDialog)
            {
                OpenDialog(dialog);

                bool spokeToThisCharacter;
                bool exists = CurrentQuest.charactersSpokenTo.TryGetValue(characterName, out spokeToThisCharacter);
                if (exists && !spokeToThisCharacter && CurrentQuest.name == Quest1Name)
                {
                    CurrentQuest.charactersSpokenTo[characterName] = true;
                    UpdateQuest1Progress();
                }
            }
            else
            {
                dialog = DefaultCharacterDialog[characterName];
                OpenDialog(dialog);
            }
        }
    }
}
