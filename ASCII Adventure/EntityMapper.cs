using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class EntityMapper {
        
        public List<S_Enemy> s_Enemies = new List<S_Enemy>();
        private char[,] staticMap;
        private Vect mapPosition;
        public EntityMapper(char[,] startMap, Vect mapPosition) {
            this.staticMap = startMap;
            this.mapPosition = mapPosition;
            StoreEntityStartPositions();
        }
        public void StoreEntityStartPositions() {
            for (int i = 0; i < staticMap.GetLength(0); ++i) {
                for (int j = 0; j < staticMap.GetLength(1); ++j) {
                    var StartPosition = new Vect(j, i);

                    if (staticMap[i, j] == 'S') {
                        s_Enemies.Add(new S_Enemy(StartPosition, mapPosition));                        
                    }
                }
            }
        }
        public void UpdateEntityLogic(ref Map map, Vect playerPosition, int frame) {
            for (int i = 0; i < s_Enemies.Count; ++i) {
                s_Enemies[i].CheckCollision(ref map, playerPosition);
                s_Enemies[i].Update(ref map, frame);
            }
        }
        public void ClearDrawnEntities() {
            for (int i = 0; i < staticMap.GetLength(0); ++i) {
                for (int j = 0; j < staticMap.GetLength(1); ++j) {
                    if (staticMap[i, j] == 'S') {
                        staticMap[i, j] = ' ';
                    }
                }
            }
        }
    }
    public abstract class Entity {
        public Vect StartPosition { get; set; }
        public Vect CurrentPosition { get; set; }
        public Vect Array2DPosition { get; set; }
        public Entity(Vect startPosition, Vect mapPosition) {
            this.StartPosition = startPosition;
            this.CurrentPosition = startPosition;
            this.Array2DPosition = startPosition;
        }        
        public abstract void Update(ref Map map, int frame);
        public abstract void CheckCollision(ref Map map, Vect playerPosition);
        public abstract void Draw();
    }
    public class S_Enemy : Entity {
        public Vect StartPosition { get; set; }
        public Vect CurrentPosition { get; set; }
        public Vect Array2DPosition { get; set; }
        public int BorderLeft { get; set; } 
        public int BorderRight { get; set; }
        public Direction StartDirection { get; set; }
        public Direction CurrentDirection { get; set; }
        private Vect MapPosition;
        public S_Enemy(Vect startPosition, Vect mapPosition) : base(startPosition, mapPosition) {
            StartPosition = startPosition;
            StartDirection = GetStartDirection();
            CurrentDirection = StartDirection;
            CurrentPosition = startPosition;
            Array2DPosition = startPosition;
            MapPosition = mapPosition;
        }
        public Direction GetStartDirection() {            
            var leftDist = ConsoleManager.DistanceToWallLeft(Map.map, '█', StartPosition.X, StartPosition.Y);
            var rightDist = ConsoleManager.DistanceToWallRight(Map.map, '█', StartPosition.X, StartPosition.Y);
            BorderLeft = leftDist.Y;
            BorderRight = rightDist.Y;

            Console.WriteLine(BorderRight + " " + BorderLeft);

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
        public override void Update(ref Map map, int frame) {

            if ((frame % 80) == 0) {
                map.GameMap[Array2DPosition.Y, Array2DPosition.X - 1] = 'S';
                if (CurrentDirection == Direction.LEFT) {
                    map.GameMap[Array2DPosition.Y, Array2DPosition.X - 1] = ' ';
                    --Array2DPosition.X;
                    if (Array2DPosition.X == BorderLeft + 1) {
                        ++Array2DPosition.X;
                        ChangeDirection();
                    }
                } else if (CurrentDirection == Direction.RIGHT) {
                    map.GameMap[Array2DPosition.Y, Array2DPosition.X - 1] = ' ';
                    ++Array2DPosition.X;
                    if (Array2DPosition.X == BorderRight + 1) {
                        --Array2DPosition.X;               
                        ChangeDirection();
                    }
                }
            }

        }
        public override void CheckCollision(ref Map map, Vect playerPosition) {
            if (playerPosition.Y == Array2DPosition.Y && playerPosition.X == Array2DPosition.X - 1) {
                Console.Clear();
                Console.WriteLine("your not very good at this game");
                Console.SetCursorPosition(0, 0);
                while (true) { }
            }
        }
        public override void Draw() {
            Console.ForegroundColor = ConsoleColor.Green;
            if (CurrentDirection == Direction.LEFT) {
                Pos(Array2DPosition.X, Array2DPosition.Y);
                Console.Write("S");

                if (Array2DPosition.X != BorderRight) {
                    Pos(Array2DPosition.X + 1, Array2DPosition.Y);
                    Console.Write(" ");
                }
            } else if (CurrentDirection == Direction.RIGHT) {
                Pos(Array2DPosition.X, Array2DPosition.Y);
                Console.Write("S");

                if (Array2DPosition.X != BorderLeft + 2) {
                    Pos(Array2DPosition.X - 1, Array2DPosition.Y);
                    Console.Write(" ");
                }
            }
            Console.ResetColor();
        }
        private void ChangeDirection() {
            if (CurrentDirection == Direction.LEFT) {
                CurrentDirection = Direction.RIGHT;
            } else if (CurrentDirection == Direction.RIGHT) {
                CurrentDirection = Direction.LEFT;
            }
        }        
        public void Pos(int x, int y) => Console.SetCursorPosition(x + MapPosition.X, y + MapPosition.Y);
    }
}
