using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using umvel.challenge.application.Commands.Customer;
using umvel.challenge.application.Commands.Customers;
using umvel.challenge.application.Queries.Customer;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Commons.Rules.Exceptions;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Customers.Rules;
using umvel.challenge.domain.Models.Customers;
using Xunit;
using static umvel.challenge.test.Application.UmvelChallengeTestAsync;

namespace umvel.challenge.test.Application.Customers
{
	public class CustomerTest
	{
		public UmvelChallengeDbContextApplicationFixture fixture;
		public CustomerTest()
		{
			fixture = new UmvelChallengeDbContextApplicationFixture();
		}

		[Theory]
		[InlineData(1)]
		public async Task Get_Customer_By_Customer_Id(int customerId)
		{
			CustomerModel expected = new CustomerModel
			{
				CustomerId = 1,
				Name = "Pedro Alejandro Morales MArtinez"
			};

			var customer = fixture.context.Customers.ToList();
			var customerMuckset = new Mock<DbSet<Customer>>();

			customerMuckset.As<IAsyncEnumerable<Customer>>()
				.Setup(x => x.GetAsyncEnumerator(default))
				.Returns(new TestAsyncEnumerator<Customer>(customer.GetEnumerator()));

			customerMuckset.As<IQueryable<Customer>>()
				.Setup(m => m.Provider)
				.Returns(new TestAsyncQueryProvider<Customer>(customer.AsQueryable().Provider));

			customerMuckset.As<IQueryable<Customer>>()
				.Setup(x => x.Expression)
				.Returns(customer.AsQueryable().Expression);

			customerMuckset.As<IQueryable<Customer>>()
				.Setup(x => x.ElementType).Returns(customer.AsQueryable().ElementType);

			customerMuckset.As<IQueryable<Customer>>()
				.Setup(X => X.GetEnumerator()).Returns(customer.GetEnumerator());

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMuckset.Object);

			GetCustomerByCustomerIdQuery query = new GetCustomerByCustomerIdQuery(customerId);
			GetCustomerByCustomerIdQueryValidator validator = new GetCustomerByCustomerIdQueryValidator();
			GetCustomerByCustomerIdQueryHandler handler = new GetCustomerByCustomerIdQueryHandler(validator, context.Object);
			var result = await handler.Handle(query, default(CancellationToken));

			Assert.Equal(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(expected));
		}

		[Theory]
		[InlineData(0, "'CustomerId' debe ser mayor a '0'")]
		public async Task Get_Customer_By_Customer_Id_Invalid(int customerId, string message)
		{
			var customerMockset = new Mock<DbSet<Customer>>();

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockset.Object);

            GetCustomerByCustomerIdQuery query = new GetCustomerByCustomerIdQuery(customerId);
            GetCustomerByCustomerIdQueryValidator validator = new GetCustomerByCustomerIdQueryValidator();
            GetCustomerByCustomerIdQueryHandler handler = new GetCustomerByCustomerIdQueryHandler(validator, context.Object);
			var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
				await handler.Handle(query, new CancellationToken())
			);

			Assert.Contains(message, exception.Message);
        }

		[Fact]
		public async Task Create_Customer_Command()
		{
			int customerId = 0;
			string name = "Raul Suarez Garcia";

			var customerMockSet = new Mock<DbSet<Customer>>();

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockSet.Object);

			var customerAggregate = new Mock<ICustomerAggregate>();

			customerAggregate.Setup(c => c.Customer).Returns(Customer.Create(customerId, name));

			CreateCustomerCommandHandler commandHandler = new CreateCustomerCommandHandler(
				new CreateCustomerCommandValidator(),
				customerAggregate.Object,
				context.Object);

			CustomerModel request = new CustomerModel()
			{
				CustomerId = customerId,
				Name = name
			};

			CreateCustomerCommand command = new CreateCustomerCommand(request);
			var response = await commandHandler.Handle(command, new CancellationToken());

			customerMockSet.Verify(m => m.AddAsync(It.IsAny<Customer>(), new CancellationToken()), Times.Once());
			context.Verify(m => m.SaveChangesAsync(new CancellationToken()), Times.Exactly(3));
			Assert.Equal(JsonConvert.SerializeObject(response), JsonConvert.SerializeObject(request));
        }

		[Theory]
		[InlineData(0,"Liliana Villegas", "CustomerId must be greater than zero")]
		public async Task Create_Customer_Request_Validation_Fails(int customerId, string name, string message)
		{
			var customerMockSet = new Mock<DbSet<Customer>>();

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockSet.Object);

			var customerAggregate = new Mock<ICustomerAggregate>();
			customerAggregate.Setup(c => c.Customer).Returns(Customer.Create(customerId, name));

			CreateCustomerCommandHandler commandHandler = new CreateCustomerCommandHandler(
				new CreateCustomerCommandValidator(),
				customerAggregate.Object,
				context.Object);

			CustomerModel request = new CustomerModel()
			{
				CustomerId = customerId,
				Name = name,
			};

			CreateCustomerCommand command = new CreateCustomerCommand(request);
			var exception = await Assert.ThrowsAsync<ValidationException>(() =>
				commandHandler.Handle(command, new CancellationToken())
			);

			Assert.Contains(message, exception.Message);
		}

		[Theory]
		[InlineData(5,"Pedro Morales Martinez", "The person id 5 does not exist for an customer")]
		public async Task Customer_Domain_Rules_CustomerId_Must_exist_Fails(
			int customerId,
			string name,
			string message
			)
		{
			CustomerModel request = new CustomerModel()
			{
				CustomerId = customerId,
				Name = name
			};

			var customerMockset = new Mock<DbSet<Customer>>();

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockset.Object);

			var customerAggregate = new Mock<ICustomerAggregate>();
			customerAggregate.Setup(c => c.AddCustomer(request)).Throws<InvalidRuleException>(() =>
			{
				var rule = new CustomerIdMustExistRule(context.Object) { CustomerId = customerId };
				throw new InvalidRuleException(rule);
			});

			CreateCustomerCommandHandler commandHandler = new CreateCustomerCommandHandler(
					new CreateCustomerCommandValidator(),
					customerAggregate.Object,
					context.Object);

			CreateCustomerCommand command = new CreateCustomerCommand(request);

			var exception = await Assert.ThrowsAsync<InvalidRuleException>(() => commandHandler.Handle(command, new CancellationToken()));

			Assert.Contains(message, exception.Message);
		}

		[Fact]
		public async Task Update_Customer_Command()
		{
			int customerId = 1;
			string name = "Felipe villegas";

			CustomerModel customerModel = new CustomerModel
			{
				CustomerId = customerId,
				Name = name
			};

			var customer = fixture.context.Customers.ToList();

			var customerMockSet = new Mock<DbSet<Customer>>();
			customerMockSet.As<IQueryable<Customer>>().Setup(v => v.Provider).Returns(customer.AsQueryable().Provider);
            customerMockSet.As<IQueryable<Customer>>().Setup(v => v.Expression).Returns(customer.AsQueryable().Expression);
            customerMockSet.As<IQueryable<Customer>>().Setup(v => v.ElementType).Returns(customer.AsQueryable().ElementType);
            customerMockSet.As<IQueryable<Customer>>().Setup(v => v.GetEnumerator()).Returns(customer.GetEnumerator());

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockSet.Object);

			var customerAggregate = new Mock<ICustomerAggregate>();
			customerAggregate.Setup(c => c.Customer).Returns(Customer.Create(customerId, name));

			UpdateCustomerCommandHandler commandHandler = new UpdateCustomerCommandHandler(
				new UpdateCustomerCommandValidator(),
				customerAggregate.Object,
				context.Object);

			CustomerModel request = new CustomerModel()
			{
				CustomerId = customerId,
				Name = name
			};

			UpdateCustomerCommand command = new UpdateCustomerCommand(request);
			var response = await commandHandler.Handle(command, new CancellationToken());

			customerMockSet.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once());
			context.Verify(m => m.SaveChanges(), Times.Once());

			Assert.Equal(JsonConvert.SerializeObject(response), JsonConvert.SerializeObject(request));
        }

		[Theory]
		[InlineData(0, "Jose de jesus Contreras", "CustomerId must be greater than zero")]
		[InlineData(9, null, "Name must not be null")]
		public async Task Update_Customer_Request_Validation_Fails(
			int customerId,
			string name,
			string message
			)
		{
			var customerMockSet = new Mock<DbSet<Customer>>();

			var context = new Mock<IUmvelChallengeDbContext>();
			context.Setup(c => c.Customers).Returns(customerMockSet.Object);

			var customerAggregate = new Mock<ICustomerAggregate>();
			customerAggregate.Setup(v => v.Customer).Returns(Customer.Create(customerId, name));

			UpdateCustomerCommandHandler commandHandler = new UpdateCustomerCommandHandler(
				new UpdateCustomerCommandValidator(),
				customerAggregate.Object,
				context.Object);

			CustomerModel requst = new CustomerModel()
			{
				CustomerId = customerId,
				Name = name
			};

			UpdateCustomerCommand command = new UpdateCustomerCommand(requst);

			var exception = await Assert.ThrowsAsync<ValidationException>(() =>
				commandHandler.Handle(command, new CancellationToken())
			);

			Assert.Contains(message, exception.Message);
		}
	}
}

