using System;

namespace TerminalMinesweeper;

public static class Draw
{
	public static void Init(int sizeX, int sizeY) {
		for (int i = 0; i < sizeX; i++) {
			for (int k = 0; k < sizeY; k++) {
				Console.Write(" ---");
			}
			Console.Write("\n");
			for (int j = 0; j < sizeY; j++) {
				Console.Write("|");
				Console.Write("   ");
			}
			Console.Write("|\n");
		}
		for (int k = 0; k < sizeY; k++) {
			Console.Write(" ---");
		}
		Console.Write("\n");
	}

	public static void DrawBoard(Tile[,] board) {
		Console.Clear();
		//Console.WriteLine($"{board.Length / board.GetLength(0)} , {board.GetLength(0)}");
		for (int i = 0; i < board.GetLength(0); i++) {
			for (int k = 0; k < board.GetLength(0); k++) {
				Console.Write(" ---");
			}
			Console.Write("\n");
			for (int j = 0; j < board.GetLength(0); j++) {
				Console.Write("|");
				Tile tile = board[i, j];
				string flag = tile.Flag ? "F" : " ";
				if (tile.Revealed) {
					Console.Write(" " + (tile.Cursor ? "+" : (tile.Bordering == 0 ? "-" : tile.Bordering)) + " ");
				} else {
					Console.Write(" " + (tile.Cursor ? "+" : flag) + " ");
				}
				/*if (tile.Mine) {
					Console.Write($"{cursor} M");
				} else {
					Console.Write($"{cursor} {tile.Bordering}");
				}*/
			}
			Console.Write("|\n");
		}
		for (int k = 0; k < board.GetLength(0); k++) {
			Console.Write(" ---");
		}
		Console.Write("\n");
	}
}

