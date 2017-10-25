using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGColombia.Api.Service;
using NGColombia.Api.Filters;
using NGColombia.Api.Dto.Input;

namespace NGColombia.Api.Controllers
{
    [Route("confrmation")]
    public class ConfirmationController : Controller
    {
        private readonly ITicketService service;

        public ConfirmationController(ITicketService service)
        {
            this.service = service;
        }

        // GET: Confirmation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Confirmation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet, Route("/create")]
        // GET: Confirmation/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet, Route("/create")]
        // POST: Confirmation/Create
        [HttpPost]
        public async  Task<ActionResult> Create(PayUConfirmation model)
        {
            var result = await service.Confirm(model);
            return Ok(result);
        }

        // GET: Confirmation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Confirmation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Confirmation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Confirmation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}