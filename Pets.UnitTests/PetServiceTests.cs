using FluentValidation;
using FluentValidation.Results;
using FluentAssertions;
using Pets.Application.Services;
using Pets.Contracts.Requests;
using Pets.Contracts.Models;
using NSubstitute;
using Pets.Application.Interfaces;
using Pets.Contracts.Responses;
using Pets.Application.Entities;
using Pets.Application.Validators;
using NSubstitute.ExceptionExtensions;
using Pets.Persistence.Repositories;

namespace Pets.UnitTests;

public class PetServiceTests 
{
    private readonly PetService _sut;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IValidator<CreatePetRequest> _createPetValidator;
    private readonly IValidator<UpdatePetRequest> _updatePetValidator;

    public PetServiceTests()
    {
        _createPetValidator = new CreatePetRequestValidator();
        _updatePetValidator = new UpdatePetRequestValidator();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _sut = new PetService(_unitOfWorkMock, _createPetValidator,_updatePetValidator); // Adjust constructor parameters as needed  
    }

    [Fact]
    public async Task CreateAsync_InValidRequest_ReturnsValidationException()
    {
        // Arrange
        var request = new CreatePetRequest
        {
            // Set the properties of the request object
            Name = "",
            PetTypeId = 0,
            MissingSince = default(DateTime),
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        
        _unitOfWorkMock.PetRepository.AddAsync(Arg.Any<Pet>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.SaveChanges().Returns(true);

        // Act
        var exception = await Record.ExceptionAsync(() => _sut.CreateAsync(request));

        // Assert
        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().ContainAll("Name is required", "Pet Type is required","Missing Since is required");

        // Fluent Assertions way of doing the above
        //Func<Task> act = async () => await _sut.CreateAsync(request);
        //await act.Should().ThrowExactlyAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsPetResponse()
    {
        // Arrange
        var request = new CreatePetRequest
        {
            // Set the properties of the request object
            Name = "Fluffy",
            PetTypeId = 1,
            MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        _unitOfWorkMock.PetRepository.AddAsync(Arg.Any<Pet>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.SaveChanges().Returns(true);

        // Act
        var response = await _sut.CreateAsync(request);

        // Assert
        response.Should().NotBeNull()
            .And.BeOfType<PetResponse>()
            .And.Match<PetResponse>(r =>
                r.Name == request.Name &&
                r.PetTypeId == request.PetTypeId &&
                r.MissingSince == request.MissingSince &&
                r.Description == request.Description &&
                r.MicroChipId == request.MicroChipId &&
                r.OwnerName == request.OwnerName &&
                r.OwnerEmail == request.OwnerEmail);
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsNull_If_FailedToSave()
    {
        // Arrange
        var request = new CreatePetRequest
        {
            // Set the properties of the request object
            Name = "Fluffy",
            PetTypeId = 1,
            MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        _unitOfWorkMock.PetRepository.AddAsync(Arg.Any<Pet>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.SaveChanges().Returns(false);

        // Act
        var response = await _sut.CreateAsync(request);

        // Assert
        response.Should().BeNull();           
    }

    [Fact]
    public async Task UpdateAsync_InValidRequest_ReturnsValidationException()
    {
        // Arrange
        int id = 1;
        var request = new UpdatePetRequest
        {
            // Set the properties of the request object
            Name = "",
            PetTypeId = 0,
            MissingSince = default(DateTime),
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        _unitOfWorkMock.PetRepository.AddAsync(Arg.Any<Pet>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.SaveChanges().Returns(true);

        // Act
        var exception = await Record.ExceptionAsync(() => _sut.UpdateAsync(id, request));

        // Assert
        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().ContainAll("Name is required", "Pet Type is required", "Missing Since is required");       
    }

    [Fact]
    public async Task UpdateAsync_ValidRequest_ReturnsTrue()
    {
        // Arrange
        int id = 1; 
        var request = new UpdatePetRequest
        {
            // Set the properties of the request object
            Name = "Fluffy",
            PetTypeId = 1,
            MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        var existingPet = new Pet
        {
            Id = id,
            Name = "ExistingFluffy",
            PetTypeId = 2,
            MissingSince = DateTime.Today.AddDays(-2), // Set MissingSince to today's date minus 1 day
            Description = "Existing A fluffy cat",
            MicroChipId = "01234567890",
            OwnerName = "Existing John Doe",
            OwnerEmail = "existingjohn@test.com"
        };

        _unitOfWorkMock.PetRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(existingPet);

        _unitOfWorkMock.PetRepository.Update(existingPet, Arg.Any<CancellationToken>());
         
        _unitOfWorkMock.SaveChanges().Returns(true);
        
        // Act
        var response = await _sut.UpdateAsync(id,request, CancellationToken.None);

        // Assert
        response.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPetResponse()
    {
        // Arrange
        int id = 1;
        var existingPet = new Pet
        {
            // Set the properties of the request object
            Id = id,
            Name = "Fluffy",
            PetTypeId = 1,
            PetType = new PetType { Id = 1, Name = "Cat" },
            MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        _unitOfWorkMock.PetRepository.GetByIdWithPetTypeAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Pet?>(existingPet));

        // Act
        var response = await _sut.GetByIdAsync(id, CancellationToken.None);

        // Assert
        response.Should().NotBeNull()
            .And.BeOfType<PetResponse>()
            .And.Match<PetResponse>(r =>
                r.Id == id &&
                r.Name == existingPet.Name &&
                r.PetTypeId == existingPet.PetTypeId &&
                r.MissingSince == existingPet.MissingSince &&
                r.Description == existingPet.Description &&
                r.MicroChipId == existingPet.MicroChipId &&
                r.OwnerName == existingPet.OwnerName &&
                r.OwnerEmail == existingPet.OwnerEmail);
    }

    [Fact]
    public async Task DeleteAsync_Returns_True()
    {
        // Arrange
        int id = 1;
        var existingPet = new Pet
        {
            // Set the properties of the request object
            Id = id,
            Name = "Fluffy",
            PetTypeId = 1,
            PetType = new PetType { Id = 1, Name = "Cat" },
            MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
            Description = "A fluffy cat",
            MicroChipId = "1234567890",
            OwnerName = "John Doe",
            OwnerEmail = "john@test.com"
        };

        _unitOfWorkMock.PetRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Pet?>(existingPet));

        _unitOfWorkMock.PetRepository.Delete(existingPet, Arg.Any<CancellationToken>());
        _unitOfWorkMock.SaveChanges().Returns(true);

        // Act
        var response = await _sut.DeleteAsync(id, CancellationToken.None);

        // Assert
        response.Should().BeTrue();
            
    }

    [Fact]
    public async Task GetAllAsync_Returns_PaginatedResponse()
    {
        // Arrange
        var options = new GetAllPetsRequestOptions
        {
            PageNumber = 1,
            PageSize = 1
        };

        var pets = new List<Pet>
        {
            new Pet
            {
                Id = 1,
                Name = "Fluffy",
                PetTypeId = 1,
                PetType = new PetType { Id = 1, Name = "Cat" },
                MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
                Description = "A cat",
                MicroChipId = "1234567890",
                OwnerName = "John Doe",
                OwnerEmail = "john@test.com"
            },
            new Pet
            {
                Id = 2,
                Name = "Rover",
                PetTypeId = 2,
                PetType = new PetType { Id = 2, Name = "Dog" },
                MissingSince = DateTime.Today.AddDays(-1), // Set MissingSince to today's date minus 1 day
                Description = "A dog",
                MicroChipId = "2345678901",
                OwnerName = "Sally Doe",
                OwnerEmail = "sally@test.com"
            }
        };

         _unitOfWorkMock.PetRepository.ListAllWithOptionsAsync(options, CancellationToken.None)
           .Returns(Task.FromResult<IReadOnlyList<Pet>>(pets));

        // Act
        PagedList<PetResponse> response = await _sut.GetAllAsync(options, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response.CurrentPage.Should().Be(1);
        response.PageSize.Should().Be(1);
        response.TotalCount.Should().Be(2);
    }
}

