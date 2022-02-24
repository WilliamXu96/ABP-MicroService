using BaseService.BaseData;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace BaseService.DataSeeder
{
    public class BaseServiceDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<DataDictionary, Guid> _dataDicRepository;
        private readonly IRepository<DataDictionaryDetail, Guid> _dataDicDetailRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BaseServiceDataSeeder(
            IRepository<DataDictionary, Guid> dataDicRepository,
            IRepository<DataDictionaryDetail, Guid> dataDicDetailRepository,
            IGuidGenerator guidGenerator
            )
        {
            _dataDicRepository = dataDicRepository;
            _dataDicDetailRepository = dataDicDetailRepository;
            _guidGenerator = guidGenerator;
        }

        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await CreateDataDictionaries();
        }

        //创建数据字典
        private async Task CreateDataDictionaries()
        {
            var id = _guidGenerator.Create();
            await _dataDicRepository.InsertAsync(new DataDictionary(id, null, "condition", "表单条件"));
            await CreateDataDictionaryDetails(id);
        }

        private async Task CreateDataDictionaryDetails(Guid pid)
        {
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), null, pid, "等于", "=", 0));
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), null, pid, "大于", ">", 1));
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), null, pid, "小于", "<", 2));
        }
    }
}
