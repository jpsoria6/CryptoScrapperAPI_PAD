using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using CryptoScrapper.DAL.Repositories;
using CryptoScrapperAPI_PAD.DTOs;
using MediatR;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Web;

namespace CryptoScrapperAPI_PAD.Features.Lists
{
    public class GetListsQuery
    {
        public class GetListQuery : IRequest<GetListQueryResponse>
        {
            public string UserId { get; set; }
        }

        public class GetListQueryResponse
        {
            public List<CustomList> CustomLists { get; set; }
        }

        public class Handler : IRequestHandler<GetListQuery, GetListQueryResponse>
        {
            IMongoRepository<CustomList> _mongoListRepository;
            IMongoRepository<CustomListItem> _mongoListItemRepository;
            public Handler(IMongoRepository<CustomList> mongoListRepository, IMongoRepository<CustomListItem> mongoListItemRepository)
            {
                _mongoListRepository = mongoListRepository;
                _mongoListItemRepository = mongoListItemRepository;
            }
            public async Task<GetListQueryResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
            {
                bool success;
                var customLists = new List<CustomList>();
                try
                {

                    var filter = Builders<CustomList>.Filter.Eq(x => x.UserId, request.UserId);
                    var lists = _mongoListRepository.GetAllDocuments(filter);

                    foreach (var customList in lists)
                    {
                        var listItemFilter = Builders<CustomListItem>.Filter.Eq(x => x.CustomListId, customList.Id);
                        customList.ListItems = _mongoListItemRepository.GetAllDocuments(listItemFilter).ToList();
                        customLists.Add(customList);
                    }

                    return new GetListQueryResponse()
                    {
                        CustomLists = customLists,
                    };

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Getting Coins {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new GetListQueryResponse();
            }
        }
    }
}
