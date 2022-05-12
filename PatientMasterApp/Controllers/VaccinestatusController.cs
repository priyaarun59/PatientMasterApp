using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientMasterApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace PatientMasterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinestatusController : ControllerBase
    {
        private readonly PatientContext _context;
        readonly ILogger<VaccinestatusController> _log;

        public VaccinestatusController(PatientContext context, ILogger<VaccinestatusController> log)
        {
            _context = context;
            _log = log;
        }

        [HttpGet]
        [Route("Vaccineinfo")]
        public async Task<IEnumerable<Vaccinestatus>> GetVaccineStatus()
        {
            return await _context.DbVaccinestatus.ToListAsync();
        }

        [HttpGet]
        //[Route("GetGeAppGroupSuppMaster")]
        public IQueryable<Object> getallvaccinedetails(int vaccId)
        {


            var list = (from a in _context.DbVaccinestatus
                            // join p in _context.GeAppGroupSupplierMaster on a.SupId equals p.suppId
                            // where p.GroupId == groupId
                            // where a.Status == '1'
                        orderby a.Dose
                        ascending
                        select new
                        {
                            PatientId = a.PatientId,
                            Dose = a.Dose
                        }).Take(3000);//.Take(5);

            return list;
        }
        [HttpPost]
        [Route("Vaccineinfosave")]
        public async Task<ActionResult<Vaccinestatus>> PostVacccineInfo(Vaccinestatus vaccinestatus)
        {
            _context.DbVaccinestatus.Add(vaccinestatus);
            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetDCandidate", new { id = dCandidate.id }, dCandidate);
            return vaccinestatus;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutvaccineInfo(int id, Vaccinestatus vaccinestatus)
        {
            if (id != vaccinestatus.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(vaccinestatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool VaccineExists(int id)
        {
            return _context.DbPatientInfo.Any(e => e.PatientId == id);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Putvaccinestatus(int id, Vaccinestatus putvaccine)
        //{
        //    if (id != putvaccine.VaccineStatusId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(putvaccine).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!vaccineExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        private bool vaccineExists(int id)
        {
            return _context.DbVaccinestatus.Any(e => e.VaccineStatusId == id);
        }

        [HttpDelete("{id}")]
       
        public async Task<ActionResult<Vaccinestatus>> Deletevaccinestatus(int id)
        {
            var vaccinedel = await _context.DbVaccinestatus.FindAsync(id);
            if (vaccinedel == null)
            {
                return NotFound();
            }

            _context.DbVaccinestatus.Remove(vaccinedel);
            await _context.SaveChangesAsync();
            _log.LogInformation("Deleted Group" + id);
            return vaccinedel;
        }
    }
}
