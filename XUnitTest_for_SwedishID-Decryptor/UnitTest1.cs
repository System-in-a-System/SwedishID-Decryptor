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
            string socialSecurityNumber1 = "19700901-1111";
            string socialSecurityNumber2 = "190901-1121";
            string socialSecurityNumber3 = "490901-1121";

            // Act
            Gender gender1 = DefineGender(socialSecurityNumber1);
            Gender gender2 = DefineGender(socialSecurityNumber2);
            Gender gender3 = DefineGender(socialSecurityNumber3);

            uint age1 = DefineAge(socialSecurityNumber1);
            uint age2 = DefineAge(socialSecurityNumber2);
            uint age3 = DefineAge(socialSecurityNumber3);


            // Assert
            Assert.Equal(Gender.Male, gender1);
            Assert.Equal(Gender.Female, gender2);
            Assert.Equal(Gender.Female, gender3);

            Assert.Equal(50, (int)age1);
            Assert.Equal(1, (int)age2);
            Assert.Equal(71, (int)age3);


        }
    }
}
