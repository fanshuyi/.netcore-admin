using IServices.ICourseServices;
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.CourseModels;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class CourseTypeService : RepositoryBase<CourseType>, ICourseTypeService
    {
        public CourseTypeService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}