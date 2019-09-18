using AutoMapper;
using BusinessLayer.Author;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModel.Author;

namespace WebApplication2.Controllers
{
    [HandleError]
    public class AuthorController : Controller
    {
        protected IMapper mapper;
        #region before adding GenericRepository
        //private AuthorRepository authorRepository;

        //public AuthorController()
        //{
        //    authorRepository = new AuthorRepository(new Model1());
        //}
        //public ActionResult Index()
        //{
        //    var model = authorRepository.GetAll();
        //    return View(model);
        //}
        #endregion

        #region Generic Repository
        //private IRepository<Authors> authorRepository;

        //public AuthorController()
        //{
        //    authorRepository = new Repository<Authors>();
        //}
        //public ActionResult Index()
        //{
        //    var model = authorRepository.GetAll();
        //    return View(model);
        //}
        #endregion

        public AuthorController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var authorList = authorBO.GetAuthorsList();
            var model = authorList.Select(m=> mapper.Map<AuthorViewModel>(m)).ToList();
            return View(model);
        }

        //[Authorize (Roles ="WriteAndRead")]
        public ActionResult Edit(int? id)
        {
            //ReturnEx(id);
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<AuthorViewModel>(authorBO);
            if (id != null)
            {
                var authorBOList = authorBO.GetAuthorsListById(id);
                model = mapper.Map<AuthorViewModel>(authorBOList);
            }
            return View(model);
        }


        //[FiltersHelper]
        //private void ReturnEx(int? id)
        //{
        //    int zero = 0;
        //    var temp = id / zero;
        //}

        [HttpPost]
        public ActionResult Edit(AuthorViewModel model)
        {
            var authorBO = mapper.Map<AuthorBO>(model);
            if (ModelState.IsValid)
            {
                authorBO.Save();
                return RedirectToAction("Index");
            }
            else return View(model);

            
        }
    }
}