using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Buffers;
using System.Security.Cryptography.X509Certificates;




namespace SnakeGame
{
    public class Program
    {
        static void Main()
        {    
            Console.WriteLine("Введите размер поля: ");
            int placeSize = Convert.ToInt32(Console.ReadLine());   
            bool game = true;
            int[] fruitCoordinates = [0,0];
            DateTime lastMove = DateTime.Now;
            List<int[]> snake = new List<int[]>();
            snake.Add(new int[] { placeSize / 2, placeSize / 2 });;
            string direction = "";
            Console.Clear();
            
            
            spawnFruit(fruitCoordinates, placeSize);
            Draw(snake, fruitCoordinates, placeSize);
            

            while (game)
            {
                


                if (Console.KeyAvailable)
                {
                    var button = Console.ReadKey(true);
                    
                    

                    if(button.KeyChar == 'w') direction = "up";
                    if(button.KeyChar == 's') direction = "down";
                    if(button.KeyChar == 'd') direction = "right";
                    if(button.KeyChar == 'a') direction = "left";
                    
                    if(button.KeyChar == 'p') game = false;
                }
                


                if((DateTime.Now - lastMove).TotalSeconds >= 1)
                {
                    lastMove = DateTime.Now;

                    int[] head = snake[0];
                    int newY = head[0];
                    int newX = head[1];

                    if (direction == "up") newY--;
                    if (direction == "down") newY++;
                    if (direction == "left") newX--;
                    if (direction == "right") newX++;
                    
                    
                       
                    int[] newHead = [newY, newX];
                    snake.Insert(0, newHead);


                    if(newY == fruitCoordinates[0] && newX == fruitCoordinates[1])
                    {
                        spawnFruit(fruitCoordinates, placeSize);
                    }
                    else
                    {
                        snake.RemoveAt(snake.Count - 1);
                    }
                    

                    Draw(snake, fruitCoordinates, placeSize);
                }
                




                if(snake[0][0] == placeSize - 1 || snake[0][1] == placeSize
                || snake[0][0] == 0 || snake[0][1] == 0 || SelfCrush(snake))
                {
                    Console.Clear();
                    Console.WriteLine("Ты рил проебал??? Ну ты и бездарь...");
                    game = false;
                }
                    
            }
        }

        public static void Draw(List<int[]> _snake, int[] _fruitCoordinates, int _placeSize)
        {
            Console.Clear();
            for(int i = 0; i < _placeSize; i++)
            {
                if(i == 0 || i == _placeSize - 1)
                {
                        for(int j = 0; j < _placeSize; j++)
                        {
                        Console.Write("##");
                        }
                }
                else
                {
                    Console.Write("#");

                    for(int j = 1; j < _placeSize; j++)
                    {
                        bool printed = false;
                        if(i == _fruitCoordinates[0] && j == _fruitCoordinates[1])
                        {
                            Console.Write("Q" + " ");
                            printed = true;
                        }
                        for (int k = 0; k < _snake.Count; k++)
                        {
                            if (_snake[k][0] == i && _snake[k][1] == j)
                            {
                                if (k == 0)
                                    Console.Write("D "); // голова
                                else
                                    Console.Write("o "); // хвост

                                printed = true;
                                break;
                            }
                        }
                        
                        if(!printed)
                        Console.Write("  ");
                    }
                    Console.Write("#");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Координаты игрока x= " + _snake[0][1] + ", y= " + _snake[0][0]);
            Console.WriteLine("Координаты фрукта x= " + _fruitCoordinates[1] + ", y= " + _fruitCoordinates[0]);
            
        }

        public static int[] spawnFruit(int[] _fruitCoordinates, int _placeSize)
        {
            Random random = new Random();
            Random random1 = new Random();
            int num = random.Next(1, _placeSize - 1);
            int num1 = random1.Next(1, _placeSize - 1);
            _fruitCoordinates[0] = num;
            _fruitCoordinates[1] = num1;


            return _fruitCoordinates;
        }

        public static bool Eat(int[] _fruitCoordinates, List<int[]> _snake)
        {
            if(_fruitCoordinates[0] == _snake[0][0] && _fruitCoordinates[1] == _snake[0][1])
            return true;
            
            return false;
        }

        static bool SelfCrush(List<int[]> snake)
        {
            int headY = snake[0][0];
            int headX = snake[0][1];

            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i][0] == headY && snake[i][1] == headX)
                    return true;
            }

            return false;
        }   


        

    }
}
