using MemeIndex.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ControllerBase = MemeIndex.Server.ControllerBase;

namespace MemeIndex.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemeController : ControllerBase
    {
        private readonly ILogger<MemeController> _logger;
        private MemeDBContext _context;

        public MemeController(ILogger<MemeController> logger, MemeDBContext context)
        {
            _logger = logger;
            _context = context as MemeDBContext;
        }

        #region Memes
        [HttpGet]
        [Route("GetAllMemes")]
        public ActionResult<List<Meme>> GetAllMemes()
        {
            try
            {
                List<Meme> result = _context.Memes.Include(m => m.MetaData).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return new List<Meme>();
            }
        }

        [HttpGet]
        [Route("GetMemesWithMaxAmount")]
        public ActionResult<List<Meme>> GetMemesWithMaxAmount(int maxAmount = 100, int offset = 0)
        {
            if (maxAmount == 0) return BadRequest();
            try
            {
                List<Meme> result = _context.Memes.Include(m => m.MetaData).Skip(offset).Take(maxAmount).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return new List<Meme>();
            }
        }

        [HttpGet]
        [Route("GetMemesByCategory")]
        public ActionResult<List<Meme>> GetMemesByCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) return BadRequest();

            try
            {
                List<Meme> result = _context.Memes.Include(m => m.MetaData).Where(m => m.MetaData.Any(d => d.Data == category)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return new List<Meme>();
            }
        }


        [HttpGet]
        [Route("GetCategoriesWithMaxAmount")]
        public ActionResult<List<MemeCategory>> GetCategoriesWithMaxAmount(int maxAmount = 100, int offset = 0)
        {
            if (maxAmount == 0) return BadRequest();
            try
            {
                List<MemeCategory> result = _context.MemeCategorys.Skip(offset).Take(maxAmount).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return new List<MemeCategory>();
            }
        }


        #endregion Memes
    }
}