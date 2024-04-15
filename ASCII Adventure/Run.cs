using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class Run {

        public void StartGame() {

            Map map = new Map();
            Player player = new Player();


            while (true) {


                player.Draw();
            }

            /*Thread.Sleep(1000);
            map.TestPrint__static();*/

        }
    }
}
