namespace PostApi.Models
{
    public class Post
    {

        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public int Likes { get; set; }
        public int Impressions { get; set; }
    }
}