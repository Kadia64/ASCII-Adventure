using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class ConsoleManager {
        public virtual void Pos(int x, int y) => Console.SetWindowPosition(x, y);
    }
}
