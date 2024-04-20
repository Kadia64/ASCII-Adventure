using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class Run {
        public object KeyboardLock = new object();
        public ConsoleKeyInfo? lastKey = new ConsoleKeyInfo();
        public ConsoleKeyInfo keybefore = new ConsoleKeyInfo();

        public void StartGame() {


            
            Vect mapStart = new Vect(50, 5);
            Vect playerSpawn = new Vect(3, 35);

            UserInterface ui = new UserInterface();            
            Map map = new Map(mapStart);
            Player player = new Player(playerSpawn, mapStart);
            EntityMapper entityMap = new EntityMapper(map.map);
            Console.CursorVisible = false;

            Thread keysThread = new Thread(() => ReadKeys());
            keysThread.Start();
            int l = 0;

            while (true) {
                var frameColors = new ConsoleColors(map.GameMap);
                ui.DisplayConsolePosition(player.ConsolePosition);
                ui.DisplayPlayerPosition(player.MapPosition);
                ui.DisplayPlayerArrayPosition(player.Array2DPosition);
                ui.DisplayPlayerDirection(player.currentDirection);

                lock (KeyboardLock) {
                    ConsoleKey? keyInput = player.KeyboardInput(lastKey, map.GameMap);

                    if ((l % 200) == 0) {
                        map.RenderFullMap(frameColors);
                    }

                    player.Draw();
                    lastKey = new ConsoleKeyInfo();
                }


                ++l;
            }

            /*Thread.Sleep(1000);
            map.TestPrint__static();*/

        }
        public void ReadKeys() {
            while (true) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                lock (KeyboardLock) {
                    lastKey = key;
                }
            }
        }
    }
}
