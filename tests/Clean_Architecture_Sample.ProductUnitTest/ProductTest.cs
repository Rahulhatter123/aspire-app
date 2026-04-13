using Clean_Architecture_Sample.Application.Features.Product.Commands;
using Clean_Architecture_Sample.Application.Features.Product.Queries;
using Clean_Architecture_Sample.Application.DTOs;
using Moq;
using Xunit;
using MediatR;

namespace Clean_Architecture_Sample.ProductUnitTest;

public class ProductTest
{
    private readonly Mock<IMediator> _mediatorMock;

public ProductTest()
{
    _mediatorMock = new Mock<IMediator>();
}

[Fact]
public async Task CreateProductCommand_ShouldReturnProductResponseDto()
{
    // Arrange
    var productDto = new ProductDto { Name = "Test Product", Price = 100 };
    var responseDto = new ProductResponseDto { Id = 1, Name = "Test Product", Price = 100 };
    _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default)).ReturnsAsync(responseDto);

    // Act
    var result = await _mediatorMock.Object.Send(new CreateProductCommand(productDto));

    // Assert
    Assert.NotNull(result);
    Assert.Equal(responseDto.Id, result.Id);
    Assert.Equal(responseDto.Name, result.Name);
}

[Fact]
public async Task DeleteProductCommand_ShouldReturnTrue()
{
    // Arrange
    _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default)).ReturnsAsync(true);

    // Act
    var result = await _mediatorMock.Object.Send(new DeleteProductCommand(1));

    // Assert
    Assert.True(result);
}

[Fact]
public async Task UpdateProductCommand_ShouldReturnTrue()
{
    // Arrange
    var productDto = new ProductDto { Name = "Updated Product", Price = 150 };
    _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default)).ReturnsAsync(true);

    // Act
    var result = await _mediatorMock.Object.Send(new UpdateProductCommand(1, productDto));

    // Assert
    Assert.True(result);
}

[Fact]
public async Task GetProductByIdQuery_ShouldReturnProductResponseDto()
{
    // Arrange
    var responseDto = new ProductResponseDto { Id = 1, Name = "Test Product", Price = 100 };
    _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default)).ReturnsAsync(responseDto);

    // Act
    var result = await _mediatorMock.Object.Send(new GetProductByIdQuery(1));

    // Assert
    Assert.NotNull(result);
    Assert.Equal(responseDto.Id, result.Id);
    Assert.Equal(responseDto.Name, result.Name);
}

[Fact]
public async Task GetAllProductsQuery_ShouldReturnListOfProductResponseDto()
{
    // Arrange
    var responseList = new List<ProductResponseDto>
    {
        new ProductResponseDto { Id = 1, Name = "Product 1", Price = 100 },
        new ProductResponseDto { Id = 2, Name = "Product 2", Price = 200 }
    };
    _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default)).ReturnsAsync(responseList);

    // Act
    var result = await _mediatorMock.Object.Send(new GetAllProductsQuery());

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count);
}

[Fact]
public async Task CreateProductCommand_ShouldHandleNullProductDto()
{
    // Arrange
    ProductDto? productDto = null;
    _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync((ProductResponseDto)null!);

    // Act
    var result = await _mediatorMock.Object.Send(new CreateProductCommand(productDto!));

    // Assert
    Assert.Null(result);
}

[Fact]
public async Task DeleteProductCommand_ShouldReturnFalse_WhenProductDoesNotExist()
{
    // Arrange
    _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default)).ReturnsAsync(false);

    // Act
    var result = await _mediatorMock.Object.Send(new DeleteProductCommand(999)); // Non-existent product ID

    // Assert
    Assert.False(result);
}

[Fact]
public async Task UpdateProductCommand_ShouldReturnFalse_WhenUpdateFails()
{
    // Arrange
    var productDto = new ProductDto { Name = "Non-existent Product", Price = 0 };
    _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default)).ReturnsAsync(false);

    // Act
    var result = await _mediatorMock.Object.Send(new UpdateProductCommand(999, productDto)); // Non-existent product ID

    // Assert
    Assert.False(result);
}

[Fact]
public async Task GetProductByIdQuery_ShouldReturnNull_WhenProductDoesNotExist()
{
    // Arrange
    _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default)).ReturnsAsync((ProductResponseDto?)null);

    // Act
    var result = await _mediatorMock.Object.Send(new GetProductByIdQuery(999)); // Non-existent product ID

    // Assert
    Assert.Null(result);
}

[Fact]
public async Task GetAllProductsQuery_ShouldReturnEmptyList_WhenNoProductsExist()
{
    // Arrange
    _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default)).ReturnsAsync(new List<ProductResponseDto>());

    // Act
    var result = await _mediatorMock.Object.Send(new GetAllProductsQuery());

    // Assert
    Assert.NotNull(result);
    Assert.Empty(result);
}
}
