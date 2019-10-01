using System;
using System.Collections.Generic;
using System.Linq;
using AdressBook;
using Microsoft.AspNetCore.Mvc;

namespace Contact_List.Controllers
{
    [ApiController]
    [Route("/contacts")]
    public class ContactController : ControllerBase
    {
        private static readonly List<Contact> contacts = new List<Contact>();

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Contact newContacts)
        {
            if (contacts.Where(i => i.Id == newContacts.Id).Count() == 0)
            {
                contacts.Add(newContacts);
                return Created("Person successfully created", newContacts);
            }
            return BadRequest("Invalid input(e.g.required field missing or empty)");
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteItem(int personId)
        {
            if (personId < 0)
            {
                return BadRequest("Invalid ID supplied");
            }
            try
            {
                Contact c = contacts.Single(i => i.Id == personId);
                    contacts.Remove(c);
                    return NoContent();
            }
            catch (InvalidOperationException e)
            {
                return NotFound("Person not found");
            }

        }

        [HttpGet]
        [Route("findByName", Name = "GetSpecificItem")]
        public IActionResult GetItem([FromQuery] string nameFilter)
        {
            if (nameFilter != null)
            {
                IEnumerable<Contact> c = contacts.Where(i => i.LastName?.ToUpper()?.StartsWith(nameFilter.ToUpper())==true);
                if (c != null)
                {
                    return Ok(c);
                }
            }
            return BadRequest("Invalid or missing name");
        }
    }
}
