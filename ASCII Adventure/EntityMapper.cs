using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class EntityMapper {
        
        public List<S_Enemy> s_Enemies = new List<S_Enemy>();
        private char[,] staticMap;
        public EntityMapper(char[,] startMap) {
            this.staticMap = startMap;
            StoreEntityStartPositions();
        }
        public void StoreEntityStartPositions() {
            for (int i = 0; i < staticMap.GetLength(0); ++i) {
                for (int j = 0; j < staticMap.GetLength(1); ++j) {
                    var StartPosition = new Vect(j, i);

                    if (staticMap[i, j] == 'S') {
                        s_Enemies.Add(new S_Enemy(StartPosition));
                        //Console.WriteLine(j + " " + i);
                    }
                }
            }
        }
    }
    public abstract class Entity {
        public Vect StartPosition { get; set; }

        public Entity(Vect startPosition) {
            this.StartPosition = startPosition;
        }
        public abstract void Draw();
    }
    public class S_Enemy : Entity {
        public Vect StartPosition { get; set; }

        // defines where they stop
        public Vect BorderLeft { get; set; } 
        public Vect BorderRight { get; set; }
        public Direction StartDirection { get; set; }
        public S_Enemy(Vect startPosition) : base(startPosition) {
            StartPosition = startPosition;
            StartDirection = GetStartDirection();
        }
        public Direction GetStartDirection() {            
            var leftDist = ConsoleManager.DistanceToWallLeft(Map.map, '█', StartPosition.X, StartPosition.Y);
            var rightDist = ConsoleManager.DistanceToWallRight(Map.map, '█', StartPosition.X, StartPosition.Y);

            int X_position = StartPosition.X;
            int left_number = leftDist.Y;
            int right_number = rightDist.Y;

            int distLeft = Math.Abs(left_number - X_position);
            int distRight = Math.Abs(right_number - X_position);

            if (distLeft < distRight) {
                return Direction.LEFT;
            } else if (distLeft > distRight) {
                return Direction.RIGHT;
            } else {
                return Direction.RIGHT;
            }
        }
        public override void Draw() {
            
        }
    }
}
