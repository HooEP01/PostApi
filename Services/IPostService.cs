using PostApi.Models;

namespace PostApi.Services
{
    public interface IPostService
    {
        List<Post> ListPost();
        Post ShowPost(int id);
        void PublishPost(Post post);
        void UpdatePost(int id, Post post);
        void CancelPost(int id);
    }
}