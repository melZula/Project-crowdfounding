using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        public Store store;
        public Server server;
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            store = new Store("Host=localhost;Port=5433;Database=foundb;Username=postgres;Password=peterina");
            server = new Server(store);
        }

        public override Task<Amount> GetFoundBalance(Id request, ServerCallContext content)
        {
            return Task.FromResult(new Amount
            {
                Value = server.store.GetFoundBalance(request.Id_)
            });
        }
        public override Task<Amount> GetUserBalance(Id request, ServerCallContext content)
        {
            return Task.FromResult(new Amount
            {
                Value = server.store.GetUserBalance(request.Id_)
            });
        }
        public override Task<Added> GiveToFound(UserToFound request, ServerCallContext content)
        {
            return Task.FromResult(new Added
            {
                Status = server.store.GiveToFound(request.UserId, request.FoundId, request.Value)
            });
        }
        public override Task<Added> AddBalance(UserAmount request, ServerCallContext content)
        {
            return Task.FromResult(new Added
            {
                Status = server.store.AddBalance(request.UserId, request.Value)
            });
        }
        public override Task<Added> CreateFound(FoundOwner request, ServerCallContext content)
        {
            return Task.FromResult(new Added
            {
                Status = server.store.CreateFound(request.Name, request.Owner) == null
            });
        }
    }
}
