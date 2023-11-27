namespace TheNaturesLastStand;

public class ScreenManager
{
    private int _inputBoxWidth;
    private int _inputBoxHeight;
    private int _inputBoxTop;
    private int _inputBoxLeft;
    
    private int _largeBoxHeight;
    private int _largeBoxTop;
    private int _largeBoxWidth;

    private int _conversationBoxWidth;
    private int _conversationBoxHeight;
    private int _conversationBoxTop;
    private int _conversationBoxLeft;
    
    public ScreenManager()
    {
        Init();
    }

    public void DisplayExampleData()
    {
        List<string> exampleQuestData = new List<string>();
        exampleQuestData.Add("Costel");
        UpdateScreen(150, exampleQuestData, "No Biome", "No Location", "No Message");
    }
    
    private void DisplayTitle()
    {
        string title = @"                         _   _       _                  _       _           _         _                  _ 
                         | \ | |     | |                ( )     | |         | |       | |                | | 
                         |  \| | __ _| |_ _   _ _ __ ___|/ ___  | | __ _ ___| |_   ___| |_ __ _ _ __   __| | 
                         | . ` |/ _` | __| | | | '__/ _ \ / __| | |/ _` / __| __| / __| __/ _` | '_ \ / _` | 
                         | |\  | (_| | |_| |_| | | |  __/ \__ \ | | (_| \__ \ |_  \__ \ || (_| | | | | (_| | 
                         |_| \_|\__,_|\__|\__,_|_|  \___| |___/ |_|\__,_|___/\__| |___/\__\__,_|_| |_|\__,_| 
                                                                                     ";
        Console.Write(title);
    }

    public void UpdateScreen(int balance, List<string> quests, string biome, string location, string message)
    {
        DrawStatusBox(balance, quests);
        DrawConversationBox(biome, location, message);
        DrawInputBox();
        SetCursorToInside();
    }
    
    private void DrawInputBox()
    {
        Console.SetCursorPosition(_inputBoxLeft, _inputBoxTop);
        Console.Write("┌" + new string('─', _inputBoxWidth - 2) + "┐");

        for (int i = 1; i < _inputBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_inputBoxLeft, _inputBoxTop + i);
            Console.Write("│" + new string(' ', _inputBoxWidth - 2) + "│");
        }

        Console.SetCursorPosition(_inputBoxLeft, _inputBoxTop + _inputBoxHeight - 1);
        Console.Write("└" + new string('─', _inputBoxWidth - 2) + "┘");
    }
    
    private void DrawStatusBox(int balance, List<string> quests)
    {
        Console.SetCursorPosition(_inputBoxLeft, _largeBoxTop);
        Console.Write("┌" + new string('─', _largeBoxWidth - 2) + "┐");

        for (int i = 1; i < _largeBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_inputBoxLeft, _largeBoxTop + i);
            if (i == 1)
            {
                Console.Write("│" + CenterTextInString($"Balance: {balance}$", 30 - 2) + "│");
            } 
            else if (i == 3)
            {
                Console.Write("│" + CenterTextInString("Active Quests", 30 - 2) + "│");
            }
            else if (i == 4)
            {
                Console.Write("│" + new string('─', _largeBoxWidth - 2) + "│");
            }
            else if (i == 5)
            {
                if (quests.Count != 0)
                {
                    for (int j = 0; j < quests.Count; j++)
                    {
                        if (j == _largeBoxHeight - 2)
                        {
                            Console.Write("│" + CenterTextInString("....", 30 - 2) + "│");
                        }
                        Console.SetCursorPosition(_inputBoxLeft, _largeBoxTop + i + j);
                        Console.Write("│" + CenterTextInString($"{j + 1}.{quests[j]}", 30 - 2) + "│");
                    }

                    i += quests.Count - 1;
                }
                else
                {
                    Console.Write("│" + CenterTextInString("No Active Quests", 30 - 2) + "│");
                }
            }
            else
            {
                Console.Write("│" + new string(' ', _largeBoxWidth - 2) + "│");
            }
        }

        Console.SetCursorPosition(_inputBoxLeft, _largeBoxTop + _largeBoxHeight - 1);
        Console.Write("└" + new string('─', _largeBoxWidth - 2) + "┘");
    }
    
    private void DrawConversationBox(string biome, string location, string conversation)
    {
        Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop);
        Console.Write("┌" + new string('─', _conversationBoxWidth - 2) + "┐");

        for (int i = 1; i < _conversationBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop + i);
            if (i == 1)
            {
                Console.Write("│" + CenterTextInString($"Biome: {biome}", 100 - 2) + "│");
            } 
            else if (i == 3)
            {
                Console.Write("│" + CenterTextInString($"Location: {location}", 100 - 2) + "│");
            }
            else if (i == 4)
            {
                Console.Write("│" + new string('─', _conversationBoxWidth - 2) + "│");
            }
            else if (i == 5)
            {
                if (conversation.Length != 0)
                {
                    List<string> conversationLines = SplitStringIntoLines(conversation, 30);
                    foreach (var line in conversationLines)
                    {
                        Console.Write("│" + CenterTextInString($"{line}", 100 - 2) + "│");
                    }
                }
                else
                {
                    Console.Write("│" + CenterTextInString("0 New Messages.", 100 - 2) + "│");
                }
            }
            else
            {
                Console.Write("│" + new string(' ', _conversationBoxWidth - 2) + "│");
            }
        }

        Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop + _conversationBoxHeight - 1);
        Console.Write("└" + new string('─', _conversationBoxWidth - 2) + "┘");
    }

    private string CenterTextInString(string text, int totalWidth)
    {
        if (text.Length >= totalWidth)
        {
            return text;
        }

        int padding = totalWidth - text.Length;
        int padLeft = padding / 2;

        return text.PadLeft(text.Length + padLeft).PadRight(totalWidth);
    }

    private List<string> SplitStringIntoLines(string text, int maxCharsPerLine)
    {
        List<string> lines = new List<string>();

        if (string.IsNullOrEmpty(text))
        {
            return lines;
        }

        while (text.Length > maxCharsPerLine)
        {
            int splitIndex = text.LastIndexOf(' ', maxCharsPerLine);
            if (splitIndex == -1) splitIndex = maxCharsPerLine;

            lines.Add(text.Substring(0, splitIndex));
            text = text.Substring(splitIndex).TrimStart();
        }

        lines.Add(text);
        return lines;
    }
    
    private void SetCursorToInside()
    {
        Console.SetCursorPosition(_inputBoxLeft + 1, _inputBoxTop + 1);
    }

    public void DisplayGameOver()
    {
        var gameOverString = 
            @"  ________                        ________                     
 /  _____/_____    _____   ____   \_____  \___  __ ___________ 
/   \  ___\__  \  /     \_/ __ \   /   |   \  \/ // __ \_  __ \
\    \_\  \/ __ \|  Y Y  \  ___/  /    |    \   /\  ___/|  | \/
 \______  (____  /__|_|  /\___  > \_______  /\_/  \___  >__|   
        \/     \/      \/     \/          \/          \/       
                                                               
                    Thank you for playing!                     ";
        Console.Clear();
        Console.Write(gameOverString);
    }

    public void DisplayHelp()
    {
        //TODO
    }

    public string ReadCommand()
    {
        return Console.ReadLine() ?? "";
    }
    
    public void Init()
    {
        DisplayTitle();
        _inputBoxWidth = 132;
        _inputBoxHeight = 3;
        _inputBoxLeft = 3;
        _largeBoxHeight = 20;
        _largeBoxWidth = 30;
        
        int titleBottom = 5;
        int spacingBetweenTitleAndLargeBox = 2;
        _largeBoxTop = titleBottom + spacingBetweenTitleAndLargeBox;

        _inputBoxTop = _largeBoxTop + _largeBoxHeight + spacingBetweenTitleAndLargeBox - 2;
        
        _conversationBoxWidth = 100;
        _conversationBoxHeight = _largeBoxHeight;
        _conversationBoxLeft = _inputBoxLeft + _largeBoxWidth + 2;
        _conversationBoxTop = _largeBoxTop;

    }

    public void DisplayTutorial()
    {
        // TODO
    }
}