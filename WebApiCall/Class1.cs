using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octokit;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.X86;

namespace WebApiCall
{
    public class Class1
    {
        private static GitHubClient clientDetail;


        //It authenticate User with userName and Personal Access Token
        public bool AuthenticateTokenAndGetClient(string UserName, string UserToken)
        {
            try
            {
                clientDetail = new GitHubClient(new ProductHeaderValue("test"));
                var tokenAuth = new Credentials(UserToken);
                clientDetail.Credentials = tokenAuth;

                var task = GetUser(clientDetail);
                User user = task.Result;
                var repositories = clientDetail.Repository.GetAllForCurrent().Result;
                if (user.Login != UserName)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        //It gets all the basic information of the user
        public static async Task<User> GetUser(GitHubClient client)
        {
            var user = await client.User.Current();
            return user;
        }


        // It gets the url of the GitHub Avtar
        public string GetAvtarUrl()
        {
            var task = GetUser(clientDetail);
            User user = task.Result;

            return (user.AvatarUrl);

        }

        //It gets the list of repository and url
        public List<RepoNameAndUrl> GetRepoNameAndUrls()
        {
            var repositories = clientDetail.Repository.GetAllForCurrent().Result;
            List<RepoNameAndUrl> classes = new List<RepoNameAndUrl>();
            foreach (var repository in repositories)
            {
                classes.Add(new RepoNameAndUrl { Name = repository.Name, Url = repository.Url });

            }
            return classes;

        }

        //It gets the date of repository creation date
        public string GetRepositoryCreationDate(string UserName,string RepositoryName )
        {
            var creationDate = clientDetail.Repository.Get(UserName,RepositoryName).Result;
            string date = creationDate.CreatedAt.Date.ToShortDateString();
            return date;
        }

        //It gets the language of the particular repository
        public string GetRepositoryLanguage(string UserName,string RepositoryName)
        {
            var language = clientDetail.Repository.Get(UserName,RepositoryName).Result;
            return language.Language;
        }

        //It gets commit author name,commit message, commit date
        public List<CommitNameAndMessage> GetCommitNameAndMessages(string UserName,string RepositoryName)
        {
            List<CommitNameAndMessage> messages = new List<CommitNameAndMessage>();
            var commits = clientDetail.Repository.Commit.GetAll(UserName,RepositoryName).Result;
            foreach(var cm in commits)
            {
                var comit = clientDetail.Repository.Commit.Get(UserName,RepositoryName, cm.Sha).Result;
                messages.Add(new CommitNameAndMessage { CAuthorName= comit.Commit.Author.Name, CMessage = comit.Commit.Message, CDate= comit.Commit.Committer.Date.Date.ToShortDateString() });
            }
            return messages;
        }

        //It gets total no of commits
        public int GetTotalNoOfCommits(string UserName,string RepositoryName)
        {
            int totalNoOfCommits = 0;
            var commits = clientDetail.Repository.Commit.GetAll(UserName, RepositoryName).Result;
            foreach( var cm in commits)
            {
                totalNoOfCommits++;
            }
            return totalNoOfCommits;
        }

    }

}
         