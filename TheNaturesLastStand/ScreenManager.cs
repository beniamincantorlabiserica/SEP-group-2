namespace TheNaturesLastStand;

public class ScreenManager
{
    private int _inputBoxWidth;
    private int _inputBoxHeight;
    private int _inputBoxTop;
    private int _inputBoxLeft;
    
    private int _stateBoxHeight;
    private int _stateBoxTop;
    private int _stateBoxWidth;

    private int _conversationBoxWidth;
    private int _conversationBoxHeight;
    private int _conversationBoxTop;
    private int _conversationBoxLeft;
    
    private int _mapBoxWidth;
    private int _mapBoxHeight;
    private int _mapBoxTop;
    private int _mapBoxLeft;

    public ScreenManager()
    {
        Init();
    }

    private void Init()
    {
        DisplayTitle();
        _inputBoxWidth = 132;
        _inputBoxHeight = 3;
        _inputBoxLeft = 3;
        _stateBoxHeight = 20;
        _stateBoxWidth = 30;
        
        const int titleBottom = 5;
        const int spacingBetweenTitleAndLargeBox = 2;
        _stateBoxTop = titleBottom + spacingBetweenTitleAndLargeBox;

        _inputBoxTop = _stateBoxTop + _stateBoxHeight + spacingBetweenTitleAndLargeBox - 2;
        
        _conversationBoxWidth = 100;
        _conversationBoxHeight = _stateBoxHeight;
        _conversationBoxLeft = _inputBoxLeft + _stateBoxWidth + 2;
        _conversationBoxTop = _stateBoxTop;
        
        _mapBoxWidth = 24;
        _mapBoxHeight = 14;
        _mapBoxLeft = _conversationBoxLeft + _conversationBoxWidth + 2;
        _mapBoxTop = _stateBoxTop;
        UpdateScreen(0, new List<string>(), "", new Location(0, "", "", null), "", 0);

    }

    public void UpdateScreen(int balance, List<string> quests, string biome, Location location, string message, double progress)
    {
        DisplayStatusBox(balance, quests);
        DisplayConversationBox(biome, location.Name, message, progress);
        DisplayMapBox( location.LeftLocation, location.RightLocation, location.UpLocation, location.DownLocation);
        DisplayInputBox();
        SetCursorToInsideInputBox();
    }

    public string ReadCommand()
    {
        return Console.ReadLine() ?? "";
    }

    public void DisplayGameOver(double progress)
    {
        var gameOverString = $"""
                                                                ________                        ________
                                                               /  _____/_____    _____   ____   \_____  \___  __ ___________
                                                              /   \  ___\__  \  /     \_/ __ \   /   |   \  \/ // __ \_  __ \
                                                              \    \_\  \/ __ \|  Y Y  \  ___/  /    |    \   /\  ___/|  | \/
                                                               \______  (____  /__|_|  /\___  > \_______  /\_/  \___  >__|
                                                                      \/     \/      \/     \/          \/          \/
                                                                                                                             
                                                                          Thank you for playing! Progress {progress}%
                              """;
        Console.Clear();
        Console.Write(gameOverString);
    }

    private void DisplayTitle()
    {
        const string title = """
                             
                                                                       _   _       _                  _       _           _         _                  _
                                                                      | \ | |     | |                ( )     | |         | |       | |                | |
                                                                      |  \| | __ _| |_ _   _ _ __ ___|/ ___  | | __ _ ___| |_   ___| |_ __ _ _ __   __| |
                                                                      | . ` |/ _` | __| | | | '__/ _ \ / __| | |/ _` / __| __| / __| __/ _` | '_ \ / _` |
                                                                      | |\  | (_| | |_| |_| | | |  __/ \__ \ | | (_| \__ \ |_  \__ \ || (_| | | | | (_| |
                                                                      |_| \_|\__,_|\__|\__,_|_|  \___| |___/ |_|\__,_|___/\__| |___/\__\__,_|_| |_|\__,_|
                                                                                                                  
                             """;
        Console.Write(title);
    }

    private void DisplayInputBox()
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

    private void DisplayStatusBox(int balance, List<string> quests)
    {
        Console.SetCursorPosition(_inputBoxLeft, _stateBoxTop);
        Console.Write("┌" + new string('─', _stateBoxWidth - 2) + "┐");

        for (int i = 1; i < _stateBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_inputBoxLeft, _stateBoxTop + i);
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
                Console.Write("│" + new string('─', _stateBoxWidth - 2) + "│");
            }
            else if (i == 5)
            {
                if (quests.Count != 0)
                {
                    for (int j = 0; j < quests.Count; j++)
                    {
                        Console.SetCursorPosition(_inputBoxLeft, _stateBoxTop + i + j);
                        if (j + i == _stateBoxHeight - 2)
                        {
                            Console.Write("│" + CenterTextInString("....", 30 - 2) + "│");
                        }
                        else if (j + i > _stateBoxHeight - 2)
                        {
                            
                        }
                        else
                        {
                            Console.Write("│" + CenterTextInString($"{j + 1}.{quests[j]}", 30 - 2) + "│");
                        }
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
                Console.Write("│" + new string(' ', _stateBoxWidth - 2) + "│");
            }
        }

        Console.SetCursorPosition(_inputBoxLeft, _stateBoxTop + _stateBoxHeight - 1);
        Console.Write("└" + new string('─', _stateBoxWidth - 2) + "┘");
    }

    private void DisplayConversationBox(string biome, string location, string conversation, double progress)
    {
        Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop);
        Console.Write("┌" + new string('─', _conversationBoxWidth - 2) + "┐");

        for (int i = 1; i < _conversationBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop + i);
            if (i == 1)
            {
                Console.Write("│" + CenterTextInString($"{biome}", _conversationBoxWidth - 2) + "│");
            } 
            else if (i == 2)
            {
                Console.Write("│" + CenterTextInString($"{location}", _conversationBoxWidth - 2) + "│");
            } 
            else if (i == 3)
            {
                Console.Write("│" + CenterTextInString($"Progress {progress}%", _conversationBoxWidth - 2) + "│");
            }
            else if (i == 4)
            {
                Console.Write("│" + new string('─', _conversationBoxWidth - 2) + "│");
            }
            else if (i == 5)
            {
                if (conversation.Length != 0)
                {
                    List<string> conversationLines = SplitStringIntoLines(conversation, 70);
                    var lineNumber = 0;
                    foreach (var line in conversationLines)
                    {
                        Console.SetCursorPosition(_conversationBoxLeft, _conversationBoxTop + i + lineNumber);
                        Console.Write("│" + CenterTextInString($"{line}", 100 - 2) + "│");
                        lineNumber++;
                    }
                    i += conversationLines.Count - 1;
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
    
    private void DisplayMapBox(Location? left, Location? right, Location? up, Location? down)
    {
        Console.SetCursorPosition(_mapBoxLeft, _mapBoxTop);
        Console.Write("┌" + new string('─', _mapBoxWidth - 2) + "┐");

        for (int i = 1; i < _mapBoxHeight - 1; i++)
        {
            Console.SetCursorPosition(_mapBoxLeft, _mapBoxTop + i);
            if (i == 1)
            {
                Console.Write("│" + new string(' ', _mapBoxWidth - 2) + "│");
            }
            else if (i == 2)
            {
                Console.Write("│" + CenterTextInString($"MAP", _mapBoxWidth - 2) + "│");
            }
            else if (i == 3)
            {
                Console.Write("│" + new string(' ', _mapBoxWidth - 2) + "│");
            }
            else if (i == 4)
            {
                Console.Write("│" + new string('─', _mapBoxWidth - 2) + "│");
            }
            else if (i == 5)
            {
                Console.Write("│" + new string(' ', _mapBoxWidth - 2) + "│");
            }
            else if (i == 6 && up != null)
            {
                Console.Write("|" + CenterTextInString($"[ ]", _mapBoxWidth - 2) + "│");
            }
            else if (i == 7 && up != null)
            {
                Console.Write("│" + CenterTextInString($"\u2191", _mapBoxWidth - 2) + "│");
            }
            else if (i == 8)
            {
                var mapString = "";
                if (left != null)
                {
                    mapString += "[ ] \u2190  ";
                }
                else
                {
                    mapString += "       ";
                }

                mapString += "[X]";
                if (right != null)
                {
                    mapString += "  \u2192 [ ]";
                }
                else
                {
                    mapString += "       ";
                }
                Console.Write("│" + CenterTextInString(mapString, _mapBoxWidth - 2) + "│");
            }
            else if (i == 9 && down != null)
            {
                Console.Write("│" + CenterTextInString($"\u2193", _mapBoxWidth - 2) + "│");
            }
            else if (i == 10 && down != null)
            {
                Console.Write("|" + CenterTextInString($"[ ]", _mapBoxWidth - 2) + "│");
            }
            else
            {
                Console.Write("│" + new string(' ', _mapBoxWidth - 2) + "│");
            }
        }

        Console.SetCursorPosition(_mapBoxLeft, _mapBoxTop + _mapBoxHeight - 1);
        Console.Write("└" + new string('─', _mapBoxWidth - 2) + "┘");
    }

    private void SetCursorToInsideInputBox()
    {
        Console.SetCursorPosition(_inputBoxLeft + 1, _inputBoxTop + 1);
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

        var rows = text.Split('\n');

        foreach (var row in rows)
        {
            text = row;
            while (text.Length > maxCharsPerLine)
            {
                int splitIndex = text.LastIndexOf(' ', maxCharsPerLine);
                if (splitIndex == -1) splitIndex = maxCharsPerLine;

                lines.Add(text.Substring(0, splitIndex));
                text = text.Substring(splitIndex).TrimStart();
            }
            lines.Add(text);
        }
        
        return lines;
    }
}