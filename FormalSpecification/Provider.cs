using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormalSpecification
{
    public class Provider
    {

        public string function = "";
        public string pre = "";
        public string post = "";
        public string classname;

        public string function_name = "";
        public Dictionary<string, string> function_variable = new Dictionary<string, string>();
        public KeyValuePair<string, string> function_result;

        public Dictionary<string, string> post_condition = new Dictionary<string, string>();

        public Provider(string class_name, string formal)
        {
            classname = class_name;
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
            handlePost(post);
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

        void handlePre(string pres)
        {
            if (pres == "")
                return;
            else
            {
                pres = "!(" + pres + ")";
                Console.WriteLine("new pre '{0}' .", pres);
            }
        }
        void handlePost(string post)
        {
            post = Regex.Replace(post, @"\(+", string.Empty);
            Console.WriteLine("post '{0}'.", post);
            post = Regex.Replace(post, @"\)+", string.Empty);
            Console.WriteLine("post '{0}'.", post);

            Regex rx_resuilt = new Regex(@"^.*?(?=&&)");
            Regex rx_condition = new Regex(@"&&(.*)");

            Match canSplit = Regex.Match(post, @"\|\|");
            Match haveCondition = Regex.Match(post, @"&&");

            if (canSplit.Success)
            {
                string[] splitPost = Regex.Split(post, @"\|\|");
                foreach (string value in splitPost)
                {
                    Console.WriteLine("value '{0}'.", value);
                    Console.WriteLine("resuilt '{0}'.", rx_resuilt.Match(value).Value);
                    Console.WriteLine("condition '{0}'.", rx_condition.Match(value).Groups[1].Value);
                    post_condition.Add(rx_condition.Match(value).Groups[1].Value, rx_resuilt.Match(value).Value);
                }
            }
            else if (haveCondition.Success)
            {
                post_condition.Add(rx_condition.Match(post).Groups[1].Value, rx_resuilt.Match(post).Value);
            }
            else
            {
                post_condition.Add("post", post);
            }
        }
    }
}
