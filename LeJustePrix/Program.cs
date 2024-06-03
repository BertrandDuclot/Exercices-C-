using System;
using System.Diagnostics;

namespace JustePrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int prixJuste = random.Next(1, 101);
            int nombreDeCoups = 0;

            Console.WriteLine("Bienvenue dans le jeu du Juste Prix.\n");

            // Utilisation de la classe Stopwatch pour commencer le chronomètre
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                Console.Write("Entrez un prix entre 1 et 100 : ");
                int prixSaisi;

                if (!int.TryParse(Console.ReadLine(), out prixSaisi) || prixSaisi < 1 || prixSaisi > 100)
                {
                    continue;
                }

                nombreDeCoups++;

                if (prixSaisi < prixJuste)
                {
                    Console.WriteLine("Trop petit.");
                }
                else if (prixSaisi > prixJuste)
                {
                    Console.WriteLine("Trop grand.");
                }
                else
                {
                    Console.WriteLine($"Vous avez trouvé le juste prix en {nombreDeCoups} coups.");
                    break;
                }
            }

            stopwatch.Stop();
            TimeSpan tempsTotal = stopwatch.Elapsed;

            Console.WriteLine($"Temps écoulé : {tempsTotal.Minutes} minutes et {tempsTotal.Seconds} secondes.");

            Console.ReadLine();
        }
    }
}
