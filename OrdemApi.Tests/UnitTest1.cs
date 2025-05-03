using Moq;
using OrdemApi.Controllers;
using OrdemApi.Services;
using OrdemApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using OrderCommonModels.Models;

public class OrdemControllerTests
{
    [Fact]
    public async Task UploadCsv_ReturnsOk_WhenValidFileUploaded()
    {
     
        var mockRabbitMq = new Mock<IRabbitMqService>();
        var mockRepo = new Mock<NegociacoesRepository>("fake-connection-string");

        var controller = new OrdemController(mockRabbitMq.Object, mockRepo.Object);

        var content = "Compra;PETR4;30.5;100\nVenda;VALE3;70.3;50";
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        var formFile = new FormFile(stream, 0, bytes.Length, "file", "ordens.csv");

        
        var result = await controller.UploadCsv(formFile);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Arquivo processado e ordens enviadas!", okResult.Value);
    }

    [Fact]
    public async Task GetOrdensProcessadas_ReturnsOk_WithList()
    {
        var mockRabbitMq = new Mock<IRabbitMqService>();
        var mockRepo = new Mock<NegociacoesRepository>("fake-connection-string");

        var fakeOrdens = new List<OrdemProcessada>
    {
        new OrdemProcessada { NomeAtivo = "PETR4", TipoOrdem = "Compra", Status = "Executada" }
    };

        mockRepo.Setup(r => r.ObterOrdensProcessadas()).ReturnsAsync(fakeOrdens);

        var controller = new OrdemController(mockRabbitMq.Object, mockRepo.Object);

        // Act
        var result = await controller.GetOrdensProcessadas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedList = Assert.IsType<List<OrdemProcessada>>(okResult.Value);
        Assert.Single(returnedList);
    }
}
