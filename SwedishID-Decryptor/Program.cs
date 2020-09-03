using System;
using static System.Console;
using System.Globalization;

namespace SwedishID_Decryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt the user for input data
            Write("First name: ");
            string firstName = ReadLine();

            Write("Last name: ");
            string lastName = ReadLine();

            Write("Social Security Number: ");
            string socialSecurityNumber = ReadLine();



            // Information to decrypt from the input data: 
            string gender;
            int age;
            string generation = "Unknown";
            string generationInformation = "Undefined";



            // --------------------------------------------- Define gender --------------------------------------------------//
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            bool isFemale = genderNumber % 2 == 0;
            gender = isFemale ? "Female" : "Male";



            // ---------------------------------------------- Define age ----------------------------------------------------//
            DateTime birthDate = DateTime.Now;

            // if we are dealing with the short version of SSN...
            if (socialSecurityNumber.Length >= 10 && socialSecurityNumber.Length <= 11)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
            }

            // if we are dealing with the long version of SSN...
            else if (socialSecurityNumber.Length >= 12 && socialSecurityNumber.Length <= 13)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            // if the length of SSN is not valid...
            else
            {
                WriteLine("Invalid Social Security Number...");
                ReadLine();
                return;
            }

            age = DateTime.Now.Year - birthDate.Year;


            // Possible age correction depending on the day of the year
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }



            // ------------------------------------------ Define generation ------------------------------------------------- // 
            // source for calculations: http://socialmarketing.org/archives/generations-xy-z-and-the-others/

            int birthYear = birthDate.Year;

            if (birthYear >= 1912 && birthYear <= 1921)
            {
                generation = "Depression generation";

                generationInformation = "the Great Depression, the Global Unrest";
            }
            else if (birthYear >= 1922 && birthYear <= 1927)
            {
                generation = "War generation";

                generationInformation = "the Korean War, the Second World War, the Cold War";
            }
            else if (birthYear >= 1928 && birthYear <= 1945)
            {
                generation = "Post-War generation";

                generationInformation = "post - war economic boom, the growth in Cold War tensions," +
                                        "\nthe potential for nuclear war and other never before seen threats.";
            }
            else if (birthYear >= 1946 && birthYear <= 1964)
            {
                generation = "Baby-Boomers generation";

                generationInformation = "post-WWII optimism, the cold war, and the hippie movement";

            }
            else if (birthYear >= 1965 && birthYear <= 1976)
            {
                generation = "Lost generation";

                generationInformation = "the end of the cold war, the rise of personal computing, " +
                                         "\nand feeling lost between the two huge generations.";
            }
            else if (birthYear >= 1977 && birthYear <= 1994)
            {
                generation = "Millennials";

                generationInformation = "the Great Recession, the technological explosion of the internet" +
                                        "\nand social media, and 9/11";
            }
            else if (birthYear >= 1995 && birthYear <= 2015)
            {
                generation = "Generation Z";

                generationInformation = "smartphones, social media, never knowing a country not at war, " +
                                        "\nand seeing the financial struggles of their parents.";
            }



            // Clear the screen
            Clear();
            Beep(100, 100);


            // Output the results
            WriteLine($"Name: {firstName} + {lastName} " +
                      $"\nSocial Security Number: {socialSecurityNumber} " +
                      $"\nGender: {gender}" +
                      $"\nAge: {age}" +
                      $"\n\nGeneration: {generation}" +
                      $"\nShaping events: {generationInformation}");
        }
    }
}
