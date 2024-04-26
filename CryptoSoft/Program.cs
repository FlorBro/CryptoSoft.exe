using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        string binaryKey = GenerateBinaryKey(64);

        //Console.WriteLine("Clé binaire de 64 bits : " + binaryKey);

        // Affichage du tableau généré
        Console.WriteLine(string.Join(" ", binaryKey));
        //Console.WriteLine("Message binaire : " + message);
        string path_test = "C:\\Users\\floba\\OneDrive\\Bureau\\Site\\beethoven.jpg";// "C:\\Users\\floba\\OneDrive\\Bureau\\test"; // modifier pour path entré
        string extension ='.' + path_test.Split('.')[1];
        Console.WriteLine(extension);
        Console.Write(path_test);
        CrypterFichier(path_test, binaryKey, extension); // + ".txt"
        DecrypterleFichier(path_test.Split('.')[0] + "_crypte" + extension, binaryKey, extension);
    }

    static string GenerateBinaryKey(int length)
    {
        Random random = new Random();
        return new string(Enumerable.Range(0, length).Select(_ => (char)('0' + random.Next(2))).ToArray());
    }

    static void CrypterFichier(string cheminFichier, string binarykey, string extension)
    {
        Console.Write(binarykey);
        int cle = (int)Convert.ToInt64(binarykey, 2);
        try
        {
            byte[] contenu = File.ReadAllBytes(cheminFichier);

            for (int i = 0; i < contenu.Length; i++)
            {
                contenu[i] = (byte)(contenu[i] ^ cle);
            }

            string fichierCrypte = cheminFichier.Replace(extension, "_crypte" + extension);
            File.WriteAllBytes(fichierCrypte, contenu);

            Console.WriteLine("Fichier crypté avec succès. Chemin du fichier crypté : " + fichierCrypte);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
        }
    }
    static void DecrypterleFichier(string fichierCrypte, string binarykey, string extension)
        {
            int cle = (int)Convert.ToInt64(binarykey, 2);
            try
            {
                byte[] contenu = File.ReadAllBytes(fichierCrypte);

                for (int i = 0; i < contenu.Length; i++)
                {
                    contenu[i] = (byte)(contenu[i] ^ cle);
            }

                string fichierdeCrypte = fichierCrypte.Replace("_crypte" + extension, "_decrypte" + extension);
                File.WriteAllBytes(fichierdeCrypte, contenu);

                Console.WriteLine("Fichier decrypté avec succès. Chemin du fichier decrypté : " + fichierdeCrypte);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
}
