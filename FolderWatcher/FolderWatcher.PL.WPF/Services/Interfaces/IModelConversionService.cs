using FolderWatcher.PL.WPF.Model.Base;
using System.Threading.Tasks;

namespace FolderWatcher.PL.WPF.Services.Interfaces
{
    public interface IModelConversionService<TModel, TTransferClass> where TModel : BaseModel where TTransferClass : class
    {
        Task<TModel> ConvertToModel(TTransferClass transfer_class);

        Task<TTransferClass> ConvertToTransfer(TModel model_class);
    }
}