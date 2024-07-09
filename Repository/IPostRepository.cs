using PostApi.Models;

namespace PostApi.Repository
{
    public interface IPostRepository
    {
        List<Post> ListPost();
        Post Item(int id);
        void Create(Post post);
        void Update(int id, Post post);
        void Delete(int id);
    }
}
