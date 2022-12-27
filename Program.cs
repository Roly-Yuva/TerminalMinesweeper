using System;

namespace TerminalMinesweeper;

class Program
{
	static void Main(string[] args) {
		int sizeX;
		int sizeY;

		if (args.Length == 1) {
			sizeX = Convert.ToInt32(args[0]);
			sizeY = Convert.ToInt32(args[0]);
		//} else if (args.Length == 2) {
		//	sizeX = Convert.ToInt32(args[0]);
		//	sizeY = Convert.ToInt32(args[1]);
		//	fix later so that the program can properly generate non-square boards
		} else {
			sizeX = 10;
			sizeY = 10;
		}

		var _ = new Logic(sizeX, sizeY);

		while (_.Game) {
			Draw.DrawBoard(_.board);
			Tutorial();
			_.MoveCursor();
			if (_.NumRevealed == _.NumGood) {
				Console.WriteLine("Congratulations!");
				break;
			}
		}
	}

	static void Tutorial() {
		Console.WriteLine("Use arrow keys to move the cursor (+)");
		Console.WriteLine("Use 'enter' to select a tile and reveal it");
		Console.WriteLine("Use 'F' to place a flag, preventing you from selecting that tile");
	}
}

