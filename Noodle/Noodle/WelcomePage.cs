using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;



namespace Noodle
{
    class WelcomePage : Step
    {
        private readonly Step _next;

        public WelcomePage()
        {
            _next = new MainMenu();
            _next.SetPrevious(this);
        }
    

    //Taal functie, niet af
        public class Greetings
        {
        public string language { get; set; }
        public string welkom_nl { get; set; }
        public string welkom_en { get; set; }
        public bool ingelogd { get; set; }
        public string ingelogdeMember { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int counter { get; set; }
        }

        public  Greetings Deserialize()
        {
        string json = File.ReadAllText("greetings.json");
        var greetingsJson = JsonSerializer.Deserialize<Greetings>(json);
        return greetingsJson;
        }
        public string Getlanguage()
        {
        var greetingsJson = Deserialize();
        return greetingsJson.language;
        }
        public string Getusername()
        {
        var greetingsJson = Deserialize();
        return greetingsJson.username;
        }
        public string Getpassword()
        {
        var greetingsJson = Deserialize();
        return greetingsJson.password;
        }
        public string Getfullname()
        {
        var greetingsJson = Deserialize();
        return greetingsJson.ingelogdeMember;
        }

        public override void Show()
        {
        Log("[Step 1]");

        
        ConsoleKeyInfo input;

        do
        {
            Console.Clear();
            var greetingsJson = Deserialize();
            if (greetingsJson.language.Equals("nl"))
            {
                Console.WriteLine(greetingsJson.welkom_nl);
            }

            if (greetingsJson.language.Equals("en"))
            {
                Console.WriteLine(greetingsJson.welkom_en);
            }

            greetingsJson.ingelogd = false;
            greetingsJson.ingelogdeMember = null;
            Serialize(greetingsJson);
            input = Console.ReadKey();
        }
        while (input.Key != ConsoleKey.Enter);

        _next.Show();
        }

        public void Serialize(Greetings greeting)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var greetingsJson = JsonSerializer.Serialize(greeting, serializeOptions);
            File.WriteAllText("greetings.json", greetingsJson);
        }
    }
}