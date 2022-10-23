using WebApiCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octokit;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApiCall.Class1 cs= new WebApiCall.Class1();
            

              bool status= cs.AuthenticateTokenAndGetClient("Sjaiswal08", "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");
            Console.WriteLine(status);
            string shu=cs.GetAvtarUrl();
            Console.WriteLine(shu);
            List<RepoNameAndUrl> list=cs.GetRepoNameAndUrls();
            foreach (RepoNameAndUrl c in list)
            {
                Console.WriteLine(c.Name);
                Console.WriteLine(c.Url);

            }









        }

    }
}



