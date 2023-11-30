using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using MediatR;

namespace CryptoScrapperAPI_PAD.Features.Lists
{
    public class AddItemsToListCommands
    {
        public class AddItemsToListCommand : IRequest<AddItemsToListCommandResponse>
        {
            public string CoinName { get; set; }
            public string CoinId { get; set; }
            public string ListId { get; set; }  
        }

        public class AddItemsToListCommandResponse
        {
            public bool Success { get; set; }
        }

        public class Handler : IRequestHandler<AddItemsToListCommand, AddItemsToListCommandResponse>
        {
            IMongoRepository<CustomListItem> _mongoRepository;
            public Handler(IMongoRepository<CustomListItem> mongoRepository)
            {
                _mongoRepository = mongoRepository;
            }

            public async Task<AddItemsToListCommandResponse> Handle(AddItemsToListCommand request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {
                    CustomListItem customListItem = new CustomListItem()
                    {
                        Name = request.CoinName,
                        ExternalId = request.CoinId,
                        CustomListId = request.ListId,
                    };
                    _mongoRepository.InsertDocument(customListItem);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Adding List Item{ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new AddItemsToListCommandResponse()
                {
                    Success = success
                };
            }
        }

    }
}
