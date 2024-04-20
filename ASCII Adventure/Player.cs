using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class Player : ConsoleManager {

        public int posX = 0;
        public int posY = 0;
        public int arrayX, arrayY;        
        public Vect MapPosition { get; set; }
        public Vect ConsolePosition { get; set; }
        public Vect Array2DPosition { get; set; }
        public Vect PlayerSpawnOffset;
        public char PlayerLeft = '<';
        public char PlayerRight = '>';
        public char PlayerUp = '^';
        public char PlayerDown = 'v';
        public Direction currentDirection;
        public bool hasMoved = false;
        private ConsoleKey currentKeyPressed;
        private int StartX, StartY;
        public Player(Vect playerSpawn, Vect mapStartPosition) {
            PlayerSpawnOffset = playerSpawn;
            StartX = mapStartPosition.X + playerSpawn.X;
            StartY = mapStartPosition.Y + playerSpawn.Y;
            MapPosition = new Vect(posX, posY);
            ConsolePosition = new Vect(mapStartPosition.X, mapStartPosition.Y);
            Array2DPosition = new Vect(playerSpawn.X - 1, playerSpawn.Y);
        }
        public void Draw() {
            MapPosition.X = posX;
            MapPosition.Y = posY;
            ConsolePosition.X = StartX + posX;
            ConsolePosition.Y = StartY + posY;
            Pos(posX, posY);

            ConsoleColors.PlayerColor();
            switch (currentDirection) {
                case Direction.UP:
                    Console.Write(PlayerUp);
                    Pos(posX, posY + 1);
                    if (hasMoved) Console.Write(" ");
                    break;
                case Direction.DOWN:
                    Console.WriteLine(PlayerDown);
                    Pos(posX, posY - 1);
                    if (hasMoved) Console.Write(" ");
                    break;
                case Direction.LEFT:
                    Console.WriteLine(PlayerLeft);
                    Pos(posX + 1, posY);
                    if (hasMoved) Console.Write(" ");
                    break;
                case Direction.RIGHT:
                    Console.WriteLine(PlayerRight);
                    Pos(posX - 1, posY);
                    if (hasMoved) Console.Write(" ");                    
                    break;
            }
            Console.ResetColor();
            hasMoved = false;
            Pos(posX, posY);
        }
        public bool CheckTileCollisions(char[,] map) {     
            return ((currentDirection == Direction.UP && map[Array2DPosition.Y - 1, Array2DPosition.X] == '█') 
                || (currentDirection == Direction.DOWN && map[Array2DPosition.Y + 1, Array2DPosition.X] == '█')
                || (currentDirection == Direction.LEFT && map[Array2DPosition.Y, Array2DPosition.X - 1] == '█') 
                || (currentDirection == Direction.RIGHT && map[Array2DPosition.Y, Array2DPosition.X + 1] == '█')) ? false : true;
        }
        public ConsoleKey? KeyboardInput(ConsoleKeyInfo? keyInfo, char[,] map) {
            currentKeyPressed = keyInfo.Value.Key;

            switch (currentKeyPressed) {
                case ConsoleKey.W:
                    currentDirection = Direction.UP;
                    if (CheckTileCollisions(map)) {
                        --posY;
                        --Array2DPosition.Y;
                        hasMoved = true;
                    }
                    break;
                case ConsoleKey.S:
                    currentDirection = Direction.DOWN;
                    if (CheckTileCollisions(map)) {
                        ++posY;
                        ++Array2DPosition.Y;
                        hasMoved = true;
                    }
                    break;
                case ConsoleKey.A:
                    currentDirection = Direction.LEFT;
                    if (CheckTileCollisions(map)) {
                        --posX;
                        --Array2DPosition.X;
                        hasMoved = true;
                    }
                    break;
                case ConsoleKey.D:
                    currentDirection = Direction.RIGHT;
                    if (CheckTileCollisions(map)) {
                        ++posX;
                        ++Array2DPosition.X;
                        hasMoved = true;
                    }
                    break;
                default:
                    return null;
            }
            return currentKeyPressed;
        }
        public override void Pos(int x, int y) => Console.SetCursorPosition(x + StartX, y + StartY);        
    }
    public enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
