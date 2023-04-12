using AsciiArt;

namespace jeu_du_pendu
{
    internal class Program
    {
        static void AfficherMot(string mot, List<char> lettres)

        {

            for (int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre + " ");
                } else
                {
                    Console.Write("_ ");
                }

            }
            Console.WriteLine();
        }

        static char DemanderUneLettre()
        {
            char lettre = 'A';
            bool on = true;
            while (on)
            {
                try
                {
                    Console.Write("Entrez une lettre: ");
                    lettre = char.Parse(Console.ReadLine());
                    lettre = char.ToUpper(lettre);
                    on = false;


                } catch
                {
                    Console.WriteLine("ERREUR: Veuillez entrer une seule lettre !");
                }

            }
            return lettre;


        }
        static void DevinerMot(string mot)
        {
            List<char> lettres = new List<char> { };
            List<char> lettresNonDevinee = new List<char> { };
            const int NB_VIES = 6;
            int vieRestantes = NB_VIES;

            while (vieRestantes != 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES - vieRestantes]);
                AfficherMot(mot, lettres);
                Console.WriteLine();
                char lettre = DemanderUneLettre();
                Console.Clear();
                if (mot.Contains(lettre))
                {
                    lettres.Add(lettre);
                    if (lettres.Count + 1 == mot.Length)
                    {
                        Console.WriteLine("Super vous avez gagner !");
                        break;
                    }

                    Console.WriteLine("Super cette lettre est dans le mot !");

                } else
                {
                    if (!lettresNonDevinee.Contains(lettre))
                    {
                        lettresNonDevinee.Add(lettre);
                        vieRestantes--;
                    }

                    if (vieRestantes == 0)
                    {
                        Console.WriteLine(Ascii.PENDU[NB_VIES - vieRestantes]);
                        Console.WriteLine("Game Over! Le mot etait: " + mot);
                        break;
                    }
                    Console.WriteLine("Ressayez il vous reste " + vieRestantes + " chance !");

                }

                if (lettresNonDevinee.Count > 0) Console.WriteLine("le mot ne contient pas : " + string.Join(", ", lettresNonDevinee));




            }

        }

        static string[] ChargerLesMots(String nomFichier)
        {
            try 
            {
                return File.ReadAllLines(nomFichier);
            } catch (Exception e)
            {
                Console.WriteLine("Erreur de lecture du fichier : " + nomFichier + " (" + e.Message + ")");
            } 
            return null;
        }

        static bool DemanderDeRejouer()
        {
            Console.WriteLine("Voulez-vous rejouer (o/n) : ");
            char reponse = DemanderUneLettre();
            if ((reponse == 'o') || (reponse == 'O'))
            {
                return true;
            }
            else if ((reponse == 'n') || (reponse == 'N'))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : Vous devez répondre avec o ou n");
                return DemanderDeRejouer();
            }
        }

        static void Main(string[] args)
        {
            var mots = ChargerLesMots("mots.txt");

            if((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("La liste de mots est vide");
            }else
            {
                while (true)
                {
                    Random r = new Random();
                    int i = r.Next(mots.Length);
                    string mot = mots[i].Trim().ToUpper();
                    DevinerMot(mot);
                    if (!DemanderDeRejouer())
                    {
                        break;
                    }
                    Console.Clear();
                }
                Console.WriteLine("Merci et à bientôt.");
            }
            
        }
    }
}