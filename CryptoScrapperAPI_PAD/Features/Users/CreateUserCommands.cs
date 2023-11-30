using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;

namespace CryptoScrapperAPI_PAD.Features.Users
{
    public class CreateUserCommands
    {
        public class CommandUser : IRequest<CommandResponseUser>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandResponseUser
        {
            public bool success { get; set; }
        }

        public class Handler : IRequestHandler<CommandUser, CommandResponseUser>
        {
            IMongoRepository<User> _mongoRepository;
            public Handler(IMongoRepository<User> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<CommandResponseUser> Handle(CommandUser request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {
                    var user = new User()
                    {
                        Email = request.Email,
                        Name = request.Name,
                        Password = request.Password
                    };
                    _mongoRepository.InsertDocument(user);
                    success = true;
                } catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Creating the User {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new CommandResponseUser()
                {
                    success = success
                };
            }
        }
    }
}
