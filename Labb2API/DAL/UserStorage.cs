namespace Labb2API.DAL.Models
{
    public class UserStorage
    {
        private readonly IDictionary<int, User> _users;

        private int _id;

        public UserStorage()
        {
            _users = new Dictionary<int, User>();
        }

        public bool CreateUser(User user)
        {
            if (_users.Values.Contains(user)) return false;

            _users.Add(_id++, user);
            return true;
        }

        public ICollection<User>? GetAllCourses()
        {
            return _users.Values;
        }

        //Kollar om det finns en user med en email, returnerar om den finns.
        public User GetUser(string email)
        {
            foreach (var user in _users.Values)
            {
                if (user.Email == email) return user;
            }

            return null;
        }

        public bool UpdateUser(User user)
        {
            //TODO Scuffed sätt att hitta rätt keyvalue på.
            var id = 0;

            foreach (var u in _users.Values)
            {
                if (u == user)
                {
                    //TODO Kommer jag ut ur if-satsen eller foreachen?
                    break;
                }
                id++;
            }

            if (!_users.Keys.Contains(id)) return false;
            _users[id] = user;
            return true;
        }

        //TODO Gör Add/Delete metoder istället för denna.
        public bool UpdateActiveCourses(int id, List<Course> courses)
        {
            if (courses.Count <= 0) return false;

            _users[id].ActiveCourses = courses;

            return true;
        }

        public bool DeleteUser(User user)
        {
            //TODO Scuffed sätt att hitta rätt keyvalue på.
            var id = 0;

            foreach (var u in _users.Values)
            {
                if (u == user)
                {
                    //TODO Kommer jag ut ur if-satsen eller foreachen?
                    break;
                }
                id++;
            }

            if (!_users.Keys.Contains(id)) return false;
            _users[id] = user;
            return true;
        }
    }
}
}