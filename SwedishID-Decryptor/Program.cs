using System;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Console;

namespace SwedishID_Decryptor
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum Generation
    {
        Generation_Z,
        Millenials,
        Lost_Generation,
        Baby_Boomers,
        Post_War_Generation,
        War_Generation,
        Depression_Generation,
        Undefined
    }


    class Program
    {
        static void Main(string[] args)
        {

            // Declare intro variables
            string firstName;
            string lastName;
            string socialSecurityNumber;



            // If parameters were given upon the program start...
            if (args.Length >= 3)
            {
                // assign input data to respective variables
                firstName = args[0];
                lastName = args[1];
                socialSecurityNumber = ValidateSSNNumber(args[2]);
            }
            else
            {
                // prompt the user for input data
                // assign the data to respective variables
                Write("First name: ");
                firstName = ReadLine();

                Write("Last name: ");
                lastName = ReadLine();

                Write("Social Security Number (YYYYMMDD-XXXX): ");
                socialSecurityNumber = ValidateSSNNumber(ReadLine());
            }



            // Retrieve gender/age/generation from input Social Security Number: 

            // Define gender 
            Gender gender = DefineGender(socialSecurityNumber);
            // Define age 
            uint age = DefineAge(socialSecurityNumber);
            // Define generation
            Generation generation = DefineGeneration(socialSecurityNumber);



            // Clear the screen
            Clear();
            Beep(200, 500);


            // Output the results
            WriteLine($"Name: {firstName} {lastName} " +
                      $"\nSocial Security Number: {socialSecurityNumber} " +
                      $"\nGender: {gender}" +
                      $"\nAge: {age}" +
                      $"\n\nGeneration: {generation}");

            ReadLine();
        }




        // Method that checks user input for validity & formats it according to uniformed 12-digit format
        private static string ValidateSSNNumber(string inputNumber)
        {
            // Initialize SSN patterns in two versions
            string pattern1 = @"^\d{6}-\d{4}$";
            string pattern2 = @"^\d{8}-\d{4}$";

            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);


            // Check inputNumber for validity
            bool validSocialSecurityNumber = regex1.IsMatch(inputNumber) || regex2.IsMatch(inputNumber);


            // Prompt the user to enter SSN until it is valid
            while (!validSocialSecurityNumber)
            {
                WriteLine("Invalid Social Security Number...");
                Write("Please, try again: ");
                inputNumber = ReadLine();
                validSocialSecurityNumber = regex1.IsMatch(inputNumber) || regex2.IsMatch(inputNumber);
            }

            // Unified SSN format: YYYYMMDD-XXXX
            string unifiedSocialSecurityNumber;

            // If the short pattern of SSN was followed...
            if (regex1.IsMatch(inputNumber))
            {
                // complement the pattern 
                int shortYearVersion = int.Parse(inputNumber.Substring(0, 2));

                if (shortYearVersion > 20 && shortYearVersion <= 99)
                {
                    unifiedSocialSecurityNumber = "19" + inputNumber;
                }
                else
                {
                    unifiedSocialSecurityNumber = "20" + inputNumber;
                }
            }

            // If the long pattern of SSN was followed...
            else
            {
                // assign the long pattern as unifiedSocialSecurityNumber
                unifiedSocialSecurityNumber = inputNumber;
            }


            return unifiedSocialSecurityNumber;
        }


        // Method that retrieves gender information from Social Security Number
        private static Gender DefineGender(string inputSocialSecurityNumber)
        {

            Gender currentGender;
            int genderNumber = int.Parse(inputSocialSecurityNumber.Substring(inputSocialSecurityNumber.Length - 2, 1));
            bool isFemale = genderNumber % 2 == 0;
            currentGender = isFemale ? Gender.Female : Gender.Male;
            return currentGender;
        }


        // Method that retrieves age information from Social Security Number
        private static uint DefineAge(string inputSocialSecurityNumber)
        {
            // Retrieve the birth date from input social security number
            DateTime birthDate = DateTime.ParseExact(inputSocialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            // Calculate age based on retrieved data
            uint age = (uint)(DateTime.Now.Year - birthDate.Year);

            // Possible age correction depending on the day of the year
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }

            return age;
        }


        // Method that retrieves generation information from Social Security Number
        private static Generation DefineGeneration(string inputSocialSecurityNumber)
        {
            // Retrieve the birth date from input social security number
            DateTime birthDate = DateTime.ParseExact(inputSocialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            // Retrieve the birth year
            int birthYear = birthDate.Year;

            // Declare a target variable
            Generation generation;

            // Define the target variable
            if (birthYear >= 1995) { generation = Generation.Generation_Z; }
            else if (birthYear >= 1977) { generation = Generation.Millenials; }
            else if (birthYear >= 1965) { generation = Generation.Lost_Generation; }
            else if (birthYear >= 1946) { generation = Generation.Baby_Boomers; }
            else if (birthYear >= 1928) { generation = Generation.Post_War_Generation; }
            else if (birthYear >= 1922) { generation = Generation.War_Generation; }
            else if (birthYear >= 1912) { generation = Generation.Depression_Generation; }
            else { generation = Generation.Undefined; }

            return generation;
        }

    }
}
