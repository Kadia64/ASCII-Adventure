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

            EntityMapper entityMap = new EntityMapper(Map.map, mapStart);
            entityMap.ClearDrawnEntities();
            Console.CursorVisible = false;


            //while (true) { }

            Thread keysThread = new Thread(() => ReadKeys());
            keysThread.Start();
            int frame = 0;

            while (true) {
                var frameColors = new ConsoleColors(map.GameMap);
                ui.DisplayConsolePosition(player.ConsolePosition);
                ui.DisplayPlayerPosition(player.MapPosition);
                ui.DisplayPlayerArrayPosition(player.Array2DPosition);
                ui.DisplayPlayerDirection(player.currentDirection);
                ui.TestPrint(entityMap.s_Enemies[4].BorderLeft.ToString(), 4);
                ui.TestPrint(entityMap.s_Enemies[4].BorderRight.ToString(), 5);
                ui.TestPrint(entityMap.s_Enemies[4].Array2DPosition.X.ToString() + " " + entityMap.s_Enemies[0].Array2DPosition.Y.ToString(), 6);                

                lock (KeyboardLock) {
                    ConsoleKey? keyInput = player.KeyboardInput(lastKey, map.GameMap);
                    
                    entityMap.UpdateEntityLogic(ref map, player.Array2DPosition, frame);
                    if ((frame % 200) == 0) {
                        map.RenderFullMap(frameColors);
                    }
                    for (int i = 0; i < entityMap.s_Enemies.Count; ++i) {
                        entityMap.s_Enemies[i].Draw();
                    }

                    player.Draw();                    
                    lastKey = new ConsoleKeyInfo();
                }
                ++frame;
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
