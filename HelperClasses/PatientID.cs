using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Solution.HelperClasses
{
    public static class PatientID
    {
        public static string CaseIDGeneration(int size)
        {
            Random random = new Random();
            string randomno = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = randomno[random.Next(0, randomno.Length)];
                builder.Append(ch);
            }

            return "PAT" + builder.ToString();
        }
    }
}