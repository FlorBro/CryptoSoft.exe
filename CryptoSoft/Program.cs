using System;
using System.Linq;
using System.Text;
using testingproject;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        if(args.Length < 2)
        {
            Console.WriteLine("Veuillez fournir le path du dossier ainsi que C pour Crypter ou D pour Decrypter");
            return;
        }
        string path_test = args[0].ToString(); //C:\\Users\\floba\\OneDrive\\Bureau\\Test CryptoSoft

        //Console.WriteLine("Message binaire : " + message);
        string[] fichiers = Directory.GetFiles(path_test);
        if (args[1] == "C")
        {
            string binaryKey = GenerateBinaryKey(64);
            foreach (string fichier in fichiers)
            {
                string extension = '.' + fichier.Split('.')[1];
                Crypter.CrypterFichier(fichier, binaryKey, extension); // + ".txt"
            }
            keywriter(binaryKey, path_test);
        }
        else if(args[1]=="D")
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
                Decrypter.DecrypterleFichier(fichier, key, extension);  
            }
            File.Delete(path_test + "\\Key0.json"); 
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