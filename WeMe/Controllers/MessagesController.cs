using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeMe.Models;

namespace WeMe.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly WeMeContext _context;

        public MessagesController(WeMeContext context)
        {
            _context = context;
        }

        [HttpGet("get-mess-box/{id}")]
        public ActionResult<Messages> GetMessageBox(int id)
        {
            var box = _context.Messages
                .GroupBy(mess => new
                {
                    mess.FromUserId,
                    mess.ToUserId,
                    mess.ToUser.FullName,
                    mess.ToUser.Avatar
                })
                .Select(mess => new
                {
                    from_id = mess.Key.FromUserId,
                    to_id = mess.Key.ToUserId,
                    to_name = mess.Key.FullName,
                    avatar = mess.Key.Avatar,
                    content = _context.Messages.OrderByDescending(m => m.Id)
                                .FirstOrDefault(messx => (messx.ToUserId == mess.Key.ToUserId && messx.FromUserId == mess.Key.FromUserId) ||
                                (messx.ToUserId == mess.Key.FromUserId && messx.FromUserId == mess.Key.ToUserId)).Content
                })
                .Where(mess => mess.from_id == id);
            return Ok(box);
        }

        [HttpGet("get-all-mess")]
        public ActionResult<Messages> GetAllMessage(int from_id, int to_id)
        {
            var box = _context.Messages
                .Select(mess => new
                {
                    from_id = mess.FromUserId,
                    to_id = mess.ToUserId,
                    from = new
                    {
                        name = mess.FromUser.FullName,
                        avatar = mess.FromUser.Avatar
                    },
                    to = new
                    {
                        name = mess.ToUser.FullName,
                        avatar = mess.ToUser.Avatar
                    },
                    content = mess.Content
                })
                .Where(mess => (mess.from_id == from_id && mess.to_id == to_id) || (mess.from_id == to_id && mess.to_id == from_id));
            return Ok(box);
        }

        [HttpPost("send-message")]
        public async Task<ActionResult<Messages>> SendMessage(Messages message)
        {
            message.CreatedAt = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var check = _context.Messages.FirstOrDefault(mess => mess.FromUserId == message.ToUserId && mess.ToUserId == message.FromUserId);

            if (check == null)
            {
                Messages mess = new Messages();
                mess.FromUserId = message.ToUserId;
                mess.ToUserId = message.FromUserId;
                mess.Content = "Bây giờ các bạn có thể chat với nhau.";
                mess.CreatedAt = DateTime.Now;

                _context.Messages.Add(mess);
                await _context.SaveChangesAsync();
            }

            return Ok(new { status = true, repli = check == null });
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Messages>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Messages>> GetMessages(int id)
        {
            var messages = await _context.Messages.FindAsync(id);

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessages(int id, Messages messages)
        {
            if (id != messages.Id)
            {
                return BadRequest();
            }

            _context.Entry(messages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Messages>> PostMessages(Messages messages)
        {
            _context.Messages.Add(messages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessages", new { id = messages.Id }, messages);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Messages>> DeleteMessages(int id)
        {
            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();

            return messages;
        }

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
