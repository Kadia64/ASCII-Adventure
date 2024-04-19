using System;
using System.Linq.Expressions;

namespace ASCII_Adventure {
    public class Program {
        static void Main(string[] args) {
            Console.SetWindowSize(200, 90); // Example: set a larger window size
            new Run().StartGame();
        }
    }
}