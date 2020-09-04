using System;
using static System.Console;
using System.Globalization;

namespace SwedishID_Decryptor
{
    class Program
    {
        static void Main(string[] args)
        {

            // Declare intro variables
            string firstName;
            string lastName;
            string socialSecurityNumber;

            

            // If paramters were given upon the program start...
            if(args.Length >= 3)
            {
                // assign the data to respective variables
                firstName = args[0];
                lastName = args[1];
                socialSecurityNumber = args[2];
            }
            else
            {
                // prompt the user for input data
                // assign the data to respective variables
                Write("First name: ");
                firstName = ReadLine();

                Write("Last name: ");
                lastName = ReadLine();

                Write("Social Security Number: ");
                socialSecurityNumber = ReadLine();
            }
            
     

            //__Check user input for validity_________________________________________________________________________//
              
            // Purify the input number-string 
            string socialSecurityNumberClean = socialSecurityNumber.Replace("-", "");
            
            // If the purified number-string is NOT convertable to float = user input is NOT valid
            if(!float.TryParse(socialSecurityNumberClean, out float socialSecurityNumberIntegerified))
            {
               // terminate the program softly
                WriteLine("Invalid Social Security Number");
                ReadLine();
                return;
            }
            //_____________________________________________________________________________________________________//




            // Information to decrypt from the input data: 
            string gender = "Unknown";
            int age = 0;
            string generation = "Unknown";
            string generationInformation = "Undefined";



            // --------------------------------------------- Define gender --------------------------------------------------//
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            bool isFemale = genderNumber % 2 == 0;
            gender = isFemale ? "Female" : "Male";



            // ---------------------------------------------- Define age ----------------------------------------------------//
            // Assign the default value to birthDate
            DateTime birthDate = DateTime.Now;


            // Retrieve birthday date from SSN:

            // If we are dealing with the short version of SSN...
            if (socialSecurityNumber.Length >= 10 && socialSecurityNumber.Length <= 11)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
            }
            // If we are dealing with the long version of SSN...
            else if (socialSecurityNumber.Length >= 12 && socialSecurityNumber.Length <= 13)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            // if the length of SSN is not valid...
            else
            {
                // terminate the program softly
                WriteLine("Invalid Social Security Number...");
                ReadLine();
                return;
            }


            // Calculate age based on retrieved data
            age = DateTime.Now.Year - birthDate.Year;

            // Possible age correction depending on the day of the year
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }



            // ------------------------------------------ Define generation ------------------------------------------------- // 
            // source: http://socialmarketing.org/archives/generations-xy-z-and-the-others/

            int birthYear = birthDate.Year;

            if (birthYear >= 1995)
            {
                generation = "Generation Z";
                generationInformation = "smartphones, social media, never knowing a country not at war.";
            }
            else if (birthYear >= 1977)
            {
                generation = "Millennials";
                generationInformation = "the Great Recession, the technological explosion of the internet and social media, 9/11.";
            }
            else if (birthYear >= 1965)
            {
                generation = "Lost generation";
                generationInformation = "the end of the Cold war, the lowest voting participation rate, skepticism.";
            }
            else if (birthYear >= 1946)
            {
                generation = "Baby-Boomers generation";
                generationInformation = "post-WWII optimism, the cold war, and the hippie movement.";
            }
            else if (birthYear >= 1928)
            {
                generation = "Post-War generation";
                generationInformation = "post-war economic boom, Cold War tensions, the potential for nuclear war.";
            }
            else if (birthYear >= 1922)
            {
                generation = "War generation";
                generationInformation = "the Korean War, the Second World War, the Cold War.";
            }
            else if (birthYear >= 1912)
            {
                generation = "Depression generation";
                generationInformation = "the Great Depression, the Global Unrest.";
            }     
            


            // Clear the screen
            Clear();
            Beep(200, 500);


            // Output the results
            WriteLine($"Name: {firstName} {lastName} " +
                      $"\nSocial Security Number: {socialSecurityNumber} " +
                      $"\nGender: {gender}" +
                      $"\nAge: {age}" +
                      $"\n\nGeneration: {generation}" +
                      $"\nShaping events: {generationInformation}");

            ReadLine();
        }
    }
}
