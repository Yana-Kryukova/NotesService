using NotesService.Application.Models.Base;

namespace NotesService.Application.Services.Abstractions.Base;

public interface IApplicationService<TModel, TCreateModel, in TId>
     where TModel : IModel<TId>
     where TId : struct, IEquatable<TId>
     where TCreateModel : ICreateModel
{
    Task<TModel?> GetModelByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TModel>> GetModelsAsync(CancellationToken cancellationToken = default);

    Task<TModel?> CreateModelAsync(TCreateModel userInformation, CancellationToken cancellationToken = default);

    Task<bool> UpdateModelAsync(TModel user, CancellationToken cancellationToken = default);

    Task<bool> DeleteModelAsync(TId id, CancellationToken cancellationToken = default);
}

