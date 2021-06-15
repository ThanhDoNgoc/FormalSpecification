using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormalSpecification
{
    class Post
    {
        Dictionary<string, string[]> post_pairs;

        public
            void getPost(string str)
        {
            string strpost = clearPost(str);
        }
        string clearPost(string post)
        {
            string str, str1;
            str = Regex.Replace(post, @"\(+", string.Empty);
            str1 = Regex.Replace(str, @"\)+", string.Empty);
            return str1;
        }
    }
}
