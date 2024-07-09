using PostApi.Models;
using PostApi.Repository;

namespace PostApi.Services
{
    public class PostService(PostRepository postRepository)
    {
        private readonly PostRepository _postRepository = postRepository;

        public List<Post> ListPost()
        {
            try {
                return _postRepository.ListPost();
            } 
            catch
            {
                throw;
            }
        }

        public Post ShowPost(int id)
        {
            try {
                return _postRepository.Item(id);
            } 
            catch
            {
                throw;
            }
        }

        public void PublishPost(Post post)
        {
            try {
                _postRepository.Create(post);
            } 
            catch
            {
                throw;
            }
        }

        public void UpdatePost(int id, Post post)
        {
            try {
                _postRepository.Update(id, post);
            }
            catch 
            {
                throw;
            }
        }

        public void CancelPost(int id)
        {
            try{
                _postRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}