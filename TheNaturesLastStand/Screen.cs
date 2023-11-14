public class Screen
{
    Point2D Command_Window_Position = new Point2D(2, 27);
    Point2D Input_Window_Position = new Point2D(2, 33);
    Point2D Center = new Point2D(43, 16);
    string[] Screen_Buffer;

    public Screen() { }

    //Initialize Screen
    public void Start()
    {
        Clear_Buffer();
        Update();
    }

    //Print out the screen buffer
    public void Update()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < Screen_Buffer.Length; i++)
        {
            Console.WriteLine(Screen_Buffer[i]);
        }
        Console.SetCursorPosition(Input_Window_Position.X, Input_Window_Position.Y);
    }

    //Fill the buffer with just the outlines of the GUI
    private void Clear_Buffer()
    {
        string Title = @"  _   _       _                  _       _           _         _                  _ 
 | \ | |     | |                ( )     | |         | |       | |                | | 
 |  \| | __ _| |_ _   _ _ __ ___|/ ___  | | __ _ ___| |_   ___| |_ __ _ _ __   __| | 
 | . ` |/ _` | __| | | | '__/ _ \ / __| | |/ _` / __| __| / __| __/ _` | '_ \ / _` | 
 | |\  | (_| | |_| |_| | | |  __/ \__ \ | | (_| \__ \ |_  \__ \ || (_| | | | | (_| | 
 |_| \_|\__,_|\__|\__,_|_|  \___| |___/ |_|\__,_|___/\__| |___/\__\__,_|_| |_|\__,_| 
                                                                                     
 ┌──────────────────┐                                                                
 │     Inventory    │                                                                
 ├──────────────────┤                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 │                  │                                                                
 └──────────────────┘                                                                
 ┌─────────────────────────────────────────────────────────────────────────────────┐ 
 │                                                                                 │ 
 │                                                                                 │ 
 │                                                                                 │ 
 │                                                                                 │ 
 │                                                                                 │ 
 ├─────────────────────────────────────────────────────────────────────────────────┤ 
 │                                                                                 │ 
 └─────────────────────────────────────────────────────────────────────────────────┘ ";

        Screen_Buffer = Title.Split('\n');
    }

    //Return a string with the user's command
    public string ReadCommand()
    {
        Console.SetCursorPosition(Input_Window_Position.X, Input_Window_Position.Y);
        Console.Write(new string (' ', Screen_Buffer[0].Length - 4));
        Console.SetCursorPosition(Input_Window_Position.X, Input_Window_Position.Y);

        return Console.ReadLine();
    }

    //Put some text onto the command window
    public void Write_To_Command_Window(string text)
    {
        int Width = 83;
        string Combined_String = Remove_New_Lines(text, Width);
        Fill_Context_Into_Buffer(Command_Window_Position, 81, 6, Combined_String);
        Update();
        Wait_For_Any_Key();
    }

    //fill the text contents of the given object and adds them to the buffer
    private void Fill_Context_Into_Buffer(Point2D Position, int Box_Width, int Box_Hight, string text)
    {
        string context;
        for (int y = 0; y < Box_Hight - 1; y++)
        {
            int offset = (Box_Width) * y;

            string line;

            if (offset + (Box_Width) <= text.Length)
            {
                line = text.Substring(offset, Box_Width);
            }
            else if (offset < text.Length)
            {
                line = text.Substring(offset, text.Length - offset);
                line += new string(' ', Box_Width - line.Length);
            }
            else
            {
                line = new string(' ', Box_Width);
            }

            context = '│' + line + '│';
            Insert_Into_Buffer(new Point2D(Position.X, Position.Y + y), context);
        }
    }

    //Draw a text box on top of the screen without adding it to the buffer
    public void Draw_Box(Point2D Position, int Box_Width, int Box_Hight, string text)
    {
        string Upper_Edge = '┌' + new string('─', Box_Width) + '┐';
        string Lower_edge = '└' + new string('─', Box_Width) + '┘';

        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(Upper_Edge);
        Console.SetCursorPosition(Position.X, Position.Y + 1);

        if (text.Length <= (Box_Width) * (Box_Hight))
        {
            Fill_Context_Over_Buffer(Box_Width, Box_Hight, text);
        }

        Console.Write(Lower_edge);

        Wait_For_Any_Key();
    }

    //A shorthand version of the Draw_Box() function that automatically centers and calcuates the width of the box
    public void Draw_Box(string text)
    {
        int Box_Width = Calc_Max_Line_Length(text) - 2;
        string Combined_String = Remove_New_Lines(text, Box_Width);


        int Box_Hight = (int)MathF.Ceiling((float)text.Length / (float)Box_Width) + 2;
        Point2D Position = new Point2D(Center.X - Box_Width / 2, Center.Y - Box_Hight / 2);
        Draw_Box(Position, Box_Width - 2, Box_Hight, Combined_String);
    }

    //Fill the text contents of the given object and prints them to the screen
    private void Fill_Context_Over_Buffer(int Box_Width, int Box_Hight, string text)
    {
        string context = "";
        for (int y = 0; y < Box_Hight - 1; y++)
        {
            int offset = (Box_Width) * y;

            string line = "";

            if (offset + (Box_Width) <= text.Length)
            {
                line = text.Substring(offset, Box_Width);
            }
            else if (offset < text.Length)
            {
                line = text.Substring(offset, text.Length - offset);
                line += new string(' ', Box_Width - line.Length);
            }
            else
            {
                line = new string(' ', Box_Width);
            }

            context = '│' + line + '│';

            Console.Write(context);
            Console.SetCursorPosition(Console.CursorLeft - context.Length, Console.CursorTop + 1);
        }
    }

    //showcase a premade "Game Over" text box
    public void GameOver()
    {
        string Game_Over_Text = 
@"  ________                        ________                     
 /  _____/_____    _____   ____   \_____  \___  __ ___________ 
/   \  ___\__  \  /     \_/ __ \   /   |   \  \/ // __ \_  __ \
\    \_\  \/ __ \|  Y Y  \  ___/  /    |    \   /\  ___/|  | \/
 \______  (____  /__|_|  /\___  > \_______  /\_/  \___  >__|   
        \/     \/      \/     \/          \/          \/       
                                                               
                    Thank you for playing!                     ";
        Draw_Box(Game_Over_Text);
    }

    //Utility Functions

    private bool Insert_Into_Buffer(Point2D Position, string text)
    {
        int Command_Window_Width = Screen_Buffer[0].Length;
        if (text.Length <= Command_Window_Width && text.Length + Position.X <= Command_Window_Width)
        {
            Screen_Buffer[Position.Y] = Screen_Buffer[Position.Y].Substring(0, Position.X-1) + text +
                Screen_Buffer[Position.Y].Substring(Position.X + text.Length - 2, Command_Window_Width - (Position.X + text.Length));
            return true;
        }
        return false;
    }

    private void Wait_For_Any_Key()
    {
        Console.ReadKey();
    }

    private int Calc_Max_Line_Length(string text)
    {
        int Current_Line_Length = 0;
        int Max_Line_Length = int.MinValue;

        for (int i = 0; i < text.Length; i++)
        {
            Current_Line_Length++;

            if (text[i] == '\n')
            {
                if(Max_Line_Length < Current_Line_Length)
                {
                    Max_Line_Length = Current_Line_Length;
                }
                Current_Line_Length = 0;
            }
        }

        if(Max_Line_Length <= 0)
        {
            Max_Line_Length = text.Length;
        }

        return Max_Line_Length + 2;
    }

    private string Remove_New_Lines(string text, int Box_Width)
    {
        Box_Width += 2;
        int Current_Line_Length = 0;
        string Combined_String = "";

        for (int i = 0; i < text.Length; i++)
        {
            Current_Line_Length++;
            Combined_String += text[i];
            if (text[i] == '\n')
            {
                Combined_String += new string(' ', Box_Width - Current_Line_Length - 2);
                Current_Line_Length = 0;
            }
        }
        Combined_String = Combined_String.Replace(Environment.NewLine, "");
        return Combined_String;
    }
}
