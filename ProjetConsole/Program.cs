using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MyApp
{
    // Question n°11
    public class Person
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Initiales { get; set; }

        public Person(string nom, string prenom, string initiales)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Initiales = initiales;
        }
    }

    // Questions n°12 et n°14
    public class Bureau
    {
        public List<Person> listePersonnes;

        public Bureau()
        {
            listePersonnes = new List<Person>();
        }

        public void AddPersonne(Person personne)
        {
            listePersonnes.Add(personne);
        }

        public void DisplayPersonne()
        {
            foreach (var person in listePersonnes)
            {
                Console.WriteLine($"Nom : {person.Nom}, Prénom : {person.Prenom}, Initiales : {person.Initiales}");
            }

        }

    }

    // Questions n°16, n°17, n°18
    interface ISortie
    {
        public void SortiePersonneBureau(Bureau bureau);
    }

    public class SortieConsole : ISortie
    {
        public void SortiePersonneBureau(Bureau bureau)
        {
            foreach (var person in bureau.listePersonnes)
            {
                Console.WriteLine($"Nom : {person.Nom}, Prénom : {person.Prenom}, Initiales : {person.Initiales}");
            }
            Console.WriteLine("\n");
        }

    }

    public class SortieFichier : ISortie
    {
        public void SortiePersonneBureau(Bureau bureau)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(bureau.listePersonnes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("personnes.json", jsonString);
                Console.WriteLine("Question n°24");
                Console.WriteLine("La liste des personnes a été écrite dans le fichier personnes.json.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur est survenue lors de l'écriture du fichier : {ex.Message}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Question n°2
            Console.WriteLine("Question n°2");
            int[] arrayNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (int element in arrayNumbers)
            {
                Console.Write(element + " ");
            }

            Console.WriteLine("\n");

            // Questions n°5 :
            List<int> listNumbers = new List<int>();
            listNumbers.AddRange(arrayNumbers);

            // Question n°7
            List<int> listNumbersTwo = new List<int>();
            for (int i = 1; i <= 100; i++)
            {
                listNumbersTwo.Add(i);
            }

            List<int> filteredListNumbersTwo = listNumbersTwo.FindAll(n => n >= 20 && n <= 65);
            Console.WriteLine("Question n°7");
            Console.WriteLine("L'écart-type de 20 à 65 est : " + CalculEcartTypeV2(filteredListNumbersTwo));
            Console.WriteLine("\n");

            // Questions n°9 et n°10
            Console.WriteLine("Questions n°9 et n°10");
            IDictionary<string, string> dictionaryPerson = new Dictionary<string, string>() {
                {"PH", "Pablo, Huston"},
                {"NH", "Nancy, Hermes"},
                {"JCG", "Jean-Charles, Garnier"},
                {"ALM", "Austin, Ligier Monia"}
            };

            foreach (var person in dictionaryPerson)
            {
                string[] filteredName = person.Value.Split(',');
                string firstName = filteredName[0].Trim();
                string lastName = filteredName[1].Trim();

                Console.WriteLine($"Nom : {lastName}, Prénom : {firstName}, Initiales : {person.Key}");
            }

            // Questions n°13 et n°15
            Console.WriteLine("\n");
            Console.WriteLine("Question n°13 et n°15");
            Bureau bureau = new Bureau();
            bureau.AddPersonne(new Person("Huston", "Pablo", "PHu"));
            bureau.AddPersonne(new Person("Hermes", "Nancy", "NH"));
            bureau.AddPersonne(new Person("Garnier", "Jean-Charles", "JCG"));
            bureau.AddPersonne(new Person("Ligier Monia", "Austin", "ALM"));

            bureau.DisplayPersonne();
            Console.WriteLine("\n");

            // Questions n°19
            ISortie sortie = new SortieConsole();
            sortie.SortiePersonneBureau(bureau);

            // Question n°20
            Console.WriteLine("\nQuestion n°20");

            var personnesTrie = bureau.listePersonnes.OrderBy(p => p.Nom);
            foreach (var person in personnesTrie)
            {
                Console.WriteLine($"Nom : {person.Nom}, Prénom : {person.Prenom}, Initiales : {person.Initiales}");
            }

            // Question n°21
            Console.WriteLine("\nQuestion n°21");
            foreach (var person in bureau.listePersonnes)
            {
                if (person.Nom.Contains("t"))
                {
                    Console.WriteLine($"Nom : {person.Nom}, Prénom : {person.Prenom}, Initiales : {person.Initiales}");
                }
            }
            Console.WriteLine("\n");

            // Question n°24
            ISortie sortieFichier = new SortieFichier();

            //Ligne mise en commentaire pour laisser le choix (2) de la création du fichier personnes.json
            //sortieFichier.SortiePersonneBureau(bureau);

            // Questions n°25 à n°27
            Dictionary<int, ISortie> dicoSortie = new Dictionary<int, ISortie>();
            dicoSortie.Add(1, new SortieConsole());
            dicoSortie.Add(2, new SortieFichier());

            // Question n°28
            Console.WriteLine("Question n°28");
            Console.WriteLine("Choisissez une option : ");
            Console.WriteLine("1 : Afficher les personnes dans la console");
            Console.WriteLine("2 : Écrire les personnes dans un fichier JSON");

            // Test d'une valeur non-nulle entrée par l'utilisateur et qui doit correspondre à la bonne clé du dictionnaire dicoSortie, sinon le choix demandé de nouveau.
            while (true)
            {
                string? input = Console.ReadLine();

                if (input != null)
                {
                    if (int.TryParse(input, out int choix))
                    {
                        if (dicoSortie.TryGetValue(choix, out ISortie? sortieChoisie))
                        {
                            sortieChoisie.SortiePersonneBureau(bureau);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Choix invalide.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrez un choix valide (un nombre entier).");
                    }
                }
                else
                {
                    Console.WriteLine("Fin de l'entrée.");
                    break;
                }
            }



            // Appel des fonctions de calculs et affichage des résultats
            double moyenne = CalculMoyenne(arrayNumbers);
            double variance = CalculVariance(arrayNumbers);
            double ecartType = CalculEcartType(arrayNumbers);

            Console.WriteLine("Question n°3");
            Console.WriteLine("La moyenne est : " + moyenne);
            Console.WriteLine("La variance est : " + variance);
            Console.WriteLine("L'écart-type est : " + ecartType);
            Console.WriteLine("\n");

            // Appel des fonctions listes
            double ecartTypeDeux = CalculEcartTypeV2(listNumbers);
            Console.WriteLine("Question n°6");
            Console.WriteLine("L'écart-type V2 de la liste est : " + ecartTypeDeux);
            Console.WriteLine("\n");

        }

        // Question n°3
        public static double CalculMoyenne(int[] numbers)
        {
            int result = 0;
            foreach (int number in numbers)
            {
                result += number;
            }

            double average = (double)result / numbers.Length;
            return average;
        }

        public static double CalculVariance(int[] numbers)
        {
            double moyenne = CalculMoyenne(numbers);
            double variance = 0;

            foreach (int number in numbers)
            {
                variance += Math.Pow(number - moyenne, 2);
            }

            variance = variance / (numbers.Length - 1);
            return variance;
        }

        public static double CalculEcartType(int[] numbers)
        {
            double variance = CalculVariance(numbers);
            double ecartType = Math.Sqrt(variance);
            return ecartType;
        }

        // Question n°6
        public static double CalculEcartTypeV2(List<int> numbers)
        {
            double variance = CalculVariance(numbers.ToArray());
            double ecartType = Math.Sqrt(variance);
            return ecartType;
        }

    }

}