
using IServices.ISysServices;
using IServices.ITaskServices;
using Microsoft.EntityFrameworkCore;
using Models.TaskModels;
using Services.Repository;

namespace Services.TaskServices
{
    public class TaskCenterService : RepositoryBase<TaskCenter>, ITaskCenterService
    {
        public TaskCenterService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    
    }
}