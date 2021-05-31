using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Validations
{
    public class CPFAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = value as string;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) {
                return new ValidationResult(GetErrorMessage());;
            }

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for(int i = 0; i < 9; i++) {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            int rest = sum % 11;
            if ( rest < 2 ) {
                rest = 0;
            } else {
                rest = 11 - rest;
            }

            string digit = rest.ToString();
            tempCpf = tempCpf + digit;

            sum = 0;
            for(int i = 0; i < 10; i++) {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;
            if (rest < 2) {
                rest = 0;
            } else {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();

            return cpf.EndsWith(digit)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"CPF inválido";
        }
    }
}