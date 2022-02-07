using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Quest
{
    public bool active = true;
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
        customCharacterDialogs = new Dictionary<string, string[]>();
        charactersSpokenTo = new Dictionary<string, bool>();
}

public static class GameData
{
    public static bool DialogOpen = false;
    static string Quest1Name = "Talk to people in town";
    static Quest CurrentQuest;
    public static List<Quest> Quests = new List<Quest>();

    public static UnityEvent<string[]> OnDialogOpen = new UnityEvent<string[]>();
    public static UnityEvent OnDialogChange = new UnityEvent();
    public static UnityEvent OnDialogClose = new UnityEvent();

    public static UnityEvent<Quest> OnQuestStarted = new UnityEvent<Quest>();
    public static UnityEvent<Quest> OnQuestComplete = new UnityEvent<Quest>();

    static GameData()
    {
        OnDialogClose.AddListener(HandleDialogClosed);
        OnDialogOpen.AddListener(HandleDialogOpen);
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
            "Hiro: $#^%! I know! It's probably just because I'm bigger than everyone else. I hate being typecast. I'm a gental soul, man",
            "You: Yeah. Well, you better go see what they want.",
            "Hiro: Alright, fine. Why don't you hang out around town while I'm gone.",
        };

        
        CurrentQuest = new Quest(Quest1Name, 0, 4);
        Quests.Add(CurrentQuest);
        OnQuestStarted.Invoke(CurrentQuest);
        OnDialogOpen.Invoke(introTextString);

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
            "Gardener: Oh, what a boisterous defense for a friend! Your a good one, you. Stay safe out there!"
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

        return true;
    }

    public static void UpdateQuest1Progress()
    {
        CurrentQuest.progress++;
        if(CurrentQuest.progress == CurrentQuest.success)
        {
            CurrentQuest.active = false;
            OnQuestComplete.Invoke(CurrentQuest);
            CurrentQuest = null;
        }

        Debug.Log($"CurrentQuest: {CurrentQuest.progress}/{CurrentQuest.success}");
    }

    public static void HandleDialogClosed()
    {
        DialogOpen = false;
    }

    public static void HandleDialogOpen(string[] dialog)
    {
        DialogOpen = true;
    }


    public static void StartCharacterDialog(string characterName)
    {
        if(CurrentQuest == null)
        {
            return;
        }
        else
        {
            string[] dialog = CurrentQuest.customCharacterDialogs[characterName];
            bool spokeToThisCharacter = CurrentQuest.charactersSpokenTo[characterName];
            if (!spokeToThisCharacter)
            {
                CurrentQuest.charactersSpokenTo[characterName] = true;
                UpdateQuest1Progress();
            }
            OnDialogOpen.Invoke(dialog);
        }
    }
}
