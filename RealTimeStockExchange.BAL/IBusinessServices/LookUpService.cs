using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public class LookUpService : ILookupService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISessionService _sessionService;

        public LookUpService(IUnitOfWork unitOfWork, ISessionService sessionService)
        {
            _uow = unitOfWork;
            _sessionService = sessionService;
        }

        public IEnumerable<LookUpDTO> GetLookUps(string type)
        {
            IEnumerable<LookUpDTO> result = new List<LookUpDTO>();

            switch (type.ToLower())
            {
                case "stock":
                    result = _uow.Repository<Stock>().GetAll()
                        .Select(x => new LookUpDTO
                        {
                            Id = x.Id,
                            Text = x.Symbol
                        });
                    break;
            }

            return result;
        }
    }
}
