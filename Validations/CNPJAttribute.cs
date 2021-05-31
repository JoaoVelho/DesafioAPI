using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Validations
{
    public class CNPJAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cnpj = value as string;

            int[] multiplier1 = new int[12] {5,4,3,2,9,8,7,6,5,4,3,2};
            int[] multiplier2 = new int[13] {6,5,4,3,2,9,8,7,6,5,4,3,2};
            
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14) {
                return new ValidationResult(GetErrorMessage());
            }

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;
            for(int i=0; i<12; i++) {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            }

            int rest = (sum % 11);
            if (rest < 2) {
                rest = 0;
            } else {
                rest = 11 - rest;
            }

            string digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++) {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            }

            rest = (sum % 11);
            if (rest < 2) {
                rest = 0;
            } else {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"CNPJ inválido";
        }
    }
}