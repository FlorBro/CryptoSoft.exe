using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingproject
{
    public class Crypter
    {
        public static void CrypterFichier(string cheminFichier, string binarykey, string extension)
        {
            int cle = (int)Convert.ToInt64(binarykey, 2);
            try
            {
                byte[] contenu = File.ReadAllBytes(cheminFichier);

                for (int i = 0; i < contenu.Length; i++)
                {
                    contenu[i] = (byte)(contenu[i] ^ cle);
                }

                //string fichierCrypte = cheminFichier.Replace(extension, "_crypte" + extension);
                File.WriteAllBytes(cheminFichier, contenu);

                Console.WriteLine("Fichier crypté avec succès. Chemin du fichier crypté : " + cheminFichier);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
    }
}
