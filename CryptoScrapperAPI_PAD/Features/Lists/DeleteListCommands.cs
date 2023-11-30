using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using static CryptoScrapperAPI_PAD.Features.Lists.CreateListCommands;

namespace CryptoScrapperAPI_PAD.Features.Lists
{
    public class DeleteListCommands
    {
        public class DeleteListCommand : IRequest<DeleteListCommandResponse>
        {
            public string ListId { get; set; }
        }

        public class DeleteListCommandResponse
        {
            public bool Success { get; set; }
        }


        public class Handler : IRequestHandler<DeleteListCommand, DeleteListCommandResponse>
        {
            IMongoRepository<CustomList> _mongoRepository;
            public Handler(IMongoRepository<CustomList> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<DeleteListCommandResponse> Handle(DeleteListCommand request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {
                    var filter = Builders<CustomList>.Filter.Eq("Id", new ObjectId(request.ListId));
                    var rowsAfected = _mongoRepository.DeleteDocument(filter);
                    success = rowsAfected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Creating the User {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new DeleteListCommandResponse()
                {
                    Success = success
                };
            }
        }
    }
}
