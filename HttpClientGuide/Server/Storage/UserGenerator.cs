using Bogus;
using HttpClientGuide.Shared.Model;

namespace HttpClientGuide.Server.Storage
{
    public static class UserGenerator
    {
        public static IEnumerable<User> GenerateUsers(int count)
        {
            var faker = new Faker<User>()
                .StrictMode(true)
                .RuleFor(user => user.Id, (f, s) => f.Random.Guid().ToString())
                .RuleFor(user => user.Age, (f, s) => f.Random.Int(20, 90))
                .RuleFor(user => user.Email, (f, s) => f.Person.Email)
                .RuleFor(user => user.FirstName, (f, s) => f.Person.FirstName)
                .RuleFor(user => user.LastName, (f, s) => f.Person.LastName)
                .RuleFor(user => user.Username, (f, s) => f.Person.UserName);
            return faker.GenerateLazy(count);
        }
    }
}
