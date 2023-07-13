using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.application.Exceptions;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.application.Commands.Customers
{
	public class UpdateCustomerCommand : IRequest<CustomerModel>
	{
		public UpdateCustomerCommand(CustomerModel customer)
		{
			Customer = customer;
		}

		public CustomerModel Customer { get; set; }
	}

	public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, CustomerModel>
	{
		private readonly IValidator<UpdateCustomerCommand> validator;
		private readonly ICustomerAggregate customerAggregate;
		private readonly IUmvelChallengeDbContext context;

		public UpdateCustomerCommandHandler(
            IValidator<UpdateCustomerCommand> validator,
            ICustomerAggregate customerAggregate,
            IUmvelChallengeDbContext context
			)
		{
			this.validator = validator;
			this.customerAggregate = customerAggregate;
			this.context = context;
		}

		public async Task<CustomerModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request);

			using (context)
			{
				if (DidTheCustomerChange(request))
				{
					customerAggregate.UpdateCustomer(request.Customer);

					try
					{
						context.Customers.Update(customerAggregate.Customer);
						context.SaveChanges();
					}
					catch (Exception ex)
					{
						throw new ServiceException($"An error ocurred while updating the entity", ex, ResponseCode.ServiceError);
					}
				}
			}

			return request.Customer;
		}

		private bool DidTheCustomerChange(UpdateCustomerCommand request)
		{
            umvel.challenge.domain.Entities.Customer currentCustomer = context.Customers
					.AsNoTracking()
					.SingleOrDefault(s => s.CustomerId == request.Customer.CustomerId);

			if (currentCustomer != null && (currentCustomer.Name != request.Customer.Name))
			{
				return true;
			}

			return false;
		}
	}

	public class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
	{
		public UpdateCustomerCommandValidator()
		{
			RuleFor(p => p.Customer).NotNull().WithMessage("Customer must not be null");
			RuleFor(p => p.Customer.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
			RuleFor(p => p.Customer.Name).NotNull().NotEmpty().WithMessage("Name must not be null or empty");
		}
	}
}

