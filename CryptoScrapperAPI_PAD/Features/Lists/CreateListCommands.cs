using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;
using MongoDB.Driver;

namespace CryptoScrapperAPI_PAD.Features.Lists
{
    public class CreateListCommands
    {
        public class CreateListCommand : IRequest<CreateListCommandResponse>
        {
            public string ListName { get; set; }
            public string UserId { get; set; }
        }

        public class CreateListCommandResponse
        {
            public bool Success { get; set; }
        }

        public class Handler : IRequestHandler<CreateListCommand, CreateListCommandResponse>
        {
            IMongoRepository<CustomList> _mongoRepository;
            public Handler(IMongoRepository<CustomList> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<CreateListCommandResponse> Handle(CreateListCommand request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {
                    CustomList customList = new CustomList()
                    {
                        Name = request.ListName,
                        UserId = request.UserId
                    };
                    _mongoRepository.InsertDocument(customList);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Creating the User {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new CreateListCommandResponse()
                {
                    Success = success
                };
            }
        }
    }
}
