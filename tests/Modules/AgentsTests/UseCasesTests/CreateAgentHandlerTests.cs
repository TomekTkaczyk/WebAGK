using Microsoft.EntityFrameworkCore;
using Moq;
using Testcontainers.PostgreSql;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Module.Agents.UseCases.Commands.CreateAgent;
using WebAGK.Shared.Abstractions.Repositories;

namespace AgentsTests.UseCasesTests;

public class CreateAgentHandlerTests : IAsyncLifetime {
    private readonly Mock<IAgentRepository> _repositoryMock;
    private readonly Mock<IAgentUnitOfWork> _unitOfWorkMock;
    private readonly CreateAgentHandler _handler;

    public CreateAgentHandlerTests() {
        _repositoryMock = new Mock<IAgentRepository>();
        _unitOfWorkMock = new Mock<IAgentUnitOfWork>();
        _handler = new CreateAgentHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

	private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
	.WithImage("postgres:15-alpine")
	.Build();

	public Task DisposeAsync()
	{
		return _postgres.DisposeAsync().AsTask();
	}

	public Task InitializeAsync()
	{
		return _postgres.StartAsync();
	} 

	[Fact]
    public async Task Handle_ShouldThrowInvalidIdentifierException_WhenTaxIdExists() {
        // Arrange
        var _command = new CreateAgentCommand( 
            LastName: "Tkaczyk",
            FirstName: "Tomasz",
            SecondName: "Andrzej",
            TaxId: "6941297604",
            PersonalId: "68102910014",
            IsActive: true,
            IsCompany: false
        );

        var _existingAgent = Agent.Create(
            "Tkaczyk", 
            "Tomasz", 
            "Andrzej", 
            "68102910014", 
            "6941297604", 
            false);

		var existingAgents = new List<Agent> { _existingAgent }.AsQueryable();

		var mockDbSet = new Mock<DbSet<Agent>>();
		mockDbSet.As<IQueryable<Agent>>()
			.Setup(m => m.Provider)
			.Returns(new List<Agent> { _existingAgent }.AsQueryable().Provider);

		mockDbSet.As<IQueryable<Agent>>()
			.Setup(m => m.Expression)
			.Returns(new List<Agent> { _existingAgent }.AsQueryable().Expression);
		mockDbSet.As<IQueryable<Agent>>()
			.Setup(m => m.ElementType)
			.Returns(new List<Agent> { _existingAgent }.AsQueryable().ElementType);
		mockDbSet.As<IQueryable<Agent>>()
			.Setup(m => m.GetEnumerator())
			.Returns(new List<Agent> { _existingAgent }.AsQueryable().GetEnumerator());
		_repositoryMock.Setup(r => r.Get(It.IsAny<ISpecification<Agent>>()))
			.Returns(mockDbSet.Object);
		_repositoryMock.Setup(r => r.Get(It.IsAny<ISpecification<Agent>>())
				.SingleOrDefaultAsync(It.IsAny<CancellationToken>()))
			.ReturnsAsync(_existingAgent);

		var _handler = new CreateAgentHandler(_repositoryMock.Object, _unitOfWorkMock.Object);

		await Assert.ThrowsAsync<InvalidIdentifierException>(() => _handler.Handle(_command, CancellationToken.None));

	}
}