public struct Tile
{
	private int x;
	private int y;
	private int bordering;
	private bool mine;
	private bool revealed;
	private bool cursor;
	private bool flag;

	public Tile(int x, int y) {
		this.X = x;
		this.Y = y;

		this.Bordering = 0;
		this.Mine = false;
		this.Revealed = false;
		this.Cursor = false;
		this.Flag = false;
	}

	public int X {
		get => x;
		private set {x = value;}
	}
	public int Y {
		get => y;
		private set {y = value;}
	}
	public int Bordering {
		get => bordering;
		set {bordering = value;}
	}
	public bool Mine {
		get => mine;
		set {mine = value;}
	}
	public bool Revealed {
		get => revealed;
		set {revealed = value;}
	}
	public bool Cursor {
		get => cursor;
		set {cursor = value;}
	}
	public bool Flag {
		get => flag;
		set {flag = value;}
	}
}

