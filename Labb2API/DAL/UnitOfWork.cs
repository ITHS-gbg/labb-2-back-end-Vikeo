using Labb2API.DAL.Contexts;
using Labb2API.DAL.Respositories;

namespace Labb2API.DAL
{
    public class UnitOfWork : IDisposable
    {
        private WebsiteContext _websiteContext;

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_websiteContext);
                }
                return _userRepository;
            }
        }

        private ICourseRepository _courseRepository;
        public ICourseRepository CourseRepository
        {
            get
            {
                if (_courseRepository is null)
                {
                    _courseRepository = new CourseRepository(_websiteContext);
                }

                return _courseRepository;
            }
        }

        public UnitOfWork(WebsiteContext websiteContext)
        {
            _websiteContext = websiteContext;
        }

        public void Save()
        {
            _websiteContext.SaveChanges();
        }

        public void Dispose()
        {
            //TODO Dessa är null så de kraschar?
            //_websiteContext.Dispose();
            //_userRepository.Dispose();
            //_courseRepository.Dispose();
        }
    }
}
