using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Events;
using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Categories.Factories;
using LookingPromos.SharedKernel.Domain.Categories.Repositories;
using LookingPromos.SharedKernel.Models;
using MassTransit;

namespace LookingPromos.Worker.Application.Categories.Consumers;

public sealed class CategoryCreatedEventConsumer(
    IUnitOfWork unitOfWork,
    ICategoryRepository categoryRepository,
    ICategoryFactory factory
) : IConsumer<CategoryCreatedEvent>
{
    public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
    {
        var categoryId = context.Message.CategoryId;
        
        var category = await categoryRepository.GetWithNetworkAssociationAsync(categoryId, context.CancellationToken);
        
        if (category == null)
        {
            var error = new Error("Category not found", $"Category with id {categoryId} not found", ErrorType.Failure);
            
            await context.RespondAsync(Result.Failure<Category>(error, HttpStatusCode.NotFound));
            
            return;
        }
        
        await factory.CreateStrategyAsync(category, context.CancellationToken);
        
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}