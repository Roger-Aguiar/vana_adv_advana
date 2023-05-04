using System;

namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class PersonalDocumentValidator
    {
        private string? PersonalDocument { get; set; }

        public PersonalDocumentValidator(string? personalDocument)
        {
            PersonalDocument = personalDocument;
        }

        public bool ValidateCPF()
        {
            bool isValid = true;
            int[] auxNumbers1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] auxNumbers2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (GetValidDigit(ValidateDigit(auxNumbers1)) == Convert.ToInt32(PersonalDocument?[9].ToString()))
            {
                if (GetValidDigit(ValidateDigit(auxNumbers2)) != Convert.ToInt32(PersonalDocument?[10].ToString()))
                    isValid = false;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        public bool ValidateCNPJ()
        {
            int[] auxNumbers1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] auxNumbers2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            bool isValid = true;

            if (GetValidDigit(ValidateDigit(auxNumbers1)) == Convert.ToInt32(PersonalDocument?[12].ToString()))
            {
                if (GetValidDigit(ValidateDigit(auxNumbers2)) != Convert.ToInt32(PersonalDocument?[13].ToString()))
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        private int ValidateDigit(int[] auxNumbers)
        {
            int digit = 0;
            for (int i = 0; i < auxNumbers.Length; i++)
            {
                digit = digit + (auxNumbers[i] * (Convert.ToInt32(PersonalDocument?[i].ToString())));
            }
            return digit;
        }

        private static int GetValidDigit(int digit)
        {
            if (digit % 11 < 2)
            {
                digit = 0;
            }
            else if (digit % 11 >= 2)
            {
                digit = 11 - digit % 11;
            }
            return digit;
        }
    }
}
