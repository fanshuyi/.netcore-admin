using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.UserModels;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 标签
    /// </summary>
    [Area("Platform")]
    public class DomainLabelController : Controller
    {
        private readonly IDomainLabelService _iDomainLabelService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _iSysUserService;
        private readonly ISysMessageCenterService _iSysMessageService;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="iDomainLabelService">
        /// </param>
        /// <param name="iSysUserService">
        /// </param>
        public DomainLabelController(IDomainLabelService iDomainLabelService, IUnitOfWork unitOfWork, UserManager<IdentityUser> iSysUserService, ISysMessageCenterService iSysMessageService)
        {
            _iDomainLabelService = iDomainLabelService;
            _unitOfWork = unitOfWork;
            _iSysUserService = iSysUserService;
            _iSysMessageService = iSysMessageService;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword">
        /// </param>
        /// <param name="ordering">
        /// </param>
        /// <param name="pageIndex">
        /// </param>
        /// <param name="pageSize">
        /// </param>
        /// <param name="search">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync(string keyword, string ordering, int pageIndex = 1, int pageSize = 20, bool search = false, bool toExcelFile = false)
        {
            var model = _iDomainLabelService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.Label,
                                         a.LabelType,
                                         a.Heat,
                                         a.Id
                                     }).Search(keyword);
            if (search)
            {
                model = model.Search(Request.Query);
            }
            if (!string.IsNullOrEmpty(ordering))
            {
                model = model.OrderBy(ordering);
            }
            else
            {
                model = model.OrderByDescending(a => a.Heat);
            }
            if (toExcelFile)
            {
                return model.ToExcelFile();
            }
            return View(model.PageResult(pageIndex, pageSize));
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var item = await _iDomainLabelService.FindAsync(id);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> CreateAsync()
        {
            return RedirectToAction("Edit");
        }

        // GET: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> EditAsync(string id)
        {
            var item = new DomainLabel();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iDomainLabelService.FindAsync(id);
            }
            return View(item);
        }

        // POST: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="collection">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string id, DomainLabel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            await _iDomainLabelService.SaveAsync(id, collection);

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string[] id)
        {
            await _iDomainLabelService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}