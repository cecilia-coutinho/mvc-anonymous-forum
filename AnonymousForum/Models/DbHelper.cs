using AnonymousForum.Data;
using Microsoft.EntityFrameworkCore;

namespace AnonymousForum.Models
{
    public class DbHelper
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AnonymousForumContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AnonymousForumContext>>()))
            {
                // Look for any topics
                if (context.Topics.Any())
                {
                    return;   // DB has been seeded
                }

                var topics = new List<Topic>
                {
                    new Topic { TopicName = "School" },
                    new Topic { TopicName = "Film" },
                    new Topic { TopicName = "Books" },
                    new Topic { TopicName = "Games" },
                    new Topic { TopicName = "Music" }
                };

                context.Topics.AddRange(topics);
                context.SaveChanges();

                // Seed threads with generated topic's IDs
                var threads = new List<Thread>
                {
                    new Thread 
                    { 
                        ThreadTitle = "Best Study Techniques", 
                        ThreadDescription = "I need some study techniques advices for success in school.", 
                        FkTopicId = topics[0].TopicId 
                    },

                   new Thread 
                   { 
                       ThreadTitle = "Top 10 Must-Watch Movies", 
                       ThreadDescription = "Please list your top 10 must-watch movies of all time.", 
                       FkTopicId = topics[1].TopicId 
                   },

                    new Thread 
                    { 
                        ThreadTitle = "Film Recommendations", 
                        ThreadDescription = "I'm looking for movie recommendations. Someone can help here?", 
                        FkTopicId = topics[1].TopicId 
                    },

                    new Thread 
                    { 
                        ThreadTitle = "Favorite Book Genre Fantasy", 
                        ThreadDescription = "Looking for a new book to read. What is the best fantasy book of all time?", 
                        FkTopicId = topics[2].TopicId 
                    }
                };

                context.Threads.AddRange(threads);
                context.SaveChanges();

                var replies = new List<Reply>
                {
                    // Replies to "Best Study Techniques" thread
                    new Reply 
                    { 
                        ReplyTitle = "Re: Best Study Techniques", 
                        ReplyDescription = "I find that taking detailed notes during lectures helps a lot.", 
                        FkThreadId = threads[0].ThreadId 
                    },
                    
                    new Reply 
                    { 
                        ReplyDescription = "Flashcards and practice tests have been effective for me.", FkThreadId = threads[0].ThreadId 
                    },

                    // Replies to "Top 10 Must-Watch Movies" thread
                    new Reply 
                    { 
                        ReplyTitle = "Re: Top 10 Must-Watch Movies", 
                        ReplyDescription = "Inception is a must-watch for any movie lover!", 
                        FkThreadId = threads[2].ThreadId 
                    },
                    new Reply 
                    { 
                        ReplyTitle = "Shawshank Redemption", 
                        ReplyDescription = "Shawshank Redemption is my all-time favorite.", 
                        FkThreadId = threads[2].ThreadId 
                    },

                    // Replies to "Favorite Book Genre Fantasy" thread
                    new Reply 
                    { 
                        ReplyTitle = "Re: Favorite Book Genre Fantasy", 
                        ReplyDescription = "I'm a fan of fantasy novels, especially Tolkien's works.", 
                        FkThreadId = threads[4].ThreadId 
                    },
                    
                    new Reply 
                    {
                        ReplyDescription = "I highly recommend 'The Lord of the Rings' trilogy by J.R.R. Tolkien. It's a classic in the fantasy genre.",
                        FkThreadId = threads[4].ThreadId 
                    }
                    // Add more replies as needed
                };

                context.Replies.AddRange(replies);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving changes to the database: {ex}");
                    throw;
                }
            }
        }

        public static Account UserData()
        {
            Account account = new Account{
                Username = "admin",
                Password = "1234"
            };

            return account;
        }
    }
}

