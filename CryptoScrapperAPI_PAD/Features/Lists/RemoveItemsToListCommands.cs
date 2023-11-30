using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using static CryptoScrapperAPI_PAD.Features.Lists.CreateListCommands;

namespace CryptoScrapperAPI_PAD.Features.Lists
{
    public class RemoveItemsToListCommands
    {
        public class RemoveItemsToListCommand : IRequest<RemoveItemsToListCommandResponse>
        {
            public string Id {  get; set; } 
        }

        public class RemoveItemsToListCommandResponse{
            public bool Success { get; set; }   
        }

        public class Handler : IRequestHandler<RemoveItemsToListCommand, RemoveItemsToListCommandResponse>
        {
            IMongoRepository<CustomListItem> _mongoRepository;
            public Handler(IMongoRepository<CustomListItem> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<RemoveItemsToListCommandResponse> Handle(RemoveItemsToListCommand request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {
                    var filter = Builders<CustomListItem>.Filter.Eq("Id", new ObjectId(request.Id));
                    _mongoRepository.DeleteDocument(filter);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Creating the User {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new RemoveItemsToListCommandResponse()
                {
                    Success = success
                };
            }
        }
    }
}
