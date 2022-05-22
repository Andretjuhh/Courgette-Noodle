using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{
    class Admin : Step // zie resoverzicht.cs voor uitleg
    {
        private readonly Step _option1_1;
        private readonly Step _option1_2;
        private readonly Step _option1_3;
        private readonly Step _option1_4;

        public Admin()
        { //als je de optie kiest gaat ie over naar ResOverzicht.cs of een van de andere
            _option1_1 = new ResOverzicht();
            _option1_2 = new TafelOverzicht();
            _option1_3 = new TafelBestellingen();
            _option1_4 = new MenuAanpassing();

            _option1_1.SetPrevious(this);
            //_option1_2.SetPrevious(this);
            //_option1_3.SetPrevious(this);
            //_option1_4.SetPrevious(this);
        }
        public override void Show()
        {
            Console.Clear();
            Log("[Step 2]");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage();
            ConsoleKeyInfo input;

            do
            {
                Console.Clear();

                if (taalSetting == "nl")
                {
                    Display("Kies alstublieft uit één van de volgende opties: ");
                    Display("");
                    Display("[1] Overzicht van de reserveringen");
                    //Display("[2] Overzicht van de tafels");
                    //Display("[3] Bestellingen per tafel");
                    //Display("[4] Aanpassen van het menu");
                    Display("[5] Terug gaan naar het hoofdmenu");
                }

                else
                {
                    Display("Please choose one of the following options:");
                    Display("");
                    Display("[1] Overview of the reservations");
                    //Display("[2] Overview of the tables");
                    //Display("[3] Orders per table");
                    //Display("[4] Change the menu");
                    Display("[5] Go back to the main menu");
                }

                input = Console.ReadKey();
            }
            //als je 1, 2, 3, 4, etc drukt dan gaat ie naar de bijbehorende optie en dan naar het volgend cs bestand
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4 && input.Key != ConsoleKey.D5);

            if (input.Key == ConsoleKey.D1)
            {
                _option1_1.Show();
            }

            if (input.Key == ConsoleKey.D2)
            {
                _option1_2.Show();
            }

            if (input.Key == ConsoleKey.D3)
            {
                _option1_3.Show();
            }
            
            if (input.Key == ConsoleKey.D4)
            {
                _option1_4.Show();
            }
            
            if (input.Key == ConsoleKey.D5)
            { // als je key escape is gaat ie terug naar het mainmenu
                var MainMenu = new MainMenu();
                MainMenu.Show();
            }
        }
    }
}