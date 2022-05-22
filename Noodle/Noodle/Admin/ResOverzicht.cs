using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Noodle
{

    class ResOverzicht : Step //class van de cs file, waarmee je in andere files naar deze kan verwijzen (bijv van admin selectie scherm naar overzicht scherm)
    {
        public class ReservatieInformaties
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string Reserveringen { get; set; }
            public int IDCounter { get; set; }
        }


        // hier zet ie de json om tot leesbaar formaat
        public ReservatieInformaties Deserialize()
        {
            string json = File.ReadAllText("ReservatieInformatie.json");
            var ReservatieInformatieJson = System.Text.Json.JsonSerializer.Deserialize<ReservatieInformaties>(json);
            return ReservatieInformatieJson;
        }

        public override void Show()
        {
            static void JsonDing(string kaas, int jen)
            {
                string Kees = kaas;
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                int Jan = jen;
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("Reserveringen");
                    writer.WriteValue(Kees);
                    writer.WritePropertyName("IDCounter");
                    writer.WriteValue(Jan);
                    writer.WriteEnd();
                }
                string json = JsonConvert.SerializeObject(sb);
                string sk = (sb.ToString());
                //Console.Clear();
                //Console.WriteLine(sb.ToString());
                File.WriteAllText("ReservatieInformatie.json", sk);
                var Admin = new Admin();
                Admin.Show();
            }
            static string ReturnLeeg()  
            {
                return "Er zijn geen reserveringen";
            }
            Log("");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage(); //pakt de taal van de greetingsfile
            ConsoleKeyInfo input, input2;
            var ReservatieInformatieJson = Deserialize();
            string Reserverings = ReservatieInformatieJson.Reserveringen;
            int IDCounters = ReservatieInformatieJson.IDCounter;
            do
            {
                {
                    Console.Clear();

                    if (taalSetting == "nl")
                    { //de tweede write line pakt ie wat onder "overzicht" staat in het json bestand
                        Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                        Console.WriteLine(ReservatieInformatieJson.Reserveringen);
                        Console.WriteLine("\n[1] Zet er een voorbeeld reservering bij");
                        Console.WriteLine("[2] Voeg of wijzig een reservering");
                        Console.WriteLine("[3] Verwijder alle reserveringen");
                        Console.WriteLine("[4] Verwijder een specifieke reservering");
                        Console.WriteLine("[5] Ga terug naar het Admin overzicht");
                    }

                    else
                    {
                        Console.WriteLine("Here is the overview of the reservations: ");
                        Console.WriteLine(ReservatieInformatieJson.Reserveringen);
                    }
                    // input wordt wat de gebruiker erin typt
                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.D5 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4);
            if (input.Key == ConsoleKey.D1)
            {
                if (Reserverings == ReturnLeeg())
                {
                    Reserverings = "";
                }
                string currentMonth = DateTime.Now.ToString("MM");
                string currentDay = DateTime.Now.ToString("dd");
                string Jantien = "ID" + Convert.ToString(IDCounters) + "-" + currentDay + "/" + currentMonth + "-4 Personen-18:30";
                Reserverings += Jantien + "\n";
                int Johan = IDCounters;
                Johan += 1;
                JsonDing(Reserverings, Johan);
            }
            if (input.Key == ConsoleKey.D2)
            {
                Console.Clear();
                Console.WriteLine("[1] Voeg een reservering toe\n[2] Wijzig een reservering");
                input2 = Console.ReadKey();
                if (input2.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                    Console.WriteLine(ReservatieInformatieJson.Reserveringen);
                    Display("Voeg hier je reserving toe als: \nmm/dd-x Personen-uu:mm\nNote: Je hoeft er geen ID bij te zetten");
                    if (Reserverings != "Er zijn geen reserveringen")
                    {
                        string Jantien = "ID" + Convert.ToString(IDCounters) + "-" + Console.ReadLine(); 
                        Reserverings += Jantien + "\n";
                    }
                    if (Reserverings == "Er zijn geen reserveringen")
                    {
                        Reserverings = Console.ReadLine();
                        string Jantien = "ID" + Convert.ToString(IDCounters) + "-" + "\n";
                        Reserverings += Jantien;

                    }
                    if (Reserverings == "")
                    {
                        Reserverings = ReturnLeeg();
                    }
                    int Johan = IDCounters;
                    Johan += 1;
                    JsonDing(Reserverings, Johan);
                }
                
                else if (input2.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                    Console.WriteLine(ReservatieInformatieJson.Reserveringen);
                    Console.WriteLine("\nTyp de reservering in die je wil wijzigen *exact* over");
                    string InputReservering = Console.ReadLine();
                    Console.WriteLine("\nTyp nu in waar je het in wil wijzigen als: \nIDxx-mm/dd-x Personen-uu:mm\nNote: ID hetzelfde laten");
                    string WijzigdeReserving = Console.ReadLine();
                    Reserverings = Reserverings.Replace(InputReservering + "\n", WijzigdeReserving + "\n");
                    if (Reserverings == "")
                    {
                        Reserverings = ReturnLeeg();
                    }
                    JsonDing(Reserverings, IDCounters);
                }

            }
            if (input.Key == ConsoleKey.D3)
            {
                Reserverings = ReturnLeeg();
                int Johan = IDCounters;
                Johan = 0;
                JsonDing(Reserverings, Johan);

            }
            if (input.Key == ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                Console.WriteLine(ReservatieInformatieJson.Reserveringen);
                Console.WriteLine("\nTyp de reservering in die je wil verwijderen *exact* over");
                string temp = Console.ReadLine();
                Reserverings = Reserverings.Replace(temp + "\n", "");
                JsonDing(Reserverings, IDCounters);
            }

            if (input.Key == ConsoleKey.D5)
            { // bij 5 gaat ie weer terug naar Admin.cs
                var Admin = new Admin();
                Admin.Show();

            }
        }
        public void Serialize(ResOverzicht resoverzicht)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            // serialized het weer want vs wil dat
            var ReservatieInformatieJson = System.Text.Json.JsonSerializer.Serialize(resoverzicht, serializeOptions);
            File.WriteAllText("ReservatieInformatie.json", ReservatieInformatieJson);
        }
    }
}