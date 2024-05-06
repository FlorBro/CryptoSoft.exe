using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace testingproject
{
    public class Decrypter
    {
        public static void DecrypterleFichier(string fichierCrypte, string binarykey, string extension)
        {
            int cle = (int)Convert.ToInt64(binarykey, 2);
            try
            {
                byte[] contenu = File.ReadAllBytes(fichierCrypte);

                for (int i = 0; i < contenu.Length; i++)
                {
                    contenu[i] = (byte)(contenu[i] ^ cle);
                }

               
                File.WriteAllBytes(fichierCrypte, contenu);

                Console.WriteLine("Fichier decrypté avec succès. Chemin du fichier decrypté : " + fichierCrypte);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
           
        }
    }
}
