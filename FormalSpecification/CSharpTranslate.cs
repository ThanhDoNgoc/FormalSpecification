using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace FormalSpecification
{
    class CSharpTranslate
    {
        Provider provider;
        string formatPath = "C:\\Users\\Admin\\Desktop\\FormalSpecification\\FormalSpecification\\CSharpFormat.txt";
        string format;
        Dictionary<string, string> variables = new Dictionary<string, string>();
        public KeyValuePair<string, string> result;

        public void CSharp_transtale(Provider provider1)
        {
            provider = provider1;

            translateVariable();
            translateResuilt();
        }
        void translateVariable()
        {

            foreach (KeyValuePair<string, string> variable in provider.function_variable)
            {
                foreach (KeyValuePair<string, string> type in variable_type)
                {
                    if (variable.Value == type.Key)
                    {
                        variables.Add(variable.Key, type.Value);
                    }
                }
            }
        }
        void translateResuilt()
        {
            foreach (KeyValuePair<string, string> type in variable_type)
            {
                if (provider.function_result.Value == type.Key)
                {
                    result = new KeyValuePair<string, string>(provider.function_result.Key, type.Value);
                }
            }
        }
        public string generateCSharpCode()
        {
            var str = File.ReadAllText(formatPath);
            format = str;

            format = Regex.Replace(format, @"@functionname", provider.function_name);
            format = Regex.Replace(format, @"@refvariable", refvariable());
            format = Regex.Replace(format, @"@strvariable", strvariable());
            format = Regex.Replace(format, @"@mainrefvariable", mainrefvariable());
            format = Regex.Replace(format, @"@mainstrvariable", mainstrvariable());
            format = Regex.Replace(format, @"@classname", provider.classname);
            format = Regex.Replace(format, @"@pre", strpre());
            format = Regex.Replace(format, @"@resulttype", result.Value);
            format = Regex.Replace(format, @"@resultkey", result.Key);
            format = Regex.Replace(format, @"@nhap_variable", nhap_variable());
            format = Regex.Replace(format, @"@main_variable", main_variable());
            format = Regex.Replace(format, @"@condiction", strcondiction());
            format = Regex.Replace(format, @"@resultfirstvalue", resultFirstValue());
            return format;
        }
        string refvariable()
        {
            string str = "";

            foreach (KeyValuePair<string, string> vari in variables)
            {
                str += "ref" + " " + vari.Value + " " + vari.Key + ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        string strvariable()
        {
            string str = "";

            foreach (KeyValuePair<string, string> vari in variables)
            {
                str += vari.Value + " " + vari.Key + ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        string mainrefvariable()
        {
            string str = "";

            foreach (KeyValuePair<string, string> vari in variables)
            {
                str += "ref" + " " + vari.Key + ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        string mainstrvariable()
        {
            string str = "";

            foreach (KeyValuePair<string, string> vari in variables)
            {
                str += vari.Key + ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        string strpre()
        {
            string str = "";
            if (provider.pre != "")
            {
                str = "if( !(" + provider.pre + ")) { return 0; }"; 
            }
            return str;
        }
        string nhap_variable()
        {
            string str = "";
            foreach (KeyValuePair<string, string> vari in variables)
            {
                str = str + "\t\t\tConsole.WriteLine(\"Nhap " + vari.Key + ": \"); " +"\n";
                str = str + "\t\t\t" + vari.Key + " = " + vari.Value + ".Parse(Console.ReadLine());\n";
            }
            return str;
        }
        string main_variable()
        {
            string str = "";
            foreach (KeyValuePair<string, string> vari in variables)
            {
                string firstvalue = "";
                if (vari.Value == "int" || vari.Value == "float")
                    firstvalue = "0";
                else if (vari.Value == "bool")
                    firstvalue = "false";
                else if (vari.Value == "string")
                    firstvalue = "\"\"";
                str = str + "\t\t\t"+vari.Value+" " + vari.Key +" = "+firstvalue +";\n";
            }
            return str;
        }
        string strcondiction()
        {
            string str ="";
            foreach (KeyValuePair<string, string> con in provider.post_condition)
            {
                if (con.Key == "post")
                    str = str + "\t\t\t" + con.Value.ToLower() + ";\n";
                else
                    str = str + "\t\t\tif(" + con.Key + ") {" + con.Value.ToLower() + ";}\n"; 
            }

            return str;
        }
        string resultFirstValue()
        {
            string firstvalue = "";
            if (result.Value == "int" || result.Value == "float")
                firstvalue = "0";
            else if (result.Value == "bool")
                firstvalue = "false";
            else if (result.Value == "string")
                firstvalue = "\"\"";
            return firstvalue;
        }
        Dictionary<string, string> variable_type = new Dictionary<string, string>()
        {
            {"Z", "int" },
            {"R", "float" },
            {"B", "bool" },
            {"char*", "string" }
        };
    }
}
