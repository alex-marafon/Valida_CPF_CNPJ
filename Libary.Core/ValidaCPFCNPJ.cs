using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Libary.Core
{
    public static class ValidaCPFCNPJ
    {

        public enum TaxIdType
        {
            [Description("cfp")]
            CPF,
            [Description("cnpj")]
            CNPJ
        }

        public class InvalidTaxIdLength : Exception
        {
            public InvalidTaxIdLength() : base("Valor informado não condis com CPF ou CNPJ valido") { }
        }

        /// <summary>
        /// Informe CPF ou CNPJ
        /// </summary>
        public static class TaxIdValidation
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="taxId"></param>
            /// <returns>Item1: Returns the Type of the TaxId<br/>
            /// Item2: Return if the taxId is valid</returns>
            public static Tuple<TaxIdType, bool> ValidateTaxId(string taxId)
            {

                var limpa = Regex.Replace(taxId, @"[^0-9]+", "");
                var strippedTaxId = limpa;


                switch (taxId.Length)
                {
                    case 11:
                        return new Tuple<TaxIdType, bool>(TaxIdType.CPF, ValidateCPF(strippedTaxId));
                    case 14:
                        return new Tuple<TaxIdType, bool>(TaxIdType.CNPJ, ValidateCNPJ(strippedTaxId));
                    default:
                        throw new InvalidTaxIdLength();
                }
            }
            //Valida CPF
            public static bool ValidateCPF(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                cpf = cpf.Trim().Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;

                for (int j = 0; j < 10; j++)
                    if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                        return false;

                string tempCpf = cpf.Substring(0, 9);
                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                int resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            }

            //Valida CNPJ
            private static bool ValidateCNPJ(string cnpj)
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                    return false;

                string tempCnpj = cnpj.Substring(0, 12);
                int soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                int resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return cnpj.EndsWith(digito);
            }

        }

    }

}
