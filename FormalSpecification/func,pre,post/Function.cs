using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormalSpecification
{
    class Function
    {
        string function_name = "";
        Dictionary<string, string> function_variable;
        KeyValuePair<string, string> function_result;

        public
        void splitFunction(string func)
        {
            Regex rx_name = new Regex(@".*(?=\()");
            Regex rx_variable = new Regex(@"\((.*?)\)");
            Regex rx_result = new Regex(@"\)(.+)$");

            function_name = rx_name.Match(func).Value;
            splitVariable(rx_variable.Match(func).Groups[1].Value);
            getResult(rx_result.Match(func).Groups[1].Value);
        }
        void splitVariable(string variable)
        {
            string[] splitVariable1 = Regex.Split(variable, @",");

            Regex rx_variable_name = new Regex(@".*(?=:)");
            Regex rx_variable_type = new Regex(@":(.+)$");

            foreach (string value in splitVariable1)
            {
                function_variable.Add(rx_variable_name.Match(variable).Value, rx_variable_type.Match(variable).Groups[1].Value);
            }
            foreach (KeyValuePair<string, string> ele in function_variable)
            {
                Console.WriteLine("Key = {0}, Value = {1}", ele.Key, ele.Value);
            }
        }
        void getResult(string result)
        {
            Regex rx_resuilt_name = new Regex(@".*(?=:)");
            Regex rx_resuilt_type = new Regex(@":(.+)$");
            function_result = new KeyValuePair<string, string>(rx_resuilt_name.Match(result).Value, rx_resuilt_type.Match(result).Groups[1].Value);       
        }
    }
}
