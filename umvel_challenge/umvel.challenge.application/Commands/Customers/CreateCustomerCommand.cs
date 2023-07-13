using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using umvel.challenge.application.Exceptions;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.application.Commands.Customer
{
	public class CreateCustomerCommand: IRequest<CustomerModel>
	{
		public CreateCustomerCommand(CustomerModel customer)
		{
			Customer = customer;
		}

		public CustomerModel Customer { get; set; }
	}

	public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, CustomerModel>
	{
		private readonly IValidator<CreateCustomerCommand> validator;
		private readonly ICustomerAggregate customerAggregate;
		private readonly IUmvelChallengeDbContext context;

		public CreateCustomerCommandHandler(
            IValidator<CreateCustomerCommand> validator,
            ICustomerAggregate customerAggregate,
            IUmvelChallengeDbContext context
			)
		{
			this.validator = validator;
			this.customerAggregate = customerAggregate;
			this.context = context;
		}

		public async Task<CustomerModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request);

			using (context)
			{
				customerAggregate.AddCustomer(request.Customer);

				try
				{
					await context.Customers.AddAsync(customerAggregate.Customer);
					await context.SaveChangesAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					throw new ServiceException($"An error ocurred while inserting the entity", ex, ResponseCode.ServiceError);
				}

				return request.Customer;
			}
		}
	}

	public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
	{
		public CreateCustomerCommandValidator()
		{
			RuleFor(p => p.Customer).NotNull().WithMessage("Customer must not be null");
			RuleFor(p => p.Customer.Name).NotNull().NotEmpty().WithMessage("Name must not be null or empty");
		}
	}
}

