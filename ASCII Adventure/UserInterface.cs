using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class UserInterface : ConsoleManager {



        public void DisplayConsolePosition(Vect position) {
            Pos(0, 0);
            Console.Write($"Console Position: X: {position.X}, Y: {position.Y}    ");
        }
        public void DisplayPlayerPosition(Vect position) {
            Pos(0, 1);
            Console.Write($"Map Position: X: {position.X}, Y: {position.Y}    ");
        }
        public void DisplayPlayerArrayPosition(Vect position) {
            Pos(0, 2);
            Console.Write($"Array Position: X: {position.X}, Y: {position.Y}    ");
        }
        public void DisplayPlayerDirection(Direction direction) {
            Pos(0, 3);
            Console.Write($"Direction: {direction}      ");
        }


        public override void Pos(int x, int y) => Console.SetCursorPosition(x, y);

    }
}
