public class Point2D
{
	private int[] position = new int[2];

	public int X
	{
		get { return position[0]; }
		set { position[0] = value; }
	}

    public int Y
    {
        get { return position[1]; }
        set { position[1] = value; }
    }

	public Point2D (int X, int Y)
	{
		position[0] = X;
		position[1] = Y;
	}
}