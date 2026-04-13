using Clean_Architecture_Sample.Domain;
using Clean_Architecture_Sample.Domain.Interfaces;
using Clean_Architecture_Sample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clean_Architecture_Sample.Infrastructure.Repositories;

public class ProductRepository(DataDbContext dbContext) : IProductRepository
{
    private readonly DataDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> UpdateAsync(int id, Product product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(
            current => current.Id == id,
            cancellationToken);

        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.Description = product.Description;
        existingProduct.ImageFile = product.ImageFile;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(
            product => product.Id == id,
            cancellationToken);

        if (existingProduct is null)
        {
            return false;
        }

        _dbContext.Products.Remove(existingProduct);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}