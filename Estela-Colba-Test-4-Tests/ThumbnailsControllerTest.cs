using Estela_Colba_Test_4.Thumbnails;
using Estela_Colba_Test_4.Thumbnails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using static System.String;

namespace Estela_Colba_Test_4_Tests;

public class ThumbnailsControllerTest
{
    private readonly ILogger<ThumbnailsController> _logger = NullLogger<ThumbnailsController>.Instance;
    private readonly Mock<IThumbnailRepository> _mockRepo = new();
    private readonly ThumbnailsController _controller;

    public ThumbnailsControllerTest()
    {
        _controller = new ThumbnailsController(_mockRepo.Object, NullLogger<ThumbnailsController>.Instance);
    }
    
    //GETALL
    [Fact]
    public async Task GivenListOfThumbnails_WhenGetAllThumbnails_ThenReturnOkResult()
    {
        //Arrange
        //_mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Thumbnail>());

        //Act
        var data = await _controller.GetAll();

        //Assert
        Assert.IsType<OkObjectResult>(data.Result);
    }

    [Fact]
    public async Task GivenListOfThumbnails_WhenGetAllThumbnails_ThenReturnListOfThumbnails()
    {
        //Arrange
        var thumbnails = new List<Thumbnail>()
        {
            new Thumbnail (
                Guid.Parse("7954b1a3-bd59-411f-b3d2-d75e533d2a4d"),
                "Test",
                "Test Description", 
                0, 
                0, 
                Empty, 
                Empty
            ), 
            new Thumbnail (
                Guid.Parse("c50e7596-1e4c-48e8-a85f-a4f9e8c26abf"),
                "Test 2",
                "Test 2 Description", 
                0, 
                0, 
                Empty, 
                Empty
            )
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(thumbnails);

        //Act
        var data = await _controller.GetAll();

        //Assert
        Assert.IsType<OkObjectResult>(data.Result);

    }

    //GETBYID
    [Fact]
    public async Task GinvenNoThumbnail_WhenGetThumbnailById_ThenReturnsNotFound()
    {
        // Arrange
        var testSessionId = Guid.Parse("bfa90f61-1611-45e6-a1f9-c99793197567");
        _mockRepo.Setup(repo => repo.GetById(testSessionId))
            .ReturnsAsync((Thumbnail?) null);

        // Act
        var result = await _controller.GetById(testSessionId);

        // Assert
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(testSessionId, notFoundObjectResult.Value);
    }

    [Fact]
    public async Task GivenThumbnail_WhenGetThumbnailById_ThenReturnsThumbnail()
    {
        //Arrange
        var id = Guid.Parse("bfa90f61-1611-45e6-a1f9-c99793197567");
        var thumbnail = new Thumbnail(id, "Test", "Test Description", 0, 0, Empty, Empty);
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync(thumbnail);
        
        //Act
        var result = await _controller.GetById(id);
        
        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    //CREATE
    [Fact]
    public async Task GivenNoThumbnail_WhenCreateThumbnail_ThenReturnError()
    {
        // Arrange 
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = await _controller.Create(createThumbnailRequest:  null);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }
    
    [Fact]
    public async Task GivenCreateThumbnailRequest_WhenCreate_ThenReturnThumbnail()
    {
        var thumbnail = new CreateThumbnailRequest
        {
            Name = "Prueba",
            Description = "Prueba"
        };
        _mockRepo.Setup(repo => repo.CreateThumbnail(thumbnail))
            .ReturnsAsync(new Thumbnail
            {
                Id = Guid.NewGuid(),
                Name = thumbnail.Name,
                Description = thumbnail.Description
            });

        var response = await _controller.Create(thumbnail);
        Assert.IsType<OkObjectResult>(response.Result);
    }
    
    //UPDATE
    [Fact]
    public async Task GivenThumbnail_WhenModifiedAThumbnail_ThenReturnModifiedThumbnail()
    {
        //Arrange
        var id = Guid.Parse("bfa90f61-1611-45e6-a1f9-c99793197567");
        var newThumbnail = new CreateThumbnailRequest()
        {
            Name = "New Test", Description = "Test Description", Width = 0, Height = 0, OriginalRoute = Empty, ThumbnailRoute = Empty
        };
        var thumbnail = new Thumbnail(id, newThumbnail.Name, newThumbnail.Description, newThumbnail.Height, newThumbnail.Width,
            newThumbnail.OriginalRoute, newThumbnail.ThumbnailRoute);
        _mockRepo.Setup(repo => repo.UpdateThumbnail(id, newThumbnail)).ReturnsAsync(thumbnail);

        //Act
        var result = await _controller.Update(id, newThumbnail);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GivenThumbnail_WhenModifiedAThumbnail_ThenReturnNotFound()
    {
        /*
        var id = Guid.Parse("bfa90f61-1611-45e6-a1f9-c99793197567");
        _mockRepo.Setup(repo => repo.UpdateThumbnail(id, newThumbnail)).ReturnsAsync(null);
        */
    }
}