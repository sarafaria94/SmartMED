using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartMed.MedicationManagement.API.DataAccessLayer;
using SmartMed.MedicationManagement.API.Models;

namespace SmartMed.MedicationManagement.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MedicationController : Controller
    {
        private MedicationDataAccessLayer dal = new MedicationDataAccessLayer();

        [HttpGet]
        [Route("add")]
        public ActionResult<int> AddMedication(string name, int quantity)
        {
            return dal.AddMedication(name, quantity);
        }

        [HttpGet]
        [Route("list")]
        public ActionResult ListMedication()
        {
            return Json(dal.ListMedication());
        }

        [HttpGet]
        [Route("delete")]
        public ActionResult<bool> DeleteMedication(int id)
        {
            return dal.DeleteMedication(id);
        }

    }
}
