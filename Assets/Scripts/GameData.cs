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

    public Quest(string _name, int _progress, int _success)
    {
        name = _name;
        progress = _progress;
        success = _success;
        customCharacterDialogs = new Dictionary<string, string[]>();
    }
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
        DialogOpen = true;
        OnDialogClose.AddListener(HandleDialogClosed);

        CurrentQuest.customCharacterDialogs.Add("Fisherman", new string[]{
            "Fisherman: Oh, come on you dang fish...",
            "Fisherman: Woah! What's wrong with you sneaking up on a fisherman like that. You should be ashamed.",
            "You: Sorry. Just trying to kill time.",
            "Fisherman: You kids these days. So violent... \"killing time\", he says. Sheesh.",
            "Fisherman: You know, your friend, Hiro, needs to calm down a bit, too. Always getting in fights. Ever since you were kids. So awful.",
            "You: That's a lie. People always fight Hiro, but he doesn't fight back!",
            "Fisherman: Whatever you say, kid. Someone that big is bound to cause trouble and I won't stand for it. Now, get away from me."
        });

        CurrentQuest.customCharacterDialogs.Add("Gardener", new string[]{
            "You: Hey, Gran Gran. How are you?",
            "Gardener: Oh, darling, so good to see you! I'm doing very well, thank you! Have you seen my trees? Bigger than Hiro, those are!",
            "You: Yeah, they're growing really well.",
            "Gardener: Speaking of your friend, what's he been up to? Causing trouble? Hehe. I remember when I was your age... Oh the trouble I got into...",
            "You: Hiro doesn't cause trouble. He attracts it.",
            "Gardener: Oh, what a boisterous defense for a friend! Your a good one, you. Stay safe out there!"
        });

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

        CurrentQuest.customCharacterDialogs.Add("Shopaholic", new string[]{
            "Shopaholic: HALT! Where do you think you're going?!",
            "You: Just out for a walk. Calm down, guardsman."
        });

        return true;
    }

    public static void UpdateQuest1Progress()
    {
        CurrentQuest.progress++;
        if(CurrentQuest.progress == CurrentQuest.success)
        {
            CurrentQuest.active = false;
            Debug.Log($"Quests: {Quests[0].active}");
            OnQuestComplete.Invoke(CurrentQuest);
            CurrentQuest = null;
        }
    }

    public static void HandleDialogClosed()
    {
        DialogOpen = false;
        OnDialogClose.RemoveListener(HandleDialogClosed);
    }

    static void GetCharacterDialog(string characterName)
    {
        if(CurrentQuest == null)
        {

        }
        else
        {

        }
    }
}
