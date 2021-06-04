using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.PropositionServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GridFS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ElamanaTakaful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropositionController : Controller
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<Proposition> _propositionService;
        private readonly PropositionRepository _propositionRepository;
        public PropositionController(ElamanaTakafulContext context, IDataRepository<Proposition> propositionService)
        {
            _context = context;
            _propositionService = propositionService;
            _propositionRepository = new(_context, _propositionService);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Proposition> GetPropositions()
        {
            return _propositionService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddPropositionStandard(Proposition proposition)
        {
            _propositionService.Add(proposition);
            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("[action]")]
        public IActionResult AddProposition()
        {
            string fileId = "";
            string fileN = "";
            Proposition proposition = new();
            try
            {
                proposition = JsonConvert.DeserializeObject<Proposition>(Request.Form["data"]);
                Debug.Print("--> " + proposition.Quantity);
                var file = Request.Form.Files[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var id = _propositionRepository.UploadFromStream(fileName, file.OpenReadStream());
                fileId = id.ToString();
                fileN = fileName;
            }
            catch (Exception ex)
            {
                StatusCode(500, $"Internal server error: {ex}");
            }
            proposition.QuoteId = fileId;
            proposition.QuoteName = fileN;

            _propositionService.Add(proposition);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateProposition(Proposition proposition)
        {
            _propositionService.Update(proposition);
            return Ok();
        }

        [HttpPut, DisableRequestSizeLimit]
        [Route("[action]")]
        public IActionResult UpdateQuote()
        {
            string fileId = "";
            string fileN = "";
            Proposition proposition = new();
            try
            {
                proposition = JsonConvert.DeserializeObject<Proposition>(Request.Form["data"]);
                Debug.Print("--> " + proposition.Quantity);
                var file = Request.Form.Files[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var id = _propositionRepository.UploadFromStream(fileName, file.OpenReadStream());
                fileId = id.ToString();
                fileN = fileName;
            }
            catch (Exception ex)
            {
                StatusCode(500, $"Internal server error: {ex}");
            }
            proposition.QuoteId = fileId;
            proposition.QuoteName = fileN;

            _propositionService.Update(proposition);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteProposition([FromRoute] int id)
        {
            var existingProposition = _propositionService.Get(id);
            if (existingProposition != null)
            {
                _propositionService.Delete(existingProposition.PropositionId);
                return Ok();
            }
            return NotFound($"Proposition Not Found with ID : {existingProposition.PropositionId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Proposition GetProposition([FromRoute] int id)
        {
            return _propositionService.Get(id);
        }


        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult ValidateProposition([FromRoute] int id, [FromBody] User validator)
        {
            _propositionRepository.ValidateProposition(id, validator);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{number}")]
        public Proposition FindPropositionByPropositionNumber([FromRoute] int number)
        {
            return _propositionRepository.FindPropositionByPropositionNumber(number);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Proposition> FindPropositionByStartDate([FromBody] DateTime startDate)
        {
            return _propositionRepository.FindPropositionByStartDate(startDate);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Proposition> FindPropositionByEndDate([FromBody] DateTime endDate)
        {
            return _propositionRepository.FindPropositionByEndDate(endDate);
        }


        [HttpGet]
        [Route("[action]/{status}")]
        public IEnumerable<Proposition> FindPropositionByStatus([FromRoute] bool status)
        {
            return _propositionRepository.FindPropositionByStatus(status);
        }

        [HttpGet]
        [Route("[action]/{direction}")]
        public IEnumerable<Proposition> FindPropositionByDirection([FromRoute] string direction)
        {
            return _propositionRepository.FindPropositionByDirection(direction);
        }

        [HttpGet]
        [Route("[action]/{supplierName}")]
        public IEnumerable<Proposition> FindPropositionBySupplierName([FromRoute] string supplierName)
        {
            return _propositionRepository.FindPropositionBySupplierName(supplierName);
        }

        [HttpGet]
        [Route("[action]/{supplierCode}")]
        public IEnumerable<Proposition> FindPropositionBySupplierNumber([FromRoute] string supplierCode)
        {
            return _propositionRepository.FindPropositionBySupplierNumber(supplierCode);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult ActivateProposition([FromRoute] int id)
        {
            _propositionRepository.ActivateProposition(id);
            return Ok();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult DeactivateProposition([FromRoute] int id)
        {
            _propositionRepository.DeactivateProposition(id);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{fileName}")]
        public GridFSDownloadStream GetPropositionQuote([FromRoute] string fileName)
        {
            var stream = _propositionRepository.DownloadFromStream(fileName);
            var name = stream?.FileInfo.Filename;
            Response.ContentType = name;
            return stream;
        }


        [HttpGet, DisableRequestSizeLimit]
        [Route("[action]/{id}")]
        public List<GridFSDownloadStream> GetPropositionQuoteHistoryById([FromRoute] int id)
        {
            List<string> quoteHistoryIds = _propositionRepository.GetQuoteHistoryIds(id);
            List <GridFSDownloadStream> files = new();
            foreach(string fileId in quoteHistoryIds) { 
                var stream = _propositionRepository.DownloadFromStream(fileId);
                var name = stream?.FileInfo.Filename;
                Response.ContentType = name;
                files.Add(stream);
            }
            return files;
        }
    }
}
