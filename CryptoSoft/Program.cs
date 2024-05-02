using System;
using System.Linq;
using System.Text;
using testingproject;
using System.Text.Json;

class Program
{
    static void Main()
    {

        //Console.WriteLine("Clé binaire de 64 bits : " + binaryKey);
        Console.WriteLine("Entrez le chemin du dossier");
        string path_test = Console.ReadLine(); //C:\\Users\\floba\\OneDrive\\Bureau\\Test CryptoSoft

        //Console.WriteLine("Message binaire : " + message);
        string[] fichiers = Directory.GetFiles(path_test);
        Console.WriteLine("Crypter : C ou Decrypter: D");
        if (Console.ReadLine() == "C")
        {
            string binaryKey = GenerateBinaryKey(64);
            foreach (string fichier in fichiers)
            {
                string extension = '.' + fichier.Split('.')[1];
                Crypter.CrypterFichier(fichier, binaryKey, extension); // + ".txt"
            }
            keywriter(binaryKey, path_test);
        }
        else
        {
            /////////////////
            string jsonContent = File.ReadAllText(path_test + "\\Key0.json");
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;
            // Utilisation d'un dictionnaire pour stocker les états et leurs valeurs associées
            string key = root.GetProperty("cle").GetString();

            ////////////////////////
            foreach (string fichier in fichiers)
            {
                string extension = '.' + fichier.Split('.')[1];
                Decrypter.DecrypterleFichier(fichier.Split('.')[0] + "_crypte" + extension, key, extension);
            }
        }
    }

    static string GenerateBinaryKey(int length)
    {
        Random random = new Random();
        return new string(Enumerable.Range(0, length).Select(_ => (char)('0' + random.Next(2))).ToArray());
    }
    static string keywriter(string key,string path_test)
    {
        int i = 0;
        string directory = path_test;

        string FichierJson = "Key" + i.ToString() + ".json";
        string path = Path.Combine(directory, FichierJson);
        if (File.Exists(path)) {
            i = +1;
            FichierJson = "Key" + i.ToString() + ".json";
            path = Path.Combine(directory, FichierJson);
        }

        var donnees = new
        {
            cle = key,
        };
        string json = JsonSerializer.Serialize(donnees);
        File.WriteAllText(path, json);
        Console.WriteLine($"clé écrites dans le fichier JSON au chemin: {path} ");
        return path;
    } 
}
public class CleData
{
    public string Cle { get; set; }
}