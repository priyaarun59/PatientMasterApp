using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
using PatientMasterApp.Model;

namespace PatientMasterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientinfoController : ControllerBase
    {
        private readonly PatientContext _context;
        readonly ILogger<PatientinfoController> _log;

        public PatientinfoController(PatientContext context, ILogger<PatientinfoController> log)
        {
            _context = context;
            _log = log;
        }

        // GET: api/Patientinfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientInfo>>> GetGeAppGroupMaster()
        {

            return await _context.DbPatientInfo.ToListAsync();
        }
        [HttpGet]
        [Route("Patientinfo")]
        public IQueryable<Object> GetAllPatientinfo(int groupId)
        {
                        var list = (from a in _context.DbPatientInfo
                            // join p in _context.GeAppGroupSupplierMaster on a.SupId equals p.suppId
                            // where p.GroupId == groupId
                            // where a.Status == '1'
                        orderby a.PatientName
                        ascending
                        select new
                        {
                            PatientId = a.PatientId,
                            PatientName = a.PatientName,
                            age = a.age,
                            gender = a.Gender,
                            vaccinestatus = a.vaccinestatus
                        }); //Take(3000);//.Take(5);

            return list;
        }

        //private void Take(int v)
        //{
        //    throw new NotImplementedException();
        //}

        private object ToList()
        {
            throw new NotImplementedException();
        }
      //  [HttpPut("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientInfo(int id, PatientInfo patientInfo)
        {
            if (id != patientInfo.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(patientInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientInfoExists(id))
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
        // PUT: api/Patientinfo/

        //[HttpPut]
        //public Task<IActionResult> PutPatientinfo(int id, PatientInfo patientinfo)
        //{
        //    if (id != patientinfo.PatientId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(patientinfo).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PatientInfoExists(id))
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
        private bool PatientInfoExists(int id)
        {
            return _context.DbPatientInfo.Any(e => e.PatientId == id);
        }

        [HttpPost]
        [Route("Patientinfosave")]
        public async Task<ActionResult<PatientInfo>> PostPatientInfo(PatientInfo patientInfo)
        {
            _context.DbPatientInfo.Add(patientInfo);
            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetDCandidate", new { id = dCandidate.id }, dCandidate);
            return patientInfo;
        }


        // DELETE: api/PatientinfoDelete/5
        [HttpDelete("{id}")]
        // [Route("PatientinfoDelete")]
        public async Task<ActionResult<PatientInfo>> DeletePatientinfo(int id)
        {
            var patientmaster = await _context.DbPatientInfo.FindAsync(id);
            if (patientmaster == null)
            {
                return NotFound();
            }

            _context.DbPatientInfo.Remove(patientmaster);
            await _context.SaveChangesAsync();
            _log.LogInformation("Deleted Group" + id);
            return patientmaster;
        }

    }
}
