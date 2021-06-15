using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormalSpecification
{
    class Provider
    {

        string function = "";
        string pre = "";
        string post = "";

        string function_name = "";
        Dictionary<string, string> function_variable = new Dictionary<string, string>();
        KeyValuePair<string, string> function_result;

        public Provider(string class_name, string formal)
        {
            string formal1 = Regex.Replace(formal, @"\s+", string.Empty);

            Regex rxpre = new Regex(@"pre(.*?)post");
            Regex rxfunc = new Regex(@".*?(?=pre)");
            Regex rxpost = new Regex(@"post(.+)$");

            function = rxfunc.Match(formal1).Value;
            pre = rxpre.Match(formal1).Groups[1].Value;
            post =rxpost.Match(formal1).Groups[1].Value;

            Console.WriteLine("func '{0}'.", function);
            Console.WriteLine("pre '{0}' .", pre);
            Console.WriteLine("post '{0}'.", post);

            handleFunction(function);
            handlePre(pre);
        }

        void handleFunction(string function)
        {
            Regex rx_name = new Regex(@".*(?=\()");
            Regex rx_variable = new Regex(@"\((.*?)\)");
            Regex rx_result = new Regex(@"\)(.+)$");

            function_name = rx_name.Match(function).Value;
            getVariable(rx_variable.Match(function).Groups[1].Value);
            getResult(rx_result.Match(function).Groups[1].Value);

            Console.WriteLine("func name '{0}'.", function_name);
            Console.WriteLine("variables '{0}'.", rx_variable.Match(function).Groups[1].Value);
            Console.WriteLine("result '{0}','{1}'.", function_result.Key,function_result.Value);
            
        }
        void getVariable(string variable)
        {
            string[] splitVariable1 = Regex.Split(variable, @",");

            Regex rx_variable_name = new Regex(@".*(?=:)");
            Regex rx_variable_type = new Regex(@":(.+)$");

            foreach (string value in splitVariable1)
            {
                function_variable.Add(rx_variable_name.Match(value).Value, rx_variable_type.Match(value).Groups[1].Value);
            }
        }
        void getResult(string result)
        {
            Regex rx_resuilt_name = new Regex(@".*(?=:)");
            Regex rx_resuilt_type = new Regex(@":(.+)$");
            function_result = new KeyValuePair<string, string>(rx_resuilt_name.Match(result).Value, rx_resuilt_type.Match(result).Groups[1].Value);
        }

        void handlePre(string pre)
        {
            pre = "!(" + pre + ")";
            Console.WriteLine("new pre '{0}' .", pre);
        }
        void handlePost(string post)
        { 

        }
    }
}
