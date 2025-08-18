using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Models;
using System.Drawing.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemContext _context;
        
        public ItemsController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/male
        [HttpGet("male")]
        public async Task<ActionResult<Item>> GetItemMale()
        {
            var items = await _context.Items.Where(u => u.Gender == "male").ToListAsync(); ;

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // GET: api/Items/male
        [HttpGet("female")]
        public async Task<ActionResult<Item>> GetItemFemale()
        {
            var items = await _context.Items.Where(u => u.Gender == "female").ToListAsync(); ;

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem([FromForm] ItemUploadDto itemDto) {
            Console.WriteLine(itemDto.Path);
            var fileName = itemDto.Path.FileName;
            var subFolder = itemDto.Gender;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string storedFileDirectory = Path.Combine(wwwrootPath, subFolder);

            if (!Directory.Exists(storedFileDirectory)) { 
                Directory.CreateDirectory(storedFileDirectory);
            
            }
            string route = Path.Combine(storedFileDirectory, fileName);

            using (var ms = new MemoryStream()) {
                await itemDto.Path.CopyToAsync(ms);
                var content = ms.ToArray();
                await System.IO.File.WriteAllBytesAsync(route, content);
            }

            var fileLocation = Path.Combine(subFolder, fileName).Replace("\\", "/");
            Item item = new Item();
            item.Name = itemDto.Name;
            item.Gender = itemDto.Gender;
            item.Path = fileName;
            item.Price = itemDto.Price;


            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("by-path/{id}")]
        public async Task<IActionResult> PutItemNoImage(int id, [FromBody] Item item)
        {
            if (id != item.Id)
                return BadRequest();

            var dbItem = await _context.Items.FindAsync(id);
            if (dbItem == null)
                return NotFound();

            dbItem.Name = item.Name;
            dbItem.Gender = item.Gender;
            dbItem.Path = item.Path;  // string path
            dbItem.Price = item.Price;
            Console.WriteLine("Hello " + item.Id);
            Console.WriteLine("Hello " + item.Name);
            Console.WriteLine("Hello " + item.Gender);
            Console.WriteLine("Hello " + item.Path);
            Console.WriteLine("Hello " + item.Price);
            await _context.SaveChangesAsync();
            return NoContent();
        }





        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, [FromForm] ItemUploadDto itemDto)
        {
            if (id != itemDto.Id)
            {
                return BadRequest();
            }
            if (itemDto.Path == null)
            {
                return BadRequest("Path (file) is required.");
            }

            var fileName = itemDto.Path.FileName;
            var subFolder = itemDto.Gender;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string storedFileDirectory = Path.Combine(wwwrootPath, subFolder);

            if (!Directory.Exists(storedFileDirectory))
            {
                Directory.CreateDirectory(storedFileDirectory);

            }
            string route = Path.Combine(storedFileDirectory, fileName);

            using (var ms = new MemoryStream())
            {
                await itemDto.Path.CopyToAsync(ms);
                var content = ms.ToArray();
                await System.IO.File.WriteAllBytesAsync(route, content);
            }

            var fileLocation = Path.Combine(subFolder, fileName).Replace("\\", "/");
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            
            item.Id = itemDto.Id;
            item.Name = itemDto.Name;
            item.Gender = itemDto.Gender;
            item.Path = fileName;
            item.Price = itemDto.Price;
            Console.WriteLine("Hello " + item.Id);
            Console.WriteLine("Hello " + item.Name);
            Console.WriteLine("Hello " + item.Gender);
            Console.WriteLine("Hello " + item.Path);
            Console.WriteLine("Hello " + item.Price);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
