using System;

namespace TerminalMinesweeper;

public class Logic
{
	private int sizeX;
	private int sizeY;
	private int cursorX;
	private int cursorY;
	private int numMines;
	private int numGood;
	private int numRevealed;
	private bool game;

	private bool boardSet = false;

	public Tile[,] board;

	public Logic(int sizeY, int sizeX) {
		this.SizeX = sizeX;
		this.SizeY = sizeY;

		this.CursorX = 0;
		this.CursorY = 0;

		this.NumMines = (int)Math.Round((sizeX * sizeY) * .15);

		this.Game = true;

		Console.WriteLine(this.NumMines);

		board = new Tile[sizeX, sizeY];

		this.NumGood = this.board.Length - this.NumMines;

		board[0, 0].Cursor = true;
	}

	public int SizeX {
		get => sizeX;
		private set {sizeX = Math.Abs(value);}
	}
	public int SizeY {
		get => sizeY;
		private set {sizeY = Math.Abs(value);}
	}
	public int CursorX {
		get => cursorX;
		private set {
			if (value < 0) {
				cursorX = 0;
			} else if (value > SizeX - 1) {
				cursorX = SizeX - 1;
			} else {
				cursorX = value;
			}
		}
	}
	public int CursorY {
		get => cursorY;
		private set {
			if (value < 0) {
				cursorY = 0;
			} else if (value > SizeY - 1) {
				cursorY = SizeY - 1;
			} else {
				cursorY = value;
			}
		}
	}
	public int NumMines {
		get => numMines;
		private set {numMines = value;}
	}
	public int NumGood {
		get => numGood;
		private set {numGood = value;}
	}
	public int NumRevealed {
		get => numRevealed;
		private set {numRevealed = value;}
	}
	public bool Game {
		get => game;
		private set {game = value;}
	}

	public void Run() {
		PlaceMines();
		SetTileNumbers();

		while (this.board[this.CursorY, this.CursorX].Bordering != 0) {
			ResetMines();
			PlaceMines();
			SetTileNumbers();
		}
	}


	public void MoveCursor() {
		bool validKey = false;

		this.board[this.CursorY, this.CursorX].Cursor = false;

		while (!validKey) {
			ConsoleKeyInfo keyInput = Console.ReadKey(false);

			if (keyInput.Key == ConsoleKey.LeftArrow || keyInput.Key == ConsoleKey.A || keyInput.Key == ConsoleKey.H) {
				this.CursorX--;
				validKey = true;
				break;
			} else if (keyInput.Key == ConsoleKey.RightArrow || keyInput.Key == ConsoleKey.D || keyInput.Key == ConsoleKey.L) {
				this.CursorX++;
				validKey = true;
				break;
			} else if (keyInput.Key == ConsoleKey.UpArrow || keyInput.Key == ConsoleKey.W || keyInput.Key == ConsoleKey.K) {
				this.CursorY--;
				validKey = true;
				break;
			} else if (keyInput.Key == ConsoleKey.DownArrow || keyInput.Key == ConsoleKey.S || keyInput.Key == ConsoleKey.J) {
				this.CursorY++;
				validKey = true;
				break;
			} else if (keyInput.Key == ConsoleKey.Enter) {
				if (!boardSet) {
					Run();
					validKey = true;
					boardSet = true;
				}
				if (!this.board[this.CursorY, this.CursorX].Flag) {
					SelectTile(this.CursorY, this.CursorX);
				}
				validKey = true;
				break;
			} else if (keyInput.Key == ConsoleKey.F) {
				Tile tile = this.board[this.CursorY, this.CursorX];
				this.board[this.CursorY, this.CursorX].Flag = tile.Revealed ? false : !tile.Flag;
				validKey = true;
				break;
			} else {
				validKey = false;
				continue;
			}
		}

		this.board[this.CursorY, this.CursorX].Cursor = true;
	}

	private void SelectTile(int x, int y) {
		bool mine = this.board[x, y].Mine;

		RevealTile(x, y);

		if (mine) {
			GameOver();
		} else {
			Draw.DrawBoard(this.board);
		}
	}

	private void RevealTile(int x, int y) {
		this.NumRevealed = !this.board[x, y].Revealed ? ++this.NumRevealed : this.NumRevealed;
		this.board[x, y].Revealed = true;

		if (this.board[x, y].Bordering == 0) {
			for (int i = -1; i < 2; i++) {
				if (x + i < 0 || x + i > this.SizeX - 1) {
					continue;
				} else {
					for (int j = -1; j < 2; j++) {
						if (y + j < 0 || y + j > this.SizeY - 1) {
							continue;
						} else {
							if (this.board[x + i, y + j].Revealed) {
								continue;
							} else {
								RevealTile(x + i, y + j);
							}
						}
					}
				}
			}
		}
	}

	private void PlaceMines() {
		var rand = new Random();
		for (int i = 0; i < NumMines; i++) {
			int x = rand.Next(SizeX);
			int y = rand.Next(SizeY);

			while (this.board[x, y].Mine) {
				x = rand.Next(SizeX);
				y = rand.Next(SizeY);
			}

			this.board[x, y].Mine = true;
		}
	}

	private void SetTileNumbers() {
		for (int i = 0; i < SizeX; i++) {
			for (int j = 0; j < SizeY; j++) {
				int minesBordering = 0;

				for (int k = -1; k <= 1; k++) {
					if ((i + k < 0) || (i + k >= SizeX)) {continue;}
					for (int l = -1; l <= 1; l++) {
						if ((j + l < 0) || (j + l >= SizeY)) {continue;}
						minesBordering = this.board[i + k, j + l].Mine ? ++minesBordering : minesBordering;
					}
				}
				
				this.board[i, j].Bordering = minesBordering;
			}
		}
	}

	private void ResetMines() {
		for (int i = 0; i < SizeX; i++) {
			for (int j = 0; j < SizeY; j++) {
				this.board[i, j].Mine = false;
			}
		}
	}

	private void GameOver() {
		Console.WriteLine("GAME OVER!");
		this.Game = false;
	}
}
