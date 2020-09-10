using System;
using static System.Console;
using System.Globalization;
using System.Text.RegularExpressions;

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

                Write("Social Security Number (YYYYMMDD-XXXX): ");
                socialSecurityNumber = ValidateSSNNumber(ReadLine());
            }




            // Decrypt information from the input Social Security Number: 
            
            // Define gender 
            string gender = DefineGender(socialSecurityNumber);

            // Define age 
            int age = DefineAge(socialSecurityNumber);

            // Define generation
            string generation = DefineGeneration(socialSecurityNumber);
            

            
            
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
        static string ValidateSSNNumber(string inputNumber)
        {
            // Initialize SSN patterns in two versions
            string pattern1 = @"^\d{6}-\d{4}$";
            string pattern2 = @"^\d{8}-\d{4}$";

            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);


            // Check inputNumber for validity
            bool validSocialSecurityNumber = regex1.IsMatch(inputNumber) || regex2.IsMatch(inputNumber);



            // Set up the loop that will prompt the user to enter SSN until it is valid
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
        static string DefineGender(string inputSocialSecurityNumber)
        {
            string gender;
            int genderNumber = int.Parse(inputSocialSecurityNumber.Substring(inputSocialSecurityNumber.Length - 2, 1));
            bool isFemale = genderNumber % 2 == 0;
            gender = isFemale ? "Female" : "Male";
            return gender;
        }


        // Method that retrieves age information from Social Security Number
        static int DefineAge(string inputSocialSecurityNumber)
        {
            // Retrieve the birth date from input social security number
            DateTime birthDate = DateTime.ParseExact(inputSocialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            
            // Calculate age based on retrieved data
            int age = DateTime.Now.Year - birthDate.Year;

            // Possible age correction depending on the day of the year
            if ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
            {
                age--;
            }

            return age;
        }


        // Method that retrieves generation information from Social Security Number
        static string DefineGeneration(string inputSocialSecurityNumber)
        {
            // Retrieve the birth date from input social security number
            DateTime birthDate = DateTime.ParseExact(inputSocialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            // Retrieve the birth year
            int birthYear = birthDate.Year;

            // Declare a target variable
            string generation;

            // Define the target variable
            if (birthYear >= 1995) { generation = "Generation Z"; }
            else if (birthYear >= 1977) { generation = "Millennials"; }
            else if (birthYear >= 1965) { generation = "Lost generation"; }
            else if (birthYear >= 1946) { generation = "Baby-Boomers generation"; }
            else if (birthYear >= 1928) { generation = "Post-War generation"; }
            else if (birthYear >= 1922) { generation = "War generation"; }
            else if (birthYear >= 1912) { generation = "Depression generation"; }
            else { generation = "Undefined"; }

            return generation;
        }
    }
}
