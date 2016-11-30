using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;

namespace NewsSystem.Data.ViewModels.Users
{
    public class AuthorBasicViewModel : IMapFrom<User>
    {
        public string UserName { get; set; }
    }
}
