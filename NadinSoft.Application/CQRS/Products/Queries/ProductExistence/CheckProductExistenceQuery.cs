using MediatR;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Queries.ProductExistence
{
    public class CheckProductExistenceQuery : IRequest<bool>
    {
        public string? ManufactureEmail { get; set; }
        public DateTime? ProduceDate { get; set; }
    }

    public class CheckProductExistenceQueryHandler : IRequestHandler<CheckProductExistenceQuery, bool>
    {
        private readonly IProductRepository _repository;

        public CheckProductExistenceQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CheckProductExistenceQuery request, CancellationToken cancellationToken)
        {
            var isExist = false;

            if (!string.IsNullOrEmpty(request.ManufactureEmail))
            {
                isExist = await _repository.AnyAsync(x => x.ManufactureEmail == request.ManufactureEmail);
            }
            if (request.ProduceDate != null && !isExist)
            {
                isExist = await _repository.AnyAsync(x => x.ProduceDate == request.ProduceDate);
            }
            return isExist;
        }
    }
}
