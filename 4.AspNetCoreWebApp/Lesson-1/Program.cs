using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace Lesson_1
{
    class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(UserId.ToString());
            sb.AppendLine(Id.ToString());
            sb.AppendLine(Title);
            sb.AppendLine(Body);
            return sb.ToString();
        }
    }
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string url = "https://jsonplaceholder.typicode.com/posts/";
        static readonly string fileName = "result.txt";

        static async Task Main(string[] args)
        {
            Post post = await GetPostAsync(1);
            if (post != null)
            {
                File.WriteAllText(fileName, post.ToString());
                Console.WriteLine(post.ToString());
            }
            Console.WriteLine("Done. Press any key!");
            Console.ReadKey();
        }

        static async Task<Post> GetPostAsync(int id)
        {
            try
            {
                string path = $"{url}{id}";
                var jsonText = await client.GetStringAsync(path);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                return JsonSerializer.Deserialize<Post>(jsonText, serializeOptions);
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
