using PostApi.Models;

namespace PostApi.Repository
{
    public class PostRepository
    {
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(ILogger<PostRepository> logger)
        {
            _logger = logger;
        }

        static List<Post> posts =  new List<Post>();

        public List<Post> ListPost()
        {
            return posts;
        }

        public Post Item(int id)
        {
            return FindModel(id);
        }

        public void Create(Post post)
        {
            posts.Add(post);
            foreach (Post p in posts)
            {
                _logger.LogInformation(p.Title + " " + p.Content + " " + p.Id);
            }
        }

        public void Update(int id, Post post)
        {
            Post model = FindModel(id);

            model.Title = post.Title;
            model.Content = post.Content;
        }

        public void Delete(int id)
        {
            Post model = FindModel(id);

            posts.Remove(model);
        }

        private Post FindModel(int id)
        {
            var model = posts.First(post => post.Id == id);
            if (model != null)
            {
                return model;
            }

            throw new KeyNotFoundException("Model not found");
        }
    }
}