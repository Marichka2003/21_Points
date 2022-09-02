using System;
using System.Collections.Generic;

namespace _21_points
{
    class Program
    {
        class Player
        {
            // Create a list for player
            private List<int> players_card = new List<int>();
            private int[,] deck;
            private int k = 0;
            private int summ = 0;
            private static int PNG = 1;//порядковий номер гравця
            private static int player;
            private static int fill = 0;
            private static int max = -999;
            private static int vinner = 0;
            private static int true_vinner = 0;
            private List<int>[] player_fill = new List<int>[6];
            private List<int>[] player_result = new List<int>[6];
            private static int draw = 0;

            // Classe`s Constructor for transfer two-dimensional array
            public Player(int[,] deck, int playerr)
            {
                this.deck = deck;
                player = playerr;
                for (int i = 0; i < player; i++)
                {
                    player_fill[i] = new List<int>();
                }
            }

            // Random selection of one card for the player
            public void More()
            {
                Random random = new Random();
                int i = random.Next(0, 8);
                int j = random.Next(0, 3);
                // Щоб не було більше 4 однакових карт 
                while (deck[i, j] == 0)
                {
                    i = random.Next(0, 8);
                    j = random.Next(0, 3);
                }
                players_card.Add(deck[i, j]);
                deck[i, j] = 0;
                k++;   
            }

            // копіюємо в новий масив для подальшого очищення списку
            public void Filling()
            {
                foreach (int i in players_card)
                {
                    player_fill[fill].Add(i);
                }
                players_card.Clear();
                fill++;
            }

            // Sum up
            public void Summarise()
            {     
                for (int i = 0; i < k; i++)
                {
                    Console.Write(players_card[i] + " ");
                }
                Console.WriteLine();
                foreach (int i in players_card)
                {
                    summ += i;
                }
                Console.Write($"our summ : {summ}");
                Console.WriteLine();
                vinner++;
                //Для знаходження вінера який ~ до 21
                if (summ == 21)
                {
                    max = 21;
                    true_vinner = vinner;
                }
                else if (summ < 21 && summ > max)
                {
                    max = summ;
                    true_vinner = vinner;
                }
                else if (summ > 21 && (summ < max || max == 0))
                {
                    max = summ;
                    true_vinner = vinner;
                }
                else if (max == -999)
                {
                    max = summ;
                    true_vinner = vinner;
                }
            }

            //Output max
            public void Output()
            {
                for(int i = 0; i < vinner; i++)
                {
                    if(max == Convert.ToInt32(player_result[i]))
                    {
                        draw++;
                    }
                }
                if (draw >= 2)
                {
                    Console.Write("Draw between:");
                    for(int i = 0; i < vinner; i++)
                    {
                        if (max == Convert.ToInt32(player_result[i]))
                        {
                            Console.Write($" player number {i+1}");
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"A biggest summ : {true_vinner}:{max} ");
                }
                
                Filling();
            }
            public void Card()
            {
                Console.WriteLine($"Player : {PNG}");
                Console.Write("Your cards : ");
                foreach (var i in players_card)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
            public void Increase()
            {
                PNG++;
            }
            public void Clean()
            {
                max = 0;
                summ = 0;
                PNG = 1;
                vinner = 0;
                for (int i = 0; i < player ;i++) 
                {
                    player_fill[i].Clear();   
                }
                fill = 0;
            }
        }
        
        //Menu
        static void Menu()
        {          
            Console.Write("Enter the number of players : ");
            int player = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Clear();
            Player[] players = new Player[player];
            int[,] deck = new int[9, 4];
            int card = 6;
            // Fill array
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // тут 10 має бути card
                    deck[i, j] = 10;
                }
                card++;
                if (card == 11)
                {
                    card = 2;
                }
                else if (card == 5)
                {
                    card = 11;
                }
            }
            // cтворення масиву обєктів
            for (int i = 0; i < player; i++)
            {
                players[i] = new Player(deck,player);
            }
            int index = 0;
            bool ifTrue = true;

            while (ifTrue)
            {
                Console.WriteLine("If you want to one more card , enter the number - 1");
                Console.WriteLine("If you get enough points , enter the number - 2");
                Console.WriteLine("If you want get result , enter the number - 3");
                Console.Write("Your choice : ");
                int start = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (start)
                {
                    case 1:                        
                        players[index].More();
                        players[index].Card();
                        break;
                    case 2:                       
                        players[index].Increase();
                        index++;
                        if (index == player)
                        {
                            Console.WriteLine("There isn't another player");
                            index--;
                            break;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < player; i++)
                        {
                            Console.Write($"Result player number {i + 1} : ");
                            players[i].Summarise();    
                        }
                        players[player - 1].Output();
                        goto point;

                    case 0:
                        point:
                        Console.WriteLine("Return for main menu - 0");
                        Console.Write("Your choice : ");
                        start = Convert.ToInt32(Console.ReadLine());   
                        Console.Clear();
                        players[index].Clean();
                        ifTrue = false;
                        break;
                    default:
                        
                        break;
                }
            }
        }
        static void Main(string[] args)
        { 
            // Create array with number of cards and suit
  
            bool isTrue = true;

            // Menu
            do
            {
                Console.WriteLine("Start game, enter the number - 1");
                Console.WriteLine("Game over , enter the number - 2");
                Console.Write("Your choice : ");
                int menu = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine();
                
                switch (menu)
                {
                    case 1:
                        Menu();
                        break;
                    case 2:
                        isTrue = false;
                        break;
                    default:
                        break;
                }
            } while (isTrue);
        }    
    }
}
