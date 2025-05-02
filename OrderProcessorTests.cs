using Moq;
using Xunit;

public class OrderProcessorTests
{
    private readonly Mock<IRepository> _mockRepository;
    private readonly OrderProcessor _processor;

    public OrderProcessorTests()
    {
        _mockRepository = new Mock<IRepository>();
        _processor = new OrderProcessor(_mockRepository.Object);
    }

    [Fact]
    public async Task ProcessOrder_ShouldCallSaveBusiness()
    {
        // Arrange
        var ordem = new Ordem { TipoOrdem = "C", NomeAtivo = "Ativo1", Preco = 100.5m, Quantidade = 500 };

        // Act
        await _processor.ProcessOrder(ordem);

        // Assert
        _mockRepository.Verify(repo => repo.SaveBusiness(It.IsAny<Negocio>()), Times.Once);
    }
}
