using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class Player {

        public int posX = 0;
        public int posY = 0;
        public char PlayerLeft = '<';
        public char PlayerRight = '>';
        public char PlayerUp = '^';
        public char PlayerDown = 'v';
        private ConsoleKey currentKeyPressed;
        private Direction currentDirection;
        private int StartX, StartY;

        public Player() {
            StartX = 5;
            StartY = 10;
        }
        public void Draw() {

            Pos(posX, posY);            

            switch (currentDirection) {
                case Direction.UP:
                    Console.Write(PlayerUp);
                    Pos(posX, posY + 1);
                    Console.Write(" ");
                    break;
                case Direction.DOWN:
                    Console.WriteLine(PlayerDown);
                    Pos(posX, posY - 1);
                    Console.Write(" ");
                    break;
                case Direction.LEFT:
                    Console.WriteLine(PlayerLeft);
                    Pos(posX + 1, posY);
                    Console.Write(" ");
                    break;
                case Direction.RIGHT:
                    Console.WriteLine(PlayerRight);
                    Pos(posX - 1, posY);
                    Console.WriteLine(" ");
                    break;
            }
            Pos(posX, posY);
        }
        public bool CheckTileCollisions() {
            return true;
        }

        public ConsoleKey? KeyboardInput() {
            ConsoleKeyInfo key = Console.ReadKey(true);
            currentKeyPressed = key.Key;

            switch (currentKeyPressed) {
                case ConsoleKey.W:
                    if (CheckTileCollisions()) {}
                    --posY;
                    currentDirection = Direction.UP;
                    break;
                case ConsoleKey.S:
                    ++posY;
                    currentDirection = Direction.DOWN;
                    break;
                case ConsoleKey.A:
                    --posX;
                    currentDirection = Direction.LEFT;
                    break;
                case ConsoleKey.D:
                    ++posX;
                    currentDirection = Direction.RIGHT;
                    break;
                default:
                    return null;
            }
            return currentKeyPressed;
        }
        public void Pos(int x, int y) => Console.SetCursorPosition(x + StartX, y + StartY);
        public enum Direction {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
    }    
}
