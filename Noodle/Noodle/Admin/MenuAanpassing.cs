using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace Noodle
{

    class MenuAanpassing : Step 
    {

        public override void Show()
        {
            ConsoleKeyInfo input;
            input = Console.ReadKey();
            string Kaas = "";
            Console.Clear();
            Console.WriteLine("Kies een ID\n[1] - ID123\n[2] - ID321");
            if (input.Key == ConsoleKey.D1)
            {
                Kaas += "ID123";
            }
            if (input.Key == ConsoleKey.D2)
            {
                Kaas += "ID321";
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("CPU");
                writer.WriteValue(Kaas);
                writer.WritePropertyName("PSU");
                writer.WriteValue("500W");
                writer.WritePropertyName("Drives");
                writer.WriteStartArray();
                writer.WriteValue("DVD read/writer");
                writer.WriteComment("(broken)");
                writer.WriteValue("500 gigabyte hard drive");
                writer.WriteValue("200 gigabyte hard drive");
                writer.WriteEnd();
                writer.WriteEndObject();
            }
            string json = JsonConvert.SerializeObject(sb);
            string sk = (sb.ToString());
            //Console.Clear();
            //Console.WriteLine(sb.ToString());
            File.WriteAllText("ReservatieInformatie.json", sk);


            input = Console.ReadKey();
            while (input.Key != ConsoleKey.Escape) ;

            if (input.Key == ConsoleKey.Escape)
            { // bij escape gaat ie weer terug naar Admin.cs
                var Admin = new Admin();
                Admin.Show();

            }
        }
        public class ReservationThing
        {
            public string Reservation { get; set; }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("CPU");
                writer.WriteValue("Intel");
                writer.WritePropertyName("PSU");
                writer.WriteValue("500W");
                writer.WritePropertyName("Drives");
                writer.WriteStartArray();
                writer.WriteValue("DVD read/writer");
                writer.WriteComment("(broken)");
                writer.WriteValue("500 gigabyte hard drive");
                writer.WriteValue("200 gigabyte hard drive");
                writer.WriteEnd();
                writer.WriteEndObject();
            }

            // {
            //   "CPU": "Intel",
            //   "PSU": "500W",
            //   "Drives": [
            //     "DVD read/writer"
            //     /*(broken)*/,
            //     "500 gigabyte hard drive",
            //     "200 gigabyte hard drive"
            //   ]
            // }
        }

        class Program
        {
            
            
        }
    }
}