using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;
using MongoDB.Driver;

namespace CryptoScrapperAPI_PAD.Features.Users
{
    public class LoginCommands
    {
        public class  LoginCommand : IRequest<LoginCommandResponse>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginCommandResponse
        {
            public bool Success { get; set; }
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<LoginCommand, LoginCommandResponse>
        {
            IMongoRepository<User> _mongoRepository;
            public Handler(IMongoRepository<User> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                bool success;
                string id = "";
                try
                {
                    var builder = Builders<User>.Filter;
                    var filter = builder.Eq(user => user.Email,request.Email) & builder.Eq(user => user.Password,request.Password);
                    var user = _mongoRepository.GetDocument(filter);
                    
                    success = user != null;
                    if (success)
                    {
                        id = user.Id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Creating the User {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new LoginCommandResponse()
                {
                    Success = success,
                    Id = id

                };
            }
        }
    }
}
