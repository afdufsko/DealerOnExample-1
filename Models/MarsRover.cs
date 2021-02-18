using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerOnExample.Models
{
    public class MarsRover
    {
        public MarsRover(int id)
        {
            this.Id = id;
            this.Direction = 'N';
            this.X = 0;
            this.Y = 0;
        }
        public MarsRover(int Id, char Direction, int X, int Y)
        {
            this.Id = Id;
            this.Direction = Direction;
            this.X = X;
            this.Y = Y;
        }
        public int Id { get; set; }
        public char Direction { get; set; }


        public int X { get; set; }
        public int Y { get; set; }

        public void rotateRight()
        {
            switch (this.Direction)
            {
                case 'N':
                    this.Direction = 'E';
                    break;
                case 'E':
                    this.Direction = 'S';
                    break;
                case 'S':
                    this.Direction = 'W';
                    break;
                case 'W':
                    this.Direction = 'N';
                    break;
                default:
                    Console.WriteLine("Error Rotating Rover");
                    break;
            }
        }
        public void rotateLeft()
        {
            switch (this.Direction)
            {
                case 'N':
                    this.Direction = 'W';
                    break;
                case 'E':
                    this.Direction = 'N';
                    break;
                case 'S':
                    this.Direction = 'E';
                    break;
                case 'W':
                    this.Direction = 'S';
                    break;
                default:
                    Console.WriteLine("Error Rotating Rover");
                    break;
            }
        }
        public void moveForward()
        {
            switch (this.Direction)
            {
                case 'N':
                    this.Y++;
                    break;
                case 'E':
                    this.X++;
                    break;
                case 'S':
                    this.Y--;
                    break;
                case 'W':
                    this.X--;
                    break;
                default:
                    Console.WriteLine("Error Rotating Rover");
                    break;
            }
        }
    }
}
