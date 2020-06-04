using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.StoreContext.CustomerCommands.Inputs;
using Store.Domain.StoreContext.CustomerCommands.Outputs;
using Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.Queries;
using Store.Domain.StoreContext.Repositories;
using Store.Shared.Commands;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;

        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [ResponseCache(Duration = 15)]
        public ActionResult<IEnumerable<ListCustomerQueryResult>> Get()
        {
            return Ok(_repository.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<GetCustomerQueryResult> GetById(Guid id)
        {
            return Ok(_repository.Get(id));
        }

        [HttpGet]
        [Route("api/v2/customers/{document}")]
        public ActionResult<GetCustomerQueryResult> GetByDocument(Guid document)
        {
            return Ok(_repository.Get(document));
        }

        [HttpGet("{id}/orders")]
        public ActionResult<IEnumerable<ListCustomerOrdersQueryResult>> GetOrders(Guid id)
        {
            return Ok(_repository.GetOrders(id));
        }

        [HttpPost]
        public ICommandResult Post([FromBody] CreateCustomerCommand command)
        {
            var result = (CreateCustomerCommandResult)_handler.Handle(command);
            return result;
        }
    }
}