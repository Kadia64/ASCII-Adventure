using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class ConsoleManager {        
        public virtual void Pos(int x, int y) => Console.SetWindowPosition(x, y);
        public static Vect DistanceToWallLeft(char[,] staticMap, char target, int x, int y) {
            if (staticMap[y, x] == target) {
                return new Vect(y, x);
            }
            return DistanceToWallLeft(staticMap, '█', x - 1, y);
        }
        public static Vect DistanceToWallRight(char[,] staticMap, char target, int x, int y) {
            if (staticMap[y, x] == target) {
                return new Vect(y, x);
            }
            return DistanceToWallRight(staticMap, '█', x + 1, y);
        }
    }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class ConsoleColors {

        private char[,]? map;
        private Vect Position;
        public ConsoleColors(char[,] gameMap) { 
            this.map = gameMap;
        }
        public void GetCoords(Vect position) {
            this.Position = position;
        }

        public static void PlayerColor() {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        public void CheckPointColor() {
            if (map[Position.Y, Position.X] == 'C') {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;
            }
        }
        public void LifeColor() {
            if (map[Position.Y, Position.X] == '+') {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }
        public void AmmoColor() {
            if (map[Position.Y, Position.X] == 'A') {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
        public void Turret_Color() {
            if (map[Position.Y, Position.X] == '-' || map[Position.Y, Position.X] == '|') {
                Console.ForegroundColor = ConsoleColor.Red;
            }
        }
        public void S_Color() {            
            if (map[Position.Y, Position.X] == 'S') {
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
        public void Percent_Color() {
            if (map[Position.Y, Position.X] == '%') {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
