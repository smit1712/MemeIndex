using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reddit;
using Reddit.Controllers;

namespace Scrapers
{
    public abstract class RedditScraperBase : ScraperBase
    {
        private string _subreddit;
        public RedditScraperBase(string subreddit)
        {
            _subreddit = subreddit;
        }

        public override Task<List<Meme>> Scrape()
        {
            // You can also pass them as named parameters.
            //TODO: move secret and APPIds
            RedditClient reddit = new RedditClient(appId: "KsG-9J6a32q8OaZSDAwhfQ", appSecret: "a9GFJ-rZFFPgDV5D78WQLLPOjWEdFw", accessToken: "55145664-cQ03V0i_Y2Y-FJx9Ak73c_StXvjXdQ", refreshToken: "55145664-AZGJV7k8U0O0x-tZ9lEFdq_RKnYFuQ");
            List<Meme> memes = new();

            try
            {
                List<Post> ScrapedPosts = new List<Post>();

                //Funny
                Subreddit subreddit = reddit.Subreddit(_subreddit);
                ScrapedPosts.AddRange(GetSubRedditPosts(subreddit));

                ////Memes
                Subreddit Rmeme = reddit.Subreddit("meme");
                ScrapedPosts.AddRange(GetSubRedditPosts(Rmeme));

                ////dankmemes
                Subreddit Rdankmemes = reddit.Subreddit("dankmemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(Rdankmemes));

                ////prequelMemes
                Subreddit RprequelMemes = reddit.Subreddit("PrequelMemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(RprequelMemes));

                ////lotrmemes
                Subreddit Rlotrmemes = reddit.Subreddit("lotrmemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(Rlotrmemes));

                ////dankmemes
                Subreddit RAnimemes = reddit.Subreddit("Animemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(RAnimemes));

                ////dankmemes
                Subreddit Rdndmemes = reddit.Subreddit("dndmemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(Rdndmemes));

                ////dankmemes
                Subreddit RHistoryMemes = reddit.Subreddit("HistoryMemes");
                ScrapedPosts.AddRange(GetSubRedditPosts(RHistoryMemes));

                return Task.FromResult(ConvertToMeme(ScrapedPosts));
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
            }

            return Task.FromResult(memes);
        }

        private List<Post> GetSubRedditPosts(Subreddit subreddit)
        {
            List<Post> posts = new List<Post>();

            posts.AddRange(subreddit.Posts.New);
            posts.AddRange(subreddit.Posts.Top);
            posts.AddRange(subreddit.Posts.Hot);

            return posts;
        }

        private List<Meme> ConvertToMeme(List<Post> posts)
        {
            List<Meme> memes = new();

            foreach (Post post in posts)
            {
                if (post.Listing.IsVideo) continue;

                List<string> metaData = new List<string>();
                metaData.Add(post.Author);
                metaData.Add(post.Subreddit);

                memes.Add(new Meme(post.Title, metaData, post.Listing.URL, post.UpVotes, post.Permalink, post.Id));
            }

            return memes;
        }
    }
}
