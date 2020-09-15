using Microsoft.VisualStudio.TestPlatform.TestHost;
using SwedishID_Decryptor;
using static SwedishID_Decryptor.Program;
using System;
using Xunit;


namespace XUnitTest_for_SwedishID_Decryptor
{
    public class UnitTest1
    {
        [Fact]
        public void Test()
        {
            // Arrange
            string socialSecurityNumber1 = ValidateSSNNumber("19700901-1111");
            string socialSecurityNumber2 = ValidateSSNNumber("190901-1121");
            string socialSecurityNumber3 = ValidateSSNNumber("490901-1121");

            // Act
            Gender gender1 = DefineGender(socialSecurityNumber1);
            Gender gender2 = DefineGender(socialSecurityNumber2);
            Gender gender3 = DefineGender(socialSecurityNumber3);

            int age1 = (int) DefineAge(socialSecurityNumber1);
            int age2 = (int) DefineAge(socialSecurityNumber2);
            int age3 = (int) DefineAge(socialSecurityNumber3);

      
            // Assert
            Assert.Equal(Gender.Male, gender1);
            Assert.Equal(Gender.Female, gender2);
            Assert.Equal(Gender.Female, gender3);

            Assert.Equal(50, age1);
            Assert.Equal(1, age2);
            Assert.Equal(71, age3);
            

        }
    }
}
