using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Reserveer : Step
    {

        public override void Show()
        {
            //Variable benoemen
            ConsoleKeyInfo input;
            string reservatieNaam = null;
            int reservatieDatumMaand = 0;
            int reservatieDatumDag = 0;
            string reservatieTijd = null;
            string reservatieAantal = null;


            static void Terug()
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();

            }

            do
            {
                //Leeg maken van de console
                Console.Clear();
                string taalSetting = "nl";


                //Als het in het Nederlands staat
                if (taalSetting == "nl")
                {
                    Display("Bedankt voor het kiezen voor de Noodle \nOm te kunnen reserveren hebben wij wat informatie nodig");
                    Display("");

                    //Enter om te beginnen met reserveren
                    do
                    {
                        //Display dat je op enter moet klikken om terug te gaan
                        Display("Druk op [ENTER] om de reservering te beginnen en [5] om af te breken\n\nNote: Als u wilt teruggaan tijdens het maken van de reservering typ 'terug' in");


                        //Met 5 terug aan het einde
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.D5)
                        {
                            var MainMenu = new MainMenu();
                            MainMenu.Show();

                        }
                    }
                    while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.D5);

                    Display("\nVul alstublieft u hele naam in:");

                    //Invullen van de naam
                    bool geldigeNaam = false;
                    while (geldigeNaam == false)
                    {
                        reservatieNaam = Console.ReadLine();
                        if (reservatieNaam == "terug")
                        {
                            Terug();
                        }
                        else if (reservatieNaam.Length < 3)
                        {
                            Display("\nVul alstublieft een naam in");
                            geldigeNaam = false;
                        }
                        else
                        {
                            geldigeNaam = true;
                        }
                    }  
                    Display("\nWelke Maand wilt u komen?");
                    bool validMonth = false;
                    while(validMonth == false)
                    {
                        string maandString = Console.ReadLine();
                        if (maandString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(maandString, out reservatieDatumMaand) && reservatieDatumMaand <= 12)
                        {
                            validMonth = true;

                        }
                        else
                        {
                            Console.WriteLine("\nDeze maand is niet geldig. Vul alstublieft een nummer in tussen 1 (januari) en 12 (december)");
                            validMonth = false;
                        }
                    }
                    Display("\nWelke Dag wilt u komen?");

                    //Controleert of de dag klopt. Met een max van het aantal dagen in die maand
                    bool validDay = false;
                    while (validDay == false)
                    {
                        string dagString = Console.ReadLine();
                        if (dagString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(dagString, out reservatieDatumDag))
                        {
                            //  Maanden met 31 dagen
                            if (reservatieDatumMaand == 1 || reservatieDatumMaand == 3 || reservatieDatumMaand == 5 || reservatieDatumMaand == 7 || reservatieDatumMaand == 8 || reservatieDatumMaand == 10 || reservatieDatumMaand == 12)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 31)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 31");
                                    validDay = false;

                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                            // Maanden met 30 dagen
                            else if (reservatieDatumMaand == 4 || reservatieDatumMaand == 6 || reservatieDatumMaand == 9 || reservatieDatumMaand == 11)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumMaand > 30)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en de 30");
                                    validDay = false;
                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                            //Februari
                            else if (reservatieDatumMaand == 2)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 28)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 28");
                                    validDay = false;
                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                        }
                        else
                        {
                            Display("\nDit is geen geldig dag voor deze maand. Vul alstublieft een nummer in.");
                        }

                    }



                    Display("\nWelke tijd wilt u komen? (uur:minuut). U kunt per half uur reserveren. De eerst mogelijke tijd is 17:00. Reserveren kan tot 22:00.");
                    //Controleert of de tijd klopt.

                    bool validTime = false;
                    while(validTime == false)
                    {
                        reservatieTijd = Console.ReadLine();
                        if (reservatieTijd == "terug")
                        {
                            Terug();
                        }
                        else if (reservatieTijd == "17:00" || reservatieTijd == "17:30" || reservatieTijd == "18:00" || reservatieTijd == "18:30" || reservatieTijd == "19:00"
                            || reservatieTijd == "19:30" || reservatieTijd == "20:00" || reservatieTijd == "20:30" || reservatieTijd == "21:00" || reservatieTijd == "21:30" || reservatieTijd == "22:00")
                        {
                            validTime = true;
                        }
                        else
                        {
                            Display("\nDit is geen geldig tijd. Let op dat het binnen de reserveertijden valt. Er kan alleen per half uur gereserveerd worden. Noteer het op de volgende manier (uur:minuut)");
                            validTime = false;
                        }
                    }


                    Display("\nMet hoeveel mensen wilt u komen?");
                    //Controleert met hoeveel mensen ze komen. De tafels hebben maximaal 4 plekken. Alles daarboven ervoor bellen
                    reservatieAantal = Console.ReadLine();
                    if (reservatieAantal == "terug")
                    {
                        Terug();
                    }
                    else if (Convert.ToInt32(reservatieAantal) > 6)
                    {
                        Display("\nVoor reserveringen van meer dan 6 personen moet u even bellen naar het restaurant.");
                    }

                    Display("\nBedankt voor je reservering! Klik op [ENTER] om de reservering te bevestigen!\nAls u de reservering niet wilt bevestigen klik dan op [TERUG]");
                    input = Console.ReadKey();

                    //Met escape terug aan het einde
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        string currentYear = DateTime.Now.Year.ToString();
                        Console.Clear();
                        Display("\nBedankt voor het reserveren.");
                        Display($"\nNaam: {reservatieNaam} \nDatum: {reservatieDatumDag}-{reservatieDatumMaand}-{currentYear} \nTijd: {reservatieTijd}\nAantal personen: {reservatieAantal}\nKlik op [ESC] om terug te gaan naar het hoofdmenu");




                    }
                    if (input.Key == ConsoleKey.D5)
                    {
                        var MainMenu = new MainMenu();
                        MainMenu.Show();
                    }
                }

                if (taalSetting == "en")
                {
                    Display("Thanks for choosing 'The Noodle' \nTo make a reservation press [ENTER]");
                    Display("");

                    //Enter om te beginnen met reserveren
                    do
                    {
                        //Display dat je op enter moet klikken om terug te gaan
                        Display("Press [ENTER] to begin with the reservation");
                        input = Console.ReadKey();

                        //Met escape terug aan het einde
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.D5)
                        {
                            var MainMenu = new MainMenu();
                            MainMenu.Show();

                        }
                    }
                    while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.D5);

                    Display("\nPlease, fill in your name:");

                    bool geldigeNaam = false;
                    while (geldigeNaam == false)
                    {
                        reservatieNaam = Console.ReadLine();
                        if (reservatieNaam == "terug")
                        {
                            Terug();
                        }
                        else if (reservatieNaam.Length < 3)
                        {
                            Display("\nPlease fill in a name.");
                            geldigeNaam = false;
                        }
                        else
                        {
                            geldigeNaam = true;
                        }
                    }

                    Display("\nWhich month would you like to come?");
                    bool validMonth = false;
                    while (validMonth == false)
                    {
                        string maandString = Console.ReadLine();
                        if (maandString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(maandString, out reservatieDatumMaand) && reservatieDatumMaand <= 12)
                        {
                            validMonth = true;

                        }
                        else
                        {
                            Console.WriteLine("\nThis month is not valid. Please fill in a number between 1 (januari) and 12 (december)");
                            validMonth = false;
                        }
                    }
                    Display("\nWhat day would you like to come?");

                    //Controleert of de dag klopt. Met een max van het aantal dagen in die maand
                    bool validDay = false;
                    while (validDay == false)
                    {
                        string dagString = Console.ReadLine();
                        if (dagString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(dagString, out reservatieDatumDag))
                        {
                            //  Maanden met 31 dagen
                            if (reservatieDatumMaand == 1 || reservatieDatumMaand == 3 || reservatieDatumMaand == 5 || reservatieDatumMaand == 7 || reservatieDatumMaand == 8 || reservatieDatumMaand == 10 || reservatieDatumMaand == 12)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 31)
                                {
                                    Display("\nThis isn't a valid day for the month. Please fill in a number between 1 and 31.");
                                    validDay = false;

                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                            // Maanden met 30 dagen
                            else if (reservatieDatumMaand == 4 || reservatieDatumMaand == 6 || reservatieDatumMaand == 9 || reservatieDatumMaand == 11)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumMaand > 30)
                                {
                                    Display("\nThis isn't a valid day for the month. Please fill in a number between 1 and 30.");
                                    validDay = false;
                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                            //Februari
                            else if (reservatieDatumMaand == 2)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 28)
                                {
                                    Display("\nThis isn't a valid day for the month. Please fill in a number between 1 and 28.");
                                    validDay = false;
                                }
                                else
                                {
                                    validDay = true;
                                }
                            }
                        }
                        else
                        {
                            Display("\nThis is not a valid day. Please fill in a number.");
                        }

                    }



                    Display("\nWhat time would you like to come (hour:minute). You can make a reservation per 30 minutes. The earliest opportunity is at 17:00. The last opportunity is at 22:00. ");
                        
                    //Controleert of de tijd klopt.

                    bool validTime = false;
                    while (validTime == false)
                    {
                        reservatieTijd = Console.ReadLine();
                        if (reservatieTijd == "terug")
                        {
                            Terug();
                        }
                        if (reservatieTijd == "17:00" || reservatieTijd == "17:30" || reservatieTijd == "18:00" || reservatieTijd == "18:30" || reservatieTijd == "19:00"
                            || reservatieTijd == "19:30" || reservatieTijd == "20:00" || reservatieTijd == "20:30" || reservatieTijd == "21:00" || reservatieTijd == "21:30" || reservatieTijd == "22:00")
                        {
                            validTime = true;
                        }
                        else
                        {
                            Display("\nThis is not a valid time. Make sure to choose a time within the reservation hours. You can make a reservation per 30 minutes with the syntax hour:minutes");
                            validTime = false;
                        }
                    }


                    Display("\nWith how many people would you like to come?");
                    //Controleert met hoeveel mensen ze komen. De tafels hebben maximaal 4 plekken. Alles daarboven ervoor bellen
                    reservatieAantal = Console.ReadLine();
                    if (reservatieAantal == "terug")
                    {
                        Terug();
                    }
                    if (Convert.ToInt32(reservatieAantal) > 6)
                    {
                        Display("\nFor reservations with more then 6 people.");
                    }

                    Display("\nThanks for filling in your information. Press [ENTER] To confirm it!\n If you dont want to finish your reservation press [ESC]");
                    input = Console.ReadKey();

                    //Met escape terug aan het einde
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        string currentYear = DateTime.Now.Year.ToString();
                        Console.Clear();
                        Display("Thanks for making a reservation at 'The noodle'!");
                        Display($"\nName: {reservatieNaam} \nDate: {reservatieDatumDag}-{reservatieDatumMaand}-{currentYear} \nTime: {reservatieTijd}\nHow many people: {reservatieAantal}\n Press [ESC] to go back to the main menu");




                    }
                    if (input.Key == ConsoleKey.D5)
                    {
                        var MainMenu = new MainMenu();
                        MainMenu.Show();
                    }
                }

            }
            while (reservatieNaam == null || reservatieDatumMaand == 0 || reservatieTijd == null || reservatieAantal == null);


            //Met escape terug aan het einde
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.D5)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();
    
            }
        }
    }
}