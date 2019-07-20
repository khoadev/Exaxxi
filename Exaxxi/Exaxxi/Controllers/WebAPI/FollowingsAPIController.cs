using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.ViewModels;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public FollowingsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Followings
        [HttpGet]
        public IEnumerable<Followings> GetFollowings()
        {
            return _context.Followings;
        }

        // GET: api/Followings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followings = await _context.Followings.FindAsync(id);

            if (followings == null)
            {
                return NotFound();
            }

            return Ok(followings);
        }

        [Route("SaveFollowing/{follow}/{idItem}")]
        public async Task<IActionResult> SaveFollowing(int follow, int idItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Take ID DSSize Table
            var takeIdDsSize = _context.ds_Size.Where(p => p.VN == follow).FirstOrDefault().id;

            //Take ID Size Table
            var takeIdSize = _context.Sizes.Where(p => p.id_ds_size == takeIdDsSize && p.id_item == idItem).FirstOrDefault().id;

            if(String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return BadRequest("Please Login For Following!");
            }

            //Take ID User
            var takeIdUser = HttpContext.Session.GetInt32("idUser").Value;

            //Count Row in Following Talbe
            var count = _context.Followings.Where(p => p.id_size == takeIdSize && p.id_user == takeIdUser).Count();
            if (count > 0)
            {
                return BadRequest("This size is followed in your account!");
            }

            Followings fol = new Followings();
            fol.id_user = takeIdUser;
            fol.id_size = takeIdSize;

            _context.Followings.Add(fol);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route("TakeFollowOfUser/{idUser}")]
        public IActionResult TakeFollowOfUser(int idUser)
        {
            var follow = _context.Followings
                .Join(_context.Sizes, a => a.id_size, b => b.id, (a, b) => new { a, b })
                .Join(_context.Items, c => c.b.id_item, d => d.id, (c, d) => new { c, d })
                .Join(_context.ds_Size, e => e.c.b.id_ds_size, f => f.id, (e,f) => new { e,f })
                .Where(p => p.e.c.a.id_user == idUser)
                .Select(g => new FollowingViewModel {
                    items = g.e.d,
                    sizes = g.e.c.b, 
                    ds_Sizes = g.f
                });

            return Ok(follow);
        }

    }
}